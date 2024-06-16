using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndPanel : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text winnerText;
    [SerializeField] private Button goToMenu;
    [SerializeField] private Button quit;

    public void Open(bool isPlayer)
    {
        winnerText.text = isPlayer ? "Player" : "Enemy";
        panel.gameObject.SetActive(true);
    }

    public void Close()
    {
        winnerText.text = "???";
        panel.gameObject.SetActive(false);
    }

    private void Start()
    {
        goToMenu.onClick.AddListener(GoToMenu);
        quit.onClick.AddListener(Quit);
    }

    private void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
