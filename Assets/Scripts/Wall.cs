using UnityEngine;

public class Wall : MonoBehaviour
    {
    public GameManager gameManager;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ball")
        {
           if (CompareTag("LeftWall"))
           {
               gameManager.AIPoint();
           }
            if (CompareTag("RightWall"))
           {
               gameManager.PlayerPoint();
           }
            
            other.gameObject.GetComponent<Ball>().ResetBall();
        }
    }
}
