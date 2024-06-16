using UnityEngine;

public class EnemyRacket : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Ball ball;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] SpriteRenderer enemyRenderer;

    public bool CanMove = true;
    public bool CanSeeBall = true;

    public void ResetEnemy()
    {
        gameObject.transform.position = new Vector2(rb.position.x, 0);
        rb.velocity = Vector2.zero;
        CanMove = true;
    }

    void Update()
    {
        if(CanMove)
            enemyRenderer.color = Color.white;
        else
            enemyRenderer.color = Color.gray;
    }

    void FixedUpdate()
    {
        if (CanMove && CanSeeBall)
        {
            float ball_y_pos = ball.transform.position.y;
            float y_pos = this.transform.position.y;

            if (ball.GetVelocity().x < 0.0f) // Going to Player Racket Or Invisible
            {
                if (y_pos > 0.0f)
                    rb.AddForce(Vector2.down * moveSpeed);
                else if (y_pos < 0.0f)
                    rb.AddForce(Vector2.up * moveSpeed);
            }
            else // Comes to Enemy Racket
            {
                if (ball_y_pos > y_pos)
                    rb.AddForce(Vector2.up * moveSpeed);
                else if (ball_y_pos < y_pos)
                    rb.AddForce(Vector2.down * moveSpeed);
            }
        }
        
    }
}
