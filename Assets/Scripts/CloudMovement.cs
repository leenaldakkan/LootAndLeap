using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    public float speed = 0.5f;

    public float resetPosition = 30f;
    public float startPosition = -30f;

    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        if (transform.position.x > resetPosition)
        {
            transform.position = new Vector3(
                startPosition,
                transform.position.y,
                transform.position.z
            );
        }
    }
}
