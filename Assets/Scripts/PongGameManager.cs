using System.Collections;
using UnityEngine;

public enum MapMode
{
    Default, Blocking, Shaking
}

public class PongGameManager : MonoBehaviour
{
    [SerializeField] private PlayerRacket player;
    [SerializeField] private EnemyRacket enemy;
    [SerializeField] private Ball ball;
    [SerializeField] private InGameUI gameUI;
    [SerializeField] private SlotController slot;
    [SerializeField] private BlockWallControl ballControl;
    [SerializeField] private ScreenShakeControl screenShakeControl;
    [SerializeField] private SkillControl skillControl;
    [SerializeField] private EndPanel endPanel;

    private int playerScore = 0;
    private int enemyScore = 0;
    private int totalScore => playerScore + enemyScore;
    private const int MaxScore = 11;
    private MapMode mode = MapMode.Default;

    private bool runningModeSelecting = false;

    public void AddPlayerScore()
    {
        screenShakeControl.Set(false);

        playerScore++;
        gameUI.UpdatePlayerScore(playerScore);
        ResetPositions();

        if (IsEnd())
        {
            endPanel.Open(playerScore > enemyScore);
            return;
        }

        StartCoroutine(SetNextRoundWrapper());
    }

    public void AddEnemyScore()
    {
        screenShakeControl.Set(false);

        enemyScore++;
        gameUI.UpdateEnemyScore(enemyScore);
        ResetPositions();

        if (IsEnd())
        {
            endPanel.Open(playerScore > enemyScore);
            return;
        }

        StartCoroutine(SetNextRoundWrapper());
    }

    private void Start()
    {
        screenShakeControl.Set(false);
        skillControl.Set(false);
        endPanel.Close();

        slot.Close();
        playerScore = 0;
        enemyScore = 0;
        mode = MapMode.Default;
    }

    private void Update()
    {
        if (!runningModeSelecting && !skillControl.IsActive && !IsEnd())
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                skillControl.Set(true);
            }
        }

        enemy.CanSeeBall = ball.ballRenderer.color.a > 0.25;
    }

    void ResetPositions()
    {
        ball.ResetBall();
        player.ResetPlayer();
        enemy.ResetEnemy();
    }

    private IEnumerator SetNextRoundWrapper()
    {
        yield return new WaitForFixedUpdate();

        yield return StartCoroutine(SetNextRound());

        ball.AddBallForce();
    }

    IEnumerator SetNextRound()
    {
        runningModeSelecting = true;

        if (totalScore >= 5)
        {
            bool isCompleted = false;
            yield return StartCoroutine(slot.OpenSlot(result =>
            {
                mode = result;
            }, () =>
            {
                isCompleted = true;
            }));

            yield return new WaitUntil(() => isCompleted);

            ballControl.Set(mode == MapMode.Blocking);
            screenShakeControl.Set(mode == MapMode.Shaking);
        }

        runningModeSelecting = false;
    }

    bool IsEnd()
    {
        return playerScore >= MaxScore || enemyScore >= MaxScore;
    }
}
