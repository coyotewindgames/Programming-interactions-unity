using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public static Menu Instance { get; private set; }
    public bool isShowingMenu = false;
    public CanvasGroup menuCanvasGroup;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        menuCanvasGroup = menuCanvasGroup.GetComponent<CanvasGroup>();
    }


    public void LoadLevel(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
            isShowingMenu = false;
            menuCanvasGroup.alpha = 0;
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    public void ToggleMenu()
    {
        if (isShowingMenu)
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            isShowingMenu = false;
            menuCanvasGroup.alpha = 0;
            return;
        }
        menuCanvasGroup.alpha = 1;
        Time.timeScale = 0;
        isShowingMenu = true;
        Cursor.lockState = CursorLockMode.None;
    }

}
