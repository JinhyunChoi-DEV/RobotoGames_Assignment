using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private HowToPlayPanel howtoPlayPanel;

    [SerializeField] private Button start;
    [SerializeField] private Button howToPlay;
    [SerializeField] private Button quit;

    void Start()
    {
        howtoPlayPanel.Close();

        start.onClick.AddListener(StartGame);
        howToPlay.onClick.AddListener(HowToPlay);
        quit.onClick.AddListener(Quit);
    }

    void StartGame()
    {
        SceneManager.LoadScene("Pong");
    }

    void HowToPlay()
    {
        howtoPlayPanel.Open();
    }

    void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
