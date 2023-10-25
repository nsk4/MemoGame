using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private BoardManager boardManager;
    [SerializeField] private GameObject gameOverMenu;

    // Start is called before the first frame update
    void Start()
    {
        boardManager.GameOverEvent += OnGameOverEvent;
    }

    public void OnGameOverEvent(object sender, EventArgs e)
    {
        gameOverMenu.SetActive(true);
    }
}
