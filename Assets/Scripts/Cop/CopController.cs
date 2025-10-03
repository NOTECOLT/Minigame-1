using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CopController : MonoBehaviour
{
    #region Intercept

    // Instead of following the player, just go ahead of the player

    #endregion

    public float movementSpeed = 7;
    public float turnSpeed = 1;
    public float speedFactorMax = 1.00f;
    public float speedFactorMin = 0.45f;
    public float acceleration = 0.02f; // How fast the car will speed up/down. Expressed as a percentage of the total movementSpeed.

    CopState _state;
    public float speedFactor;
    public Transform target;

    void Start()
    {
        speedFactor = speedFactorMax;
        _state = new ChasePlayer(this);
    }


    void OnDestroy()
    {

    }

    void ChangeState(CopState newState)
    {
        if (_state.GetType() == newState.GetType()) return;

        // Debug.Log($"[{name}] Switching to state {newState}");
        _state.OnExit();
        _state = newState;
    }

    void FixedUpdate()
    {
        _state.OnFixedUpdate();
        ChangeState(_state.NextState());
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Wall")
        {

        }
        else if (collision.collider.gameObject.tag == "Coin")
        {
            // movementSpeed += 1;
        }
    }
}
