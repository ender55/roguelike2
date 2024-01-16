using System;
using Unity.VisualScripting;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    
    private void OnEnable()
    {
        Player.OnGameOver += Activate;
    }

    private void OnDisable()
    {
        Player.OnGameOver -= Activate;
    }

    private void Activate()
    {
        gameOverScreen.SetActive(true);
    }
}
