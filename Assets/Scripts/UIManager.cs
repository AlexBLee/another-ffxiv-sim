using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI _hitText;
    [SerializeField] private Slider _progressBar;
    [SerializeField] private TextMeshProUGUI _mechName;

    [SerializeField] private Button _startButton;

    private float _currentTime = 0f;
    private float _maxTime = 0f;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (_maxTime > 0)
        {
            _currentTime += Time.deltaTime;
            _progressBar.value = _currentTime / _maxTime;
        }
    }

    public void ShowProgressBar(BossTimelineRenderer.ScheduledAction scheduledAction)
    {
        if (string.IsNullOrEmpty(scheduledAction.Name))
        {
            _progressBar.gameObject.SetActive(false);
            _maxTime = -1f;
            return;
        }

        _progressBar.gameObject.SetActive(true);
        _maxTime = scheduledAction.GlobalTime;
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
