using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public event Action OnCoinCollect;

    [SerializeField] Vector2 _coinSpawnBoundsMin;
    [SerializeField] Vector2 _coinSpawnBoundsMax;

    void Start()
    {
        RandomizeSpawn();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Player")
        {
            OnCoinCollect.Invoke();
        }
        else
        {
            RandomizeSpawn();
        }
    }

    void RandomizeSpawn()
    {
        Vector3 spawnPoint = new Vector3(UnityEngine.Random.Range(_coinSpawnBoundsMin.x, _coinSpawnBoundsMax.x), UnityEngine.Random.Range(_coinSpawnBoundsMin.y, _coinSpawnBoundsMax.y), 0);
        transform.localPosition = spawnPoint;
    }
}
