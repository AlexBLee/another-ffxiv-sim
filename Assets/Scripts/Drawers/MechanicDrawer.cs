using TMPro;
using UnityEngine;

public class MechanicDrawer : Drawer
{
    [SerializeField] private TMP_InputField _nameInputField;
    [SerializeField] private TMP_InputField _timeInputField;

    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _timeText;

    private BossMechanic _bossMechanic;

    private void OnEnable()
    {
        _nameInputField.onEndEdit.AddListener(OnNameInputFieldEndEdit);
        _timeInputField.onEndEdit.AddListener(OnTimeInputFieldEndEdit);
    }

    private void OnDisable()
    {
        _nameInputField.onEndEdit.RemoveListener(OnNameInputFieldEndEdit);
        _timeInputField.onEndEdit.AddListener(OnTimeInputFieldEndEdit);
    }

    public void SetBossMechanic(BossMechanic bossMechanic)
    {
        _bossMechanic = bossMechanic;
    }

    private void OnNameInputFieldEndEdit(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return;
        }

        _nameInputField.gameObject.SetActive(false);
        _nameText.text = text;
        _bossMechanic.Name = text;
    }

    private void OnTimeInputFieldEndEdit(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return;
        }

        var isNumber = float.TryParse(text, out float time);

        if (isNumber)
        {
            _timeInputField.gameObject.SetActive(false);
            _timeText.text = "Time: " + text;
            _bossMechanic.Time = time;
        }
    }
}
