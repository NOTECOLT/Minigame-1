using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CopController : MonoBehaviour
{
    public float movementSpeed = 7;
    public float turnSpeed = 1;
    public float speedFactorMin = 1.00f;
    public float speedFactorMax = 0.45f;
    public float acceleration = 0.02f; // How fast the car will speed up/down. Expressed as a percentage of the total movementSpeed.
    Rigidbody2D _rb;

    float _speedFactor;

    public Transform target;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        _speedFactor = speedFactorMin;
    }


    void OnDestroy()
    {

    }

    void FixedUpdate()
    {
        float zEuler = transform.localRotation.eulerAngles.z + 90;
        Vector3 forwardVector = new Vector3(Mathf.Cos(zEuler * Mathf.Deg2Rad), Mathf.Sin(zEuler * Mathf.Deg2Rad), 0).normalized;
        Vector3 targetVector = (target.position - transform.position).normalized;


        // DRAW RAYCAST VECTORS
        // Facing Vector
        Debug.DrawLine(transform.position, transform.position + forwardVector * 5, Color.red);

        // Target Vector
        if (transform != null)
            Debug.DrawLine(transform.position, transform.position + targetVector * 5, Color.yellow);


        // else if (false) // reverse
        // {
        //     if (_speedFactor >= -0.5f)
        //         _speedFactor -= ACCELERATION;


        // TURN TOWARDS VECTOR
        float angle = Vector3.SignedAngle(targetVector, forwardVector, Vector3.forward);

        float direction;
        if (angle < -20)
        {
            direction = 1;
        }
        else if (angle >= -20 && angle <= 20)
        {
            direction = 0;
        }
        else
        {
            direction = -1;
        }

        // SLOW DOWN
        // If player is behind the cop, slow down to lessen turning radius
        if (Mathf.Abs(angle) > 100)
        {
            if (_speedFactor >= speedFactorMax) _speedFactor -= acceleration;
        }
        else
        {
            if (_speedFactor <= speedFactorMin) _speedFactor += acceleration;
        }

        // FORWARD MOVEMENT
        _rb.velocity = forwardVector * movementSpeed * 100 * Time.deltaTime;
        _rb.velocity *= _speedFactor;

        // TURNING
        transform.Rotate(new Vector3(0, 0, direction * turnSpeed * 100 * Time.deltaTime));
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
            // movementSpeed += 1;
        }
    }
}
