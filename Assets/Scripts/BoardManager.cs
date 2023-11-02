using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField] private float spacing;
    [SerializeField] private Tile tilePrefab;

    public event EventHandler GameOverEvent;

    private int pairsLeft;
    private int tilesToNextEffect;
    private List<Tile> tiles;
    private Tile flippedTile1, flippedTile2;

    private readonly System.Random random = new();

    // Start is called before the first frame update
    void Start()
    {
        pairsLeft = Settings.PairCount;
        tilesToNextEffect = Settings.EffectPeriod;
        flippedTile1 = null;
        flippedTile2 = null;
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        tiles = new List<Tile>();

        List<int> numbers = new();
        numbers.AddRange(Enumerable.Range(0, Settings.PairCount));
        numbers.AddRange(Enumerable.Range(0, Settings.PairCount));
        numbers = numbers.OrderBy(x => random.Next()).ToList();

        int numRows = Mathf.FloorToInt(Mathf.Sqrt(Settings.PairCount));
        int row = 0;
        int col = 0;
        int tilesLeft = Settings.PairCount * 2;
        do
        {
            if (row > numRows)
            {
                row = 0;
                col++;
            }
            Tile spawnedTile = Instantiate(tilePrefab, new Vector3(spacing * row, spacing * col), Quaternion.identity, this.transform);
            spawnedTile.TileNumber = numbers.ElementAt(0);
            numbers.RemoveAt(0);
            spawnedTile.MouseLeftClickEvent += OnTileLeftClickedEvent;
            tiles.Add(spawnedTile);
            row++;
            tilesLeft--;
        } while (tilesLeft > 0);

        // Center board
        transform.position = new Vector3(transform.position.x - spacing * row / 2f - 0.5f, transform.position.y - spacing * col / 2f - 0.5f, transform.position.z);
    }

    /// <summary>
    /// Get a random unflipped tile.
    /// </summary>
    /// <returns>A random unflipped tile.</returns>
    private Tile GetRandomUnflippedTile()
    {
        List<Tile> tempUnflippedTileList = tiles.FindAll(x => !x.IsOpen);
        return tempUnflippedTileList.ElementAt(random.Next(tempUnflippedTileList.Count));
    }

    private void OnTileLeftClickedEvent(object sender, EventArgs e)
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
            MatchFound();
        }
    }

    private void MatchFound()
    {
        pairsLeft--;
        if (pairsLeft <= 0)
        {
            // TODO: play win sound
            GameOverEvent.Invoke(this, EventArgs.Empty);
        }
        else
        {
            tilesToNextEffect--;
            if (tilesToNextEffect <= 0)
            {
                // TODO: play special sound
                SwapRandomTiles();
                tilesToNextEffect = Settings.EffectPeriod;
            }
        }
    }

    private void SwapRandomTiles()
    {
        Tile a = GetRandomUnflippedTile();
        Tile b;
        do
        {
            b = GetRandomUnflippedTile();
        } while (a == b);
        Vector3 posA = a.transform.position;
        Vector3 posB = b.transform.position;
        a.Move(posB);
        b.Move(posA);
    }
}
