using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int score;
    
    [SerializeField] Transform _envTransform;

    [SerializeField] GameObject _playerPrefab;
    [SerializeField] GameObject _coinPrefab;

    UIManager _ui;

    GameObject _player;
    GameObject _currentCoin;

    public event Action<GameObject> OnNewGame;

    void Start()
    {
        _ui = GetComponent<UIManager>();
        StartNewGame();
    }
    void Update()
    {

    }

    /// <summary>
    /// Spawns New Player & Restarts Game
    /// </summary>
    public void StartNewGame()
    {
        if (_player != null)
        {
            _player.GetComponent<PlayerController>().OnPlayerCollision -= OnPlayerCollision;
            Destroy(_player);
        }

        _player = Instantiate(_playerPrefab);
        _player.transform.position = Vector3.zero;
        _player.GetComponent<PlayerController>().OnPlayerCollision += OnPlayerCollision;

        score = 0;
        SpawnCoin();

        OnNewGame.Invoke(_player);
        _ui.DisableGameOverScreen();
        _ui.UpdateScore(score);
    }

    /// <summary>
    /// Spawns new coin and deletes existing coin if it exists
    /// </summary>
    void SpawnCoin()
    {
        if (_currentCoin != null)
        {
            _currentCoin.GetComponent<Coin>().OnCoinCollect -= OnCoinCollect;
            Destroy(_currentCoin);
        }
        _currentCoin = Instantiate(_coinPrefab, _envTransform);
        _currentCoin.GetComponent<Coin>().OnCoinCollect += OnCoinCollect;
    }

    void OnCoinCollect()
    {
        score += 1;
        _ui.UpdateScore(score);
        SpawnCoin();
    }

    void OnPlayerCollision()
    {
        GameOver();
    }

    void GameOver()
    {
        GetComponent<UIManager>().TriggerGameOverScreen();
    }
}
