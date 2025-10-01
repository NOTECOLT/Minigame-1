using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopController : MonoBehaviour
{
    static float SPEED_FACTOR_MIN = 1.00f;
    static float SPEED_FACTOR_MAX = 0.6f;
    static float ACCELERATION = 0.01f; // How fast the car will speed up/down
    Rigidbody2D _rb;

    float _speedFactor = SPEED_FACTOR_MIN;

    public float movementSpeed = 5;
    public float turnSpeed = 5;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }


    void OnDestroy()
    {

    }

    void FixedUpdate()
    {
        float zEuler = transform.localRotation.eulerAngles.z + 90;
        _rb.velocity = new Vector2(Mathf.Cos(zEuler * Mathf.Deg2Rad), Mathf.Sin(zEuler * Mathf.Deg2Rad)).normalized * movementSpeed * 100 * Time.deltaTime;
        _rb.velocity *= _speedFactor;

        // if (false) // brake
        // {
        //     if (_speedFactor >= SPEED_FACTOR_MAX)
        //         _speedFactor -= ACCELERATION;
        // }
        // else if (false) // reverse
        // {
        //     if (_speedFactor >= -0.5f)
        //         _speedFactor -= ACCELERATION;
        // } else
        // {
        //     if (_speedFactor <= SPEED_FACTOR_MIN)
        //         _speedFactor += ACCELERATION;
        // }

        // transform.Rotate(new Vector3(0, 0, _input.Player.Turn.ReadValue<float>() * turnSpeed * 100 * Time.deltaTime));
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Wall")
        {
            if (_speedFactor >= 0.85f)
            {
            }
        }
        else if (collision.collider.gameObject.tag == "Coin")
        {
            movementSpeed += 1;
        }
    }
}
