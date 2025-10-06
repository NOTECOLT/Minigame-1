using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public int score;

    [SerializeField] Transform _envTransform;

    [SerializeField] GameObject _playerPrefab;
    [SerializeField] GameObject _copPrefab;
    [SerializeField] List<Transform> _copSpawnpoints;
    [SerializeField] GameObject _coinPrefab;

    UIManager _ui;

    GameObject _player;
    List<GameObject> _cops = new List<GameObject>();
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

        DestroyAllCops();
        SpawnCop();
    }

    void DestroyAllCops()
    {
        foreach (GameObject cop in _cops)
        {
            Destroy(cop);
        }
        _cops = new List<GameObject>();
    }

    void SpawnCop()
    {
        GameObject cop = Instantiate(_copPrefab);

        Transform spawnpoint = _copSpawnpoints[UnityEngine.Random.Range(0, _copSpawnpoints.Count)];

        cop.transform.position = spawnpoint.position;
        cop.GetComponent<CopController>().target = _player.transform;
        _cops.Add(cop);
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
        SpawnCop();
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
