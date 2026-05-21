using UnityEngine;
using System.Collections;
using DG.Tweening;
using Unity.VisualScripting;

public class Ball : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rig;
    public SpriteRenderer sprite;
    public TrailRenderer trail;
    public GameObject explosion;
    
    
    [Header("Audios")]
    public AudioSource audioSource;
    public AudioClip explode;
    public AudioClip ballHit;
    public AudioClip gameOver;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        
        StartCoroutine(PlayBall());

       

    }

    

     void Launch()
    {
       audioSource.PlayOneShot(ballHit);

        Vector2 direction = Vector2.zero;

        // Randomly choose a direction for the ball to launch
        if (Random.value < 0.5f)
        {
            direction = Vector2.left;
        }
        else
        {
            direction= Vector2.right;
        }

        direction.y = Random.Range(-0.5f, 0.5f);

        rig.linearVelocity = direction * speed;
    }

    public void ResetBall()
    {
        PlayExplode();
       GameObject exp = Instantiate(explosion, transform.position, transform.rotation);
        Destroy(exp, 2f);
       trail.Clear();
       trail.enabled = false;
       rig.linearVelocity= Vector2.zero;
       transform.position = new Vector2(-0.01f, 0.25f);
       StartCoroutine(PlayBall());
       trail.enabled = true;
    }

    IEnumerator PlayBall()
    {
        sprite.enabled = true;
        yield return new WaitForSeconds(0.5f);
        sprite.enabled = false;
        yield return new WaitForSeconds(0.5f);
        sprite.enabled = true;
        yield return new WaitForSeconds(0.5f);
        sprite.enabled = false;
        yield return new WaitForSeconds(0.5f);
        sprite.enabled = true;
        yield return new WaitForSeconds(0.5f);

        Launch();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Paddle")
        {
            BallJuice();
        }
    }
        void BallJuice() // Play the ball hit sound effect and animate the ball sprite
    {
            audioSource.PlayOneShot(ballHit);

                       
        }
     void PlayExplode() // Play the explosion sound effect
    { 
        audioSource.PlayOneShot(explode);
        audioSource.PlayOneShot(gameOver);
    }
  
}
