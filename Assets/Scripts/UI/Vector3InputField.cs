using System;
using System.Globalization;
using TMPro;
using UnityEngine;

public class Vector3InputField : MonoBehaviour
{
    [SerializeField] private TMP_InputField _xInputField;
    [SerializeField] private TMP_InputField _yInputField;
    [SerializeField] private TMP_InputField _zInputField;

    public event Action<Vector3> OnValueChangedCallback;

    private void Start()
    {
        _xInputField.onEndEdit.AddListener(OnValueChanged);
        _yInputField.onEndEdit.AddListener(OnValueChanged);
        _zInputField.onEndEdit.AddListener(OnValueChanged);
    }

    public Vector3 GetValue()
    {
        float x = float.TryParse(_xInputField.text, out var xResult) ? xResult : 0f;
        float y = float.TryParse(_yInputField.text, out var yResult) ? yResult : 0f;
        float z = float.TryParse(_zInputField.text, out var zResult) ? zResult : 0f;
        return new Vector3(x, y, z);
    }

    public void SetValue(Vector3 value)
    {
        if (value == Vector3.zero)
        {
            value = Vector3.one;
        }

        _xInputField.text = value.x.ToString(CultureInfo.InvariantCulture);
        _yInputField.text = value.y.ToString(CultureInfo.InvariantCulture);
        _zInputField.text = value.z.ToString(CultureInfo.InvariantCulture);
    }

    private void OnValueChanged(string arg)
    {
        OnValueChangedCallback?.Invoke(GetValue());
    }
}
