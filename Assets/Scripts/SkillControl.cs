using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SkillControl : MonoBehaviour
{
    [SerializeField] private Ball ball;
    [SerializeField] private PlayerRacket player;
    [SerializeField] private EnemyRacket enemy;

    [SerializeField] private GameObject panel;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button invisibleButton;
    [SerializeField] private Button blockButton;
    [SerializeField] private Button speedButton;

    public bool IsActive => panel.gameObject.activeSelf;

    private bool canUseInvisible = true;
    private bool canUseBlock = true;
    private bool canUseSpeed = true;

    private bool isInputLocked = false;
    private float inputCooldown = 0.1f;

    public void Set(bool active)
    {
        if (isInputLocked) return;

        panel.SetActive(active);
        Time.timeScale = active ? 0.0f : 1.0f;

        if (active)
        {
            StartCoroutine(LockInputTemporarily());
        }

        invisibleButton.gameObject.SetActive(canUseInvisible);
        blockButton.gameObject.SetActive(canUseBlock);
        speedButton.gameObject.SetActive(canUseSpeed);
    }

    private void Start()
    {
        invisibleButton.onClick.AddListener(SetInvisibleSkill);
        blockButton.onClick.AddListener(SetBlockSkill);
        speedButton.onClick.AddListener(SetSpeedSkill);
        closeButton.onClick.AddListener(() => Set(false));
    }

    private void Update()
    {
        if (panel.gameObject.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                Set(false);
            }
        }
    }

    private IEnumerator LockInputTemporarily()
    {
        isInputLocked = true;
        yield return new WaitForSecondsRealtime(inputCooldown);
        isInputLocked = false;
    }

    void SetInvisibleSkill()
    {
        canUseInvisible = false;
        ball.Invisible = true;
        Set(false);
    }

    void SetBlockSkill()
    {
        canUseBlock = false;
        enemy.CanMove = false;
        Set(false);
    }

    void SetSpeedSkill()
    {
        canUseSpeed = false;
        player.playerBounce.strenght *= 2.0f;
        Set(false);
    }
}
