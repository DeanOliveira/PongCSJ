using JetBrains.Annotations;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPaddle : MonoBehaviour
{
    public float speed;
    private Vector2 directionInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * directionInput * speed * Time.deltaTime);
    }
    public void OnMove(InputValue value)

    {
        directionInput = value.Get<Vector2>();
    }
}
