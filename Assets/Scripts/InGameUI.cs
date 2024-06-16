using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private TMP_Text playerScore;
    [SerializeField] private TMP_Text enemyScore;

    public void UpdatePlayerScore(int score)
    {
        playerScore.text = score.ToString();
    }

    public void UpdateEnemyScore(int score)
    {
        enemyScore.text = score.ToString();
    }
}
