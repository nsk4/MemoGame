using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoardManager : MonoBehaviour
{

    [SerializeField] private float spacing;
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private Tile tilePrefab;

    private List<Tile> tiles;
    private Tile flippedTile1, flippedTile2;

    public event EventHandler MatchFoundEvent;

    private readonly System.Random random = new();

    // Start is called before the first frame update
    void Start()
    {
        flippedTile1 = null;
        flippedTile2 = null;
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        tiles = new List<Tile>();

        List<int> numbers = new();
        numbers.AddRange(Enumerable.Range(0, width * height / 2));
        numbers.AddRange(Enumerable.Range(0, width * height / 2));

        numbers = numbers.OrderBy(x => random.Next()).ToList();

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Tile spawnedTile = Instantiate(tilePrefab, new Vector3(spacing * i, spacing * j), Quaternion.identity, this.transform);
                spawnedTile.TileNumber = numbers.ElementAt(0);
                numbers.RemoveAt(0);
                spawnedTile.OnMouseLeftClickEvent += OnTileLeftClickedEvent;
                tiles.Add(spawnedTile);
            }
        }

        transform.position = new Vector3(transform.position.x - spacing * width / 2f + 0.5f, transform.position.y - spacing * height / 2f + 0.5f, transform.position.z);
    }

    public Tile GetRandomTile()
    {
        return tiles.ElementAt(random.Next(tiles.Count));
    }

    public int GetNumberOfTiles()
    {
        return tiles.Count;
    }

    public void OnTileLeftClickedEvent(object sender, EventArgs e)
    {
        // If 2 tiles were flipped before and they did not match then flip them back.
        if (flippedTile1 != null && flippedTile2 != null)
        {
            flippedTile1.Flip();
            flippedTile2.Flip();
            flippedTile1 = null;
            flippedTile2 = null;
        }

        // Check if tile has already been flipped. If yes, then there is nothing to be done.
        Tile tile = (Tile)sender;
        if (tile.IsOpen)
        {
            return;
        }

        // Flip the tile and check if it is the 1st flipped tile.
        tile.Flip();
        if (flippedTile1 == null)
        {
            flippedTile1 = tile;
            return;
        }

        // If tile is the 2nd flipped tile then compare it with the 1st tile.
        flippedTile2 = tile;
        if (flippedTile1.TileNumber == flippedTile2.TileNumber)
        {
            // Match was found, remove tiles from flipped history.
            // TODO: play match found sound
            flippedTile1 = null;
            flippedTile2 = null;
            MatchFoundEvent?.Invoke(this, EventArgs.Empty);
        }
    }

    public void OnSwapRandomTilesEvent(object sender, EventArgs e)
    {
        Tile a = GetRandomTile();
        Tile b;
        do
        {
            b = GetRandomTile();
        } while (a == b);
        Vector3 posA = a.transform.position;
        Vector3 posB = b.transform.position;
        a.Move(posB);
        b.Move(posA);
    }
}
