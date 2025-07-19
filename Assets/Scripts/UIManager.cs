using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI _hitText;
    [SerializeField] private Button _startButton;

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
