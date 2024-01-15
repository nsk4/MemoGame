using System;
using TMPro;
using UnityEngine;

/// <summary>
/// Tile object containing number and other various properties.
/// </summary>
public class Tile : MonoBehaviour
{
    [SerializeField] private GameObject highlight;
    [SerializeField] private TextMeshProUGUI text;

    [SerializeField] private RotationAnimation rotationAnimation;
    [SerializeField] private SpinMoveAnimation spinMoveAnimation;

    public event EventHandler MouseLeftClickEvent;
    public bool IsOpen { get; set; }

    private int tileNumber;
    public int TileNumber { get { return tileNumber; } set { tileNumber = value; text.SetText(value.ToString()); } }

    private bool mouseDown;

    void Start()
    {
        mouseDown = false;
        IsOpen = false;
        transform.Rotate(Vector3.up, 180.0f); // Close the tile
    }

    void OnMouseDown()
    {
        mouseDown = true;
    }

    void OnMouseUp()
    {
        if (mouseDown)
        {
            MouseLeftClickEvent?.Invoke(this, EventArgs.Empty);
            mouseDown = false;
        }
    }
    void OnMouseEnter()
    {
        highlight.SetActive(true);
    }

    void OnMouseExit()
    {
        mouseDown = false;
        highlight.SetActive(false);
    }

    /// <summary>
    /// Flip the tile.
    /// </summary>
    public void Flip()
    {
        FindObjectOfType<AudioManager>().Play("TileFlip");
        IsOpen = !IsOpen;
        rotationAnimation.Rotate180();
    }

    /// <summary>
    /// Start the tile spin move animation to the given destination.
    /// </summary>
    /// <param name="destination">Destination to move tile to.</param>
    public void Move(Vector3 destination)
    {
        // TODO: sync sound and swapping
        FindObjectOfType<AudioManager>().Play("TileSpin");
        spinMoveAnimation.SpinMove(destination);
    }
}
