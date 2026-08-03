using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class ShapeController : MonoBehaviour
{
    [Header("Movimiento")]
    public float forwardSpeed = 7f;
    public float moveSpeed = 6f;

    [Header("Límites")]
    public float limitX = 4f;
    public float limitY = 2.5f;

    private Rigidbody rb;
    private CapsuleCollider col;
    [Header("Visual")]
    public Transform capsuleVisual;

    public enum ShapeType
    {
        Normal,
        Thin,
        Ball
    }

    public ShapeType currentShape = ShapeType.Normal;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();

        ChangeToNormal();
    }

    void Update()
    {
        if (GameManager.Instance.currentStage != GameManager.Stage.Shape)
            return;

        if (Input.GetKeyDown(KeyCode.I))
            ChangeToThin();

        if (Input.GetKeyDown(KeyCode.O))
            ChangeToBall();

        if (Input.GetKeyDown(KeyCode.P))
            ChangeToNormal();
    }

    void FixedUpdate()
    {
        if (GameManager.Instance.currentStage != GameManager.Stage.Shape)
            return;

        float horizontal = 0f;
        float vertical = 0f;

        if (Input.GetKey(KeyCode.A))
            horizontal = -1;

        if (Input.GetKey(KeyCode.D))
            horizontal = 1;

        if (Input.GetKey(KeyCode.W))
            vertical = 1;

        if (Input.GetKey(KeyCode.S))
            vertical = -1;

        Vector3 velocity = new Vector3(
            horizontal * moveSpeed,
            vertical * moveSpeed,
            forwardSpeed);

        rb.linearVelocity = velocity;

        Vector3 pos = rb.position;

        pos.x = Mathf.Clamp(pos.x, -limitX, limitX);
        pos.y = Mathf.Clamp(pos.y, -limitY, limitY);

        rb.position = pos;
    }
    public void ResetShape()
    {
        ChangeToNormal();
    }
    void ChangeToThin()
    {
        currentShape = ShapeType.Thin;

        // Collider
        col.radius = 0.20f;
        col.height = 2.4f;

        // Apariencia
        capsuleVisual.localScale = new Vector3(0.4f, 1.2f, 0.4f);
        Debug.Log(capsuleVisual.name);
    }

    void ChangeToBall()
    {
        currentShape = ShapeType.Ball;

        // Collider
        col.radius = 0.70f;
        col.height = 1.4f;

        // Apariencia
        capsuleVisual.localScale =
            new Vector3(1.4f, 0.7f, 1.4f);
    }

    void ChangeToNormal()
    {
        currentShape = ShapeType.Normal;

        // Collider
        col.radius = 0.50f;
        col.height = 2f;

        // Apariencia
        capsuleVisual.localScale =
            Vector3.one;
    }
}