using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    [SerializeField] Transform _focus;
    [SerializeField] float _speed = 2;

    // Update is called once per frame
    void Update()
    {
        if (_focus == null) return;

        transform.position = Vector3.Lerp(transform.position, new Vector3(_focus.position.x, _focus.position.y, transform.position.z), _speed * Time.deltaTime);
    }
}
