using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _hitText;
    [SerializeField] private Slider _progressBar;
    [SerializeField] private TextMeshProUGUI _mechName;
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _editorButton;

    private float _currentTime = 0f;
    private float _maxTime = 0f;

    private void Update()
    {
        if (_maxTime > 0 && _currentTime < _maxTime)
        {
            _currentTime += Time.deltaTime;
            _progressBar.value = _currentTime / _maxTime;
        }
        else
        {
            _progressBar.gameObject.SetActive(false);
            _maxTime = -1f;
        }
    }

    public void ShowProgressBar(BossTimelineRenderer.ScheduledAction scheduledAction)
    {
        if (string.IsNullOrEmpty(scheduledAction.Name))
        {
            return;
        }

        _progressBar.gameObject.SetActive(true);
        _currentTime = 0f;
        _maxTime = scheduledAction.ActualTime;
        _mechName.text = scheduledAction.Name;
    }

    public void SetStartButtonInteractiveState(bool state)
    {
        _startButton.interactable = state;
    }

    public void ShowHitText()
    {
        _hitText.gameObject.SetActive(true);
        _hitText.text += "Player was hit!" + '\n';
    }

    public void HideHitText()
    {
        _hitText.gameObject.SetActive(false);
        _hitText.text = "";
    }
}
