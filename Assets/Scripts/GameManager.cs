using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private BoardManager boardManager;
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private int specialEventPeriod;

    private int tilesLeft;
    private int specialEventCounter;

    public event EventHandler SwapTilesEvent;

    // Start is called before the first frame update
    void Start()
    {
        specialEventCounter = specialEventPeriod;
        boardManager.MatchFoundEvent += OnMatchFoundEvent;
        SwapTilesEvent += boardManager.OnSwapRandomTilesEvent;

        tilesLeft = boardManager.GetNumberOfTiles();
    }

    private void OnMatchFoundEvent(object sender, EventArgs e)
    {
        tilesLeft -= 2;
        if (tilesLeft == 0)
        {
            // TODO: play win sound
            victoryPanel.SetActive(true);
        }
        else
        {
            specialEventCounter--;
            if (specialEventCounter == 0)
            {
                // TODO: play special sound
                SwapTilesEvent?.Invoke(sender, EventArgs.Empty);
                specialEventCounter = specialEventPeriod;
            }
        }
    }
}
