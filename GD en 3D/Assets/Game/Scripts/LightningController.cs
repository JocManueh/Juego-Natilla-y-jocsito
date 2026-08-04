using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class LightningController : MonoBehaviour
{
    public float speed = 8f;

    private Rigidbody rb;

    private bool started = false;

    // Plano YZ
    private Vector3 direction = Vector3.up;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void ResetLightning()
    {
        started = false;
        direction = Vector3.up;
    }

    void Update()
    {
        if (!enabled)
            return;

        if (!started)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                started = true;
                Debug.Log("¡Comenzó el laberinto!");
            }

            return;
        }

        // W = arriba en tu laberinto
        if (Input.GetKeyDown(KeyCode.W) && direction != Vector3.up)
            direction = Vector3.down;

        // S = abajo en tu laberinto
        if (Input.GetKeyDown(KeyCode.S) && direction != Vector3.down)
            direction = Vector3.up;

        // A = Z+
        if (Input.GetKeyDown(KeyCode.A) && direction != Vector3.back)
            direction = Vector3.forward;

        // D = Z-
        if (Input.GetKeyDown(KeyCode.D) && direction != Vector3.forward)
            direction = Vector3.back;
    }

    void FixedUpdate()
    {
        if (!enabled)
            return;

        if (!started)
        {
            rb.linearVelocity = Vector3.zero;
            return;
        }

        rb.linearVelocity = direction * speed;
    }
}