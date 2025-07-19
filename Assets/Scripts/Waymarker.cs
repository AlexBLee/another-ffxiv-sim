using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Waymarker : MonoBehaviour
{
    private enum WaymarkerType
    {
        Square,
        Circle
    }

    [SerializeField] private TextMeshPro _text;

    [SerializeField] private Renderer _circleObject;
    [SerializeField] private Renderer _squareObject;

    private Camera _camera;

    private readonly Dictionary<string, (WaymarkerType, Color)> _waymarkerDetails = new()
    {
        {"A", new ValueTuple<WaymarkerType, Color>(WaymarkerType.Circle, Color.red)},
        {"B", new ValueTuple<WaymarkerType, Color>(WaymarkerType.Circle, Color.yellow)},
        {"C", new ValueTuple<WaymarkerType, Color>(WaymarkerType.Circle, Color.cyan)},
        {"D", new ValueTuple<WaymarkerType, Color>(WaymarkerType.Circle, Color.purple)},
        {"1", new ValueTuple<WaymarkerType, Color>(WaymarkerType.Square, Color.red)},
        {"2", new ValueTuple<WaymarkerType, Color>(WaymarkerType.Square, Color.yellow)},
        {"3", new ValueTuple<WaymarkerType, Color>(WaymarkerType.Square, Color.cyan)},
        {"4", new ValueTuple<WaymarkerType, Color>(WaymarkerType.Square, Color.purple)},
    };

    private void Start()
    {
        _camera = Camera.main;
    }

    public void SetData(string key, WaymarkerData data)
    {
        var xOffset = 100;
        var zOffset = 110;

        Vector3 waymarkerPosition = new Vector3(data.X - xOffset, data.Y, data.Z - zOffset);
        transform.localPosition = waymarkerPosition;

        _text.text = key;

        SetShapeAndColor();
    }

    private void SetShapeAndColor()
    {
        var wayMarkerDetail = _waymarkerDetails[_text.text];

        if (wayMarkerDetail.Item1 == WaymarkerType.Circle)
        {
            _circleObject.gameObject.SetActive(true);
            _squareObject.gameObject.SetActive(false);

            _circleObject.material.color = wayMarkerDetail.Item2;
        }
        else
        {
            _circleObject.gameObject.SetActive(false);
            _squareObject.gameObject.SetActive(true);

            _squareObject.material.color = wayMarkerDetail.Item2;
        }

        _text.color = wayMarkerDetail.Item2;
    }

    private void Update()
    {
        _text.transform.LookAt(_camera.transform);
        _text.transform.rotation = Quaternion.LookRotation(_camera.transform.forward);
    }
}
