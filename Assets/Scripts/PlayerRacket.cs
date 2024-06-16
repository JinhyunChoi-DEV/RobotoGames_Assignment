using UnityEngine;

public class PlayerRacket : MonoBehaviour
{
    public float speed;
    public BounceComponent playerBounce;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] SpriteRenderer playerRenderer;

    private Vector2 dir;
    private float originalBounce;

    public void ResetPlayer()
    {
        gameObject.transform.position = new Vector2(rb.position.x, 0);
        rb.velocity = Vector2.zero;
        playerBounce.strenght = originalBounce;
    }

    void Start()
    {
        originalBounce = playerBounce.strenght;
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            dir = Vector2.up;
        else if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            dir = Vector2.down;
        else
            dir = Vector2.zero;

        if (playerBounce.strenght > originalBounce)
            playerRenderer.color = Color.yellow;
        else
            playerRenderer.color = Color.white;
    }

    void FixedUpdate()
    {
        rb.AddForce(dir * speed);
    }
}
