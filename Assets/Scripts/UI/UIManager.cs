using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private GameplayUI _gameplayUI;
    [SerializeField] private EditorUI _editorUI;

    public GameplayUI GameplayUI => _gameplayUI;
    public EditorUI EditorUI => _editorUI;

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

    public void ShowGameplayUI()
    {
        _gameplayUI.gameObject.SetActive(true);
        _editorUI.gameObject.SetActive(false);
    }

    public void ShowEditorUI()
    {
        _editorUI.gameObject.SetActive(true);
        _gameplayUI.gameObject.SetActive(false);
    }
}
