using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private CharacterController _controller;

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 inputDir = new Vector3(h, 0f, v);

        if (inputDir.sqrMagnitude > 0f)
        {
            Vector3 moveDir = Quaternion.Euler(0, _cameraTransform.eulerAngles.y, 0) * inputDir.normalized;

            transform.rotation = Quaternion.LookRotation(moveDir);
            _controller.Move(moveDir * _moveSpeed * Time.deltaTime);
        }
        else
        {
            _controller.Move(Vector3.zero);
        }
    }
}