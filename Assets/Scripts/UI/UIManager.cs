using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private GameplayUI _gameplayUI;
    [SerializeField] private EditorUI _editorUI;

    public bool _inEditorMode;

    public GameplayUI GameplayUI => _gameplayUI;
    public EditorUI EditorUI => _editorUI;

    public bool InEditorMode => _inEditorMode;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        _inEditorMode = true;
    }

    public void ShowGameplayUI()
    {
        _gameplayUI.gameObject.SetActive(true);
        _editorUI.gameObject.SetActive(false);

        _editorUI.BossTimelineEditor.ResetActionDrawerHandles();
        _inEditorMode = false;
    }

    public void ShowEditorUI()
    {
        _editorUI.gameObject.SetActive(true);
        _gameplayUI.gameObject.SetActive(false);
        _inEditorMode = true;
    }
}