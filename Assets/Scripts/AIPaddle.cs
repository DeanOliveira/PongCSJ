using UnityEngine;

public class AIPaddle : MonoBehaviour
{
    private Transform ball;
    public float speed = 10f;

    void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Ball").transform;
    }
    // Update is called once per frame
    void Update()
    {
        if (ball.position.y > transform.position.y)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
        else if (ball.position.y < transform.position.y)
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
        }
        
    }
}
