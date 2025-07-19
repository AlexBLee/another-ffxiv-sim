using System;
using TMPro;
using UnityEngine;

public class Waymarker : MonoBehaviour
{
    [SerializeField] private TextMeshPro _text;

    [SerializeField] private GameObject _circleObject;
    [SerializeField] private GameObject _squareObject;

    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    public void SetData(string name, WaymarkerData data)
    {
        var xOffset = 100;
        var zOffset = 110;

        Vector3 waymarkerPosition = new Vector3(data.X - xOffset, data.Y, data.Z - zOffset);
        transform.localPosition = waymarkerPosition;

        _text.text = name;
    }

    private void Update()
    {
        _text.transform.LookAt(_camera.transform);
        _text.transform.rotation = Quaternion.LookRotation(_camera.transform.forward);
    }
}
