using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 6f;

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {

        Debug.Log(GameManager.Instance.currentStage);
        if (GameManager.Instance.currentStage != GameManager.Stage.Normal)
            return;

        float horizontal = Input.GetAxis("Horizontal"); // A y D
        float vertical = Input.GetAxis("Vertical");     // W y S

        Vector3 velocity = rb.linearVelocity;

        velocity.x = horizontal * moveSpeed;
        velocity.z = vertical * moveSpeed;

        rb.linearVelocity = velocity;
    }
}