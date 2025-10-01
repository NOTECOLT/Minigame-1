using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    [SerializeField] Transform _focus;
    [SerializeField] float _speed = 2;

    public bool followRotation = false;
    [SerializeField] float _rotationSpeed = 2;

    GameManager _gm;
    void Start()
    {
        _gm = FindObjectOfType<GameManager>();
        _gm.OnNewGame += SetFocus;
    }

    void OnDestroy()
    {
        _gm.OnNewGame -= SetFocus;
    }

    void SetFocus(GameObject player)
    {
        _focus = player.transform;
    }

    void Update()
    {
        if (_focus == null) return;

        transform.position = Vector3.Lerp(transform.position, new Vector3(_focus.position.x, _focus.position.y, transform.position.z), _speed * Time.deltaTime);

        if (followRotation)
            transform.rotation = Quaternion.Lerp(transform.rotation, _focus.rotation, _rotationSpeed * Time.deltaTime);
    }
}
