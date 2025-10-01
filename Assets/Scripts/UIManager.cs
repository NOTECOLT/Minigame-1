using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject _gameOverScreen;
    [SerializeField] GameObject _scoreCount;
    GameManager _gm;
    void Start()
    {
        _gm = FindObjectOfType<GameManager>();
    }

    void OnDestroy()
    {
    }

    public void TriggerGameOverScreen()
    {
        _gameOverScreen.SetActive(true);
    }

    public void DisableGameOverScreen()
    {
        _gameOverScreen.SetActive(false);
    }

    public void UpdateScore(int score)
    {
        _scoreCount.GetComponent<TMPro.TMP_Text>().text = $"Coins: {score}";
    }
}
