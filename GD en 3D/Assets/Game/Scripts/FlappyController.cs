using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FlappyController : MonoBehaviour
{
    public float forwardSpeed = 6f;
    public float jumpForce = 7f;

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (GameManager.Instance.currentStage != GameManager.Stage.Flappy)
            return;

        // Mantener avance constante
        Vector3 velocity = rb.linearVelocity;
        velocity.z = forwardSpeed;
        rb.linearVelocity = velocity;
    }

    void Update()
    {
        if (GameManager.Instance.currentStage != GameManager.Stage.Flappy)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 velocity = rb.linearVelocity;
            velocity.y = jumpForce;
            rb.linearVelocity = velocity;
        }
    }
}