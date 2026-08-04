using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class LightningController : MonoBehaviour
{
    public float speed = 8f;

    private Rigidbody rb;

    private bool started = false;

    private Vector3 direction = Vector3.up;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (GameManager.Instance.currentStage != GameManager.Stage.Lightning)
            return;

        // Esperar ENTER
        if (!started)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                started = true;
                Debug.Log("¡Comenzó el laberinto!");
            }

            return;
        }

        // Cambiar dirección (sin reversa)

        if (Input.GetKeyDown(KeyCode.W) && direction != Vector3.down)
            direction = Vector3.up;

        if (Input.GetKeyDown(KeyCode.S) && direction != Vector3.up)
            direction = Vector3.down;

        if (Input.GetKeyDown(KeyCode.A) && direction != Vector3.forward)
            direction = Vector3.back;

        if (Input.GetKeyDown(KeyCode.D) && direction != Vector3.back)
            direction = Vector3.forward;
    }

    void FixedUpdate()
    {
        void FixedUpdate()
        {
            if (GameManager.Instance.currentStage != GameManager.Stage.Lightning)
                return;

            transform.position += Vector3.up * speed * Time.fixedDeltaTime;
        }
        if (!started)
        {
            rb.linearVelocity = Vector3.zero;
            return;
        }
        Debug.Log(direction);
        rb.linearVelocity = direction * speed;
    }
    public void ResetLightning()
    {
        started = false;
        direction = Vector3.up;
    }
}