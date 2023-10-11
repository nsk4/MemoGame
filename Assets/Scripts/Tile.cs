using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject highlight;
    [SerializeField] private TextMeshProUGUI text;

    [SerializeField] private RotationAnimation rotationAnimation;
    [SerializeField] private SpinMoveAnimation spinMoveAnimation;

    public event EventHandler OnMouseLeftClickEvent;
    public bool IsOpen { get; set; }

    private int tileNumber;
    public int TileNumber { get { return tileNumber; } set { tileNumber = value; text.SetText(value.ToString()); } }

    void Start()
    {
        IsOpen = false;
        transform.Rotate(Vector3.up, 180.0f); // Close the tile
    }

    void OnMouseEnter()
    {
        highlight.SetActive(true);
    }

    void OnMouseExit()
    {
        highlight.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnMouseLeftClickEvent?.Invoke(this, EventArgs.Empty);
        }
    }

    public void Flip()
    {
        // TODO: play flip sound
        IsOpen = !IsOpen;
        rotationAnimation.Rotate180();
    }

    public void Move(Vector3 destination)
    {
        // TODO: play move sound
        spinMoveAnimation.SpinMove(destination);
    }
}
