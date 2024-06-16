using UnityEngine;

public class ScoreWall : MonoBehaviour
{
    [SerializeField] PongGameManager gameManager;

    private void OnCollisionEnter2D(Collision2D other)
    {
        var ball = other.gameObject.GetComponent<Ball>();

        if (ball == null)
            return;

        if (this.gameObject.tag == "PlayerSideWall")
            gameManager.AddEnemyScore();
        else if (this.gameObject.tag == "EnemySideWall")
            gameManager.AddPlayerScore();
        
    }
}
