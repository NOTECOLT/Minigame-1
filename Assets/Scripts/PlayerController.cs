using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float rotation = 0;
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

    void Update()
    {
    }

    void FixedUpdate()
    {
        // float zEuler = transform.localRotation.eulerAngles.z + 90;

        if (_input.Player.Brake.IsPressed())
        {
            if (_speedFactor >= speedFactorMin)
                _speedFactor -= acceleration;
        }
        else if (_input.Player.Reverse.IsPressed())
        {
            if (_speedFactor >= -0.5f)
                _speedFactor -= acceleration;
        }
        else
        {
            if (_speedFactor <= speedFactorMax)
                _speedFactor += acceleration;
        }

        _rb.linearVelocity = new Vector2(Mathf.Cos(rotation * Mathf.Deg2Rad), Mathf.Sin(rotation * Mathf.Deg2Rad)).normalized * movementSpeed * 100 * Time.deltaTime;
        _rb.linearVelocity *= _speedFactor;


        rotation += _input.Player.Turn.ReadValue<float>() * turnSpeed * 100 * Time.deltaTime;
        if (rotation < 0) rotation += 360;
        else if (rotation >= 360) rotation -= 360;

        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, rotation + 90));
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


// if (rotation < 22.5 || rotation > 337.5)
// {
//     GetComponent<SpriteRenderer>().sprite = right;
// }
// else if (rotation >= 22.5 && rotation <= 67.5)
// {
//     GetComponent<SpriteRenderer>().sprite = rightUp;
// }
// else if (rotation > 67.5 && rotation < 112.5)
// {
//     GetComponent<SpriteRenderer>().sprite = up;
// }
// else if (rotation >= 112.5 && rotation <= 157.5)
// {
//     GetComponent<SpriteRenderer>().sprite = leftUp;
// }
// else if (rotation > 157.5 && rotation < 202.5)
// {
//     GetComponent<SpriteRenderer>().sprite = left;
// }
// else if (rotation >= 202.5 && rotation <= 247.5)
// {
//     GetComponent<SpriteRenderer>().sprite = leftDown;
// }
// else if (rotation > 247.5 && rotation < 292.5)
// {
//     GetComponent<SpriteRenderer>().sprite = down;
// }
// else if (rotation >= 292.5 && rotation <= 337.5)
// {
//     GetComponent<SpriteRenderer>().sprite = rightDown;
// }