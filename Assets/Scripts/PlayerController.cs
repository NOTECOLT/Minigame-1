using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    static float ACCELERATION_FACTOR_MIN = 1.00f;
    static float ACCELERATION_FACTOR_MAX = 0.75f;
    PlayerInput _input;
    Rigidbody2D _rb;

    float _accelerationFactor = ACCELERATION_FACTOR_MIN;

    public float movementSpeed = 5;
    public float turnSpeed = 5;

    void Start()
    {
        _input = new PlayerInput();

        _input.Player.Enable();

        _rb = GetComponent<Rigidbody2D>();
    }


    void OnDestroy()
    {
        _input.Player.Disable();   
    }

    void FixedUpdate()
    {
        float zEuler = transform.localRotation.eulerAngles.z + 90;
        _rb.velocity = new Vector2(Mathf.Cos(zEuler*Mathf.Deg2Rad), Mathf.Sin(zEuler*Mathf.Deg2Rad)).normalized * movementSpeed * 100 * Time.deltaTime;
        _rb.velocity *= _accelerationFactor;

        if (_input.Player.Brake.IsPressed())
        {
            if (_accelerationFactor >= ACCELERATION_FACTOR_MAX)
                _accelerationFactor -= 0.025f;
        }
        else
        {
            if (_accelerationFactor <= ACCELERATION_FACTOR_MIN)
                _accelerationFactor += 0.025f;
        }

        transform.Rotate(new Vector3(0, 0, _input.Player.Turn.ReadValue<float>() * turnSpeed * 100 * Time.deltaTime));
    }
}
