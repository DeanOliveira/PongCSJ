using UnityEngine;
using DG.Tweening;

public class PaddleJuice : MonoBehaviour
{
    public float bounceForce;
    public Transform paddleSprite;
    public void PlayHitEffect()
    {
        paddleSprite.transform.DOScale(1.3f, 0.1f).SetLoops(2, LoopType.Yoyo);
                          
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ball")
        {
            Rigidbody2D ballRig = other.gameObject.GetComponent<Rigidbody2D>();
            float yOffeset = other.transform.position.y - transform.position.y;
            float paddleHeight = GetComponent<Collider2D>().bounds.size.y;
            float normalizedY = yOffeset / (paddleHeight / 2f); // Normalize the offset to a value between -1 and 1 -1 se bater embaixo 0 se bater no centro e 1 se bater em cima

            Vector2 direction = new Vector2(Mathf.Sign(ballRig.linearVelocity.x), normalizedY).normalized; // Create a new direction vector based on the normalized offset
            ballRig.linearVelocity = direction * bounceForce;
        }
    }
}
