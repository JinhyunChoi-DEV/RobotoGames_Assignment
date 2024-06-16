using UnityEngine;

public class BounceComponent : MonoBehaviour
{
    public float strenght;

    private void OnCollisionEnter2D(Collision2D other)
    {
        var ball = other.gameObject.GetComponent<Ball>();

        if(ball == null)
            return;

        Vector2 dir = other.GetContact(0).normal;
        ball.AddForce(-dir, strenght);
    }
}
