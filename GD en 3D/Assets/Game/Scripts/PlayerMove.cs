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
        if (GameManager.Instance.currentStage != GameManager.Stage.Normal)
            return;
        float vertical = Input.GetAxis("Vertical");

        Vector3 velocity = rb.linearVelocity;

        velocity.z = vertical * moveSpeed;

        rb.linearVelocity = velocity;
    }
}