using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Manage board tiles and grid generation.
/// </summary>
public class BoardManager : MonoBehaviour
{
    [SerializeField] private float spacing;
    [SerializeField] private Tile tilePrefab;

    /// <summary>
    /// Event signaling that the game is over.
    /// </summary>
    public event EventHandler GameOverEvent;

    private int pairsLeft;
    private int tilesToNextEffect;
    private List<Tile> tiles;
    private Tile flippedTile1, flippedTile2;
    private int totalFlipCount;

    private readonly System.Random random = new();

    void Start()
    {
        pairsLeft = Settings.PairCount;
        tilesToNextEffect = Settings.EffectPeriod;
        flippedTile1 = null;
        flippedTile2 = null;
        totalFlipCount = 0;
        GenerateGrid();
    }

    /// <summary>
    /// Generate grid by instantiating tiles.
    /// </summary>
    private void GenerateGrid()
    {
        tiles = new List<Tile>();
        List<int> numbers = new();
        numbers.AddRange(Enumerable.Range(0, Settings.PairCount));
        numbers.AddRange(Enumerable.Range(0, Settings.PairCount));
        numbers = numbers.OrderBy(x => random.Next()).ToList();

        foreach (Vector3 position in GenerateTilePositions(numbers.Count))
        {
            Tile spawnedTile = Instantiate(tilePrefab, position, Quaternion.identity, this.transform);
            spawnedTile.TileNumber = numbers.ElementAt(0);
            numbers.RemoveAt(0);
            spawnedTile.MouseLeftClickEvent += OnTileLeftClickedEvent;
            tiles.Add(spawnedTile);
        }
    }

    /// <summary>
    /// Generate tile positions in a circle. Each outer circle contains 6 more tiles than the inner one. if tiles in the outermost circle cannot finish the circle they will be evenly spread around the circle.
    /// </summary>
    /// <param name="totalTiles">Total positions to generate.</param>
    /// <returns>List of tile positions. The Z axis is set to 0.</returns>
    private List<Vector3> GenerateTilePositions(int totalTiles)
    {
        List<Vector3> positions = new List<Vector3>();
        int numConcentricCircles = 0;
        int currentTileCount = 0;

        while (currentTileCount < totalTiles)
        {
            numConcentricCircles++;
            float angleStep = 2 * (float)Math.PI / (numConcentricCircles * 6);
            // If the remainder of the tiles do not fit into the final circle then spread it out;
            if (totalTiles - currentTileCount < numConcentricCircles * 6)
            {
                angleStep = 2 * (float)Math.PI / (totalTiles - currentTileCount);
            }

            for (int i = 0; i < numConcentricCircles * 6; i++)
            {
                if (currentTileCount >= totalTiles)
                {
                    break;
                }
                float angle = i * angleStep;
                float currentRadius = numConcentricCircles * 1.5f; // 1.5 for spacing scaling
                float x = currentRadius * (float)Math.Cos(angle);
                float y = currentRadius * (float)Math.Sin(angle);
                positions.Add(new Vector3(x, y, 0));
                currentTileCount++;
            }
        }

        return positions;
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

    /// <summary>
    /// Event triggered on tile left click.
    /// </summary>
    /// <param name="sender">Event sender</param>
    /// <param name="e">Event parameters</param>
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
        totalFlipCount++;
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

    /// <summary>
    /// Logic executed when the matching tiles are found.
    /// </summary>
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

    /// <summary>
    /// Swap position of 2 random unopened tiles. 
    /// </summary>
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

    /// <summary>
    /// Get the total flip count for the game.
    /// </summary>
    /// <returns>Total flip count of the game.</returns>
    public int GetTotalFlipCount()
    {
        return totalFlipCount;
    }
}
