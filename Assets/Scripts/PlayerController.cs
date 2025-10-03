using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 7;
    public float turnSpeed = 1;
    public float speedFactorMax = 1.00f;
    public float speedFactorMin = 0.6f;
    public float acceleration = 0.01f; // How fast the car will speed up/down. Expressed as a percentage of the total movementSpeed. 
    PlayerInput _input;
    Rigidbody2D _rb;

    float _speedFactor;


    public event Action OnPlayerCollision;

    void Start()
    {
        _input = new PlayerInput();

        _input.Player.Enable();

        _rb = GetComponent<Rigidbody2D>();
        
        _speedFactor = speedFactorMax;
    }


    void OnDestroy()
    {
        _input.Player.Disable();
    }

    void FixedUpdate()
    {
        float zEuler = transform.localRotation.eulerAngles.z + 90;

        if (_input.Player.Brake.IsPressed())
        {
            if (_speedFactor >= speedFactorMin)
                _speedFactor -= acceleration;
        }
        else if (_input.Player.Reverse.IsPressed())
        {
            if (_speedFactor >= -0.5f)
                _speedFactor -= acceleration;
        } else
        {
            if (_speedFactor <= speedFactorMax)
                _speedFactor += acceleration;
        }
    
        _rb.velocity = new Vector2(Mathf.Cos(zEuler * Mathf.Deg2Rad), Mathf.Sin(zEuler * Mathf.Deg2Rad)).normalized * movementSpeed * 100 * Time.deltaTime;
        _rb.velocity *= _speedFactor;

        transform.Rotate(new Vector3(0, 0, _input.Player.Turn.ReadValue<float>() * turnSpeed * 100 * Time.deltaTime));
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Wall")
        {
            if (_speedFactor >= 0.85f)
            {
                _input.Player.Disable();
                OnPlayerCollision.Invoke();
            }
        }
        else if (collision.collider.gameObject.tag == "Cop")
        {
            _input.Player.Disable();
            OnPlayerCollision.Invoke();
        }
        else if (collision.collider.gameObject.tag == "Coin")
        {
            movementSpeed += 1;
        }
    }
}
