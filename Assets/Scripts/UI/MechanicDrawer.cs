using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class MechanicDrawer : MonoBehaviour
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
        _nameInputField.gameObject.SetActive(false);
        _nameText.text = text;
        _bossMechanic.Name = text;
    }

    private void OnTimeInputFieldEndEdit(string text)
    {
        var isNumber = float.TryParse(text, out float time);

        if (isNumber)
        {
            _timeInputField.gameObject.SetActive(false);
            _timeText.text = "Time: " + text;
            _bossMechanic.Time = time;
        }
    }
}
