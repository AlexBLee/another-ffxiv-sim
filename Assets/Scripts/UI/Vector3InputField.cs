using System;
using TMPro;
using UnityEngine;

public class Vector3InputField : MonoBehaviour
{
    [SerializeField] private TMP_InputField _xInputField;
    [SerializeField] private TMP_InputField _yInputField;
    [SerializeField] private TMP_InputField _zInputField;

    public event Action<Vector3> OnValueChangedCallback;

    public Vector3 GetValue()
    {
        float x = float.TryParse(_xInputField.text, out var xResult) ? xResult : 0f;
        float y = float.TryParse(_yInputField.text, out var yResult) ? yResult : 0f;
        float z = float.TryParse(_zInputField.text, out var zResult) ? zResult : 0f;
        return new Vector3(x, y, z);
    }

    private void Start()
    {
        _xInputField.onEndEdit.AddListener(OnValueChanged);
        _yInputField.onEndEdit.AddListener(OnValueChanged);
        _zInputField.onEndEdit.AddListener(OnValueChanged);
    }

    private void OnValueChanged(string arg)
    {
        OnValueChangedCallback?.Invoke(GetValue());
    }
}
