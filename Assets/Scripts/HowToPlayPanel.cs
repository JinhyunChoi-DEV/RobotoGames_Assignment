using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HowToPlayPanel : MonoBehaviour
{
    [SerializeField] private GameObject howToPlayPanel;
    [SerializeField] private GameObject mainMenu;

    [SerializeField] private GameObject description;
    [SerializeField] private GameObject key;
    [SerializeField] private GameObject skill;

    [SerializeField] private Button quitButton;
    [SerializeField] private Button nextButton;
    [SerializeField] private TMP_Text indexPage;

    private int index = 1;

    public void Open()
    {
        mainMenu.SetActive(false);
        howToPlayPanel.gameObject.SetActive(true);

        index = 1;
        description.SetActive(true);
        key.SetActive(false);
        skill.SetActive(false);
    }

    public void Close()
    {
        mainMenu.SetActive(true);
        howToPlayPanel.gameObject.SetActive(false);
    }

    void Start()
    {
        quitButton.onClick.AddListener(Close);
        nextButton.onClick.AddListener(SetNextPage);
    }

    void Update()
    {
        indexPage.text = string.Format("{0}/3", index);
    }

    void SetNextPage()
    {
        if (index >= 3)
            return;

        index++;

        description.SetActive(index == 1);
        key.SetActive(index == 2);
        skill.SetActive(index == 3);
    }

}
