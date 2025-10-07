using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class AvoidObstacle : CopState
{

    Rigidbody2D _rb;
    public AvoidObstacle(CopController cc) : base(cc)
    {
        _rb = cc.gameObject.GetComponent<Rigidbody2D>();
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();

        Transform transform = cc.transform;
        Transform target = cc.target;

        float zEuler = transform.localRotation.eulerAngles.z + 90;
        Vector3 forwardVector = new Vector3(Mathf.Cos(zEuler * Mathf.Deg2Rad), Mathf.Sin(zEuler * Mathf.Deg2Rad), 0).normalized;
        Vector3 targetVector = (target.position + transform.position).normalized;

        // DRAW RAYCAST VECTORS
        // Facing Vector
        Debug.DrawLine(transform.position, transform.position + forwardVector * 5, Color.red);

        // Target Vector
        if (transform != null)
            Debug.DrawLine(transform.position, transform.position + targetVector * 5, Color.yellow);

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

        // If the cop hit a wall, reverse
        if (Mathf.Abs(angle) > 100 || Mathf.Abs(angle) < 20)
        {
            if (cc.speedFactor >= -0.5f) cc.speedFactor -= cc.acceleration;
            if (direction == 0)
            {
                direction = (Random.Range(1, 3) == 1) ? 1 : -1;
            }
            else
            {
                direction *= -1;
            }            
        }
        else
        {
            if (cc.speedFactor <= cc.speedFactorMax) cc.speedFactor += cc.reverseAcceleration;
        }

        // FORWARD MOVEMENT
        _rb.linearVelocity = forwardVector * cc.movementSpeed * 100 * Time.deltaTime;
        _rb.linearVelocity *= cc.speedFactor;

        // TURNING
        transform.Rotate(new Vector3(0, 0, direction * cc.turnSpeed * 100 * Time.deltaTime));
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override CopState NextState()
    {
        Transform transform = cc.transform;

        float zEuler = transform.localRotation.eulerAngles.z + 90;
        Vector3 forwardVector = new Vector3(Mathf.Cos(zEuler * Mathf.Deg2Rad), Mathf.Sin(zEuler * Mathf.Deg2Rad), 0).normalized;

        RaycastHit2D hit = Physics2D.Raycast(transform.position+forwardVector, forwardVector, 5);
        Debug.DrawLine(transform.position+forwardVector, transform.position+forwardVector*5, Color.green);
    
        // Determine next state
        if (hit)
        {
            if (hit.collider.gameObject.tag == "Wall")
            {
                return this;
            }
        }
        return new ChasePlayer(cc);
    }
}
