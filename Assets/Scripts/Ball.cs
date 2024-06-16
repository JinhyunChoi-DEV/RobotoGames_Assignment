using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float minXSpeed = 1.0f; // 최소 x 속도
    public SpriteRenderer ballRenderer;
    public float fadeSpeed = 1.0f;

    public bool Invisible = false;

    public Vector2 GetVelocity()
    {
        return rb.velocity;
    }

    public void AddForce(Vector2 dir, float strenght)
    {
        Vector2 force = dir.normalized * strenght;
        rb.AddForce(force, ForceMode2D.Impulse);
    }

    public void ResetBall()
    {
        rb.position = Vector3.zero;
        rb.velocity = Vector3.zero;
        Invisible = false;
        Color current = ballRenderer.color;
        current.a = 1.0f;
        ballRenderer.color = current;
    }

    public void AddBallForce()
    {
        float x = Random.value < 0.5f ? -1.0f : 1.0f;
        float y = Random.Range(-1.0f, 1.0f);

        Vector2 force = new Vector2(x, y).normalized * speed;

        rb.AddForce(force, ForceMode2D.Impulse);
    }

    void Start()
    {
        AddBallForce();
    }

    void Update()
    {
        if (Invisible)
        {
            float alpha = Mathf.PingPong(Time.time * fadeSpeed, 1.0f);
            Color current = ballRenderer.color;
            current.a = alpha;
            ballRenderer.color = current;
        }
    }

    void FixedUpdate()
    {
        if (rb.velocity != Vector2.zero)
        {
            Vector2 velocity = rb.velocity;
            if (Mathf.Abs(velocity.x) < minXSpeed)
            {
                velocity.x = minXSpeed * Mathf.Sign(velocity.x); // 최소 속도로 조정
                rb.velocity = velocity;
            }
        }
        
    }
}