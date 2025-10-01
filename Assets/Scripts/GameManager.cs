using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int score;
    [SerializeField] Transform _envTransform;

    [SerializeField] GameObject _coinPrefab;
    GameObject _currentCoin;

    void Start()
    {
        score = 1;
        GenerateCoin();
    }
    void Update()
    {

    }

    void GenerateCoin()
    {
        _currentCoin = Instantiate(_coinPrefab, _envTransform);
        _currentCoin.GetComponent<Coin>().OnCoinCollect += OnCoinCollect;
    }

    void OnCoinCollect()
    {
        score += 1;
        _currentCoin.GetComponent<Coin>().OnCoinCollect -= OnCoinCollect;
        Destroy(_currentCoin);
        GenerateCoin();
    }
}
