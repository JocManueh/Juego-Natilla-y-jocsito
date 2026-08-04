using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(AudioSource))]
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
    private AudioSource audioSource;

    [Header("Visual")]
    public Transform capsuleVisual;

    [Header("Sonidos")]
    public AudioClip soundNormal;
    public AudioClip soundThin;
    public AudioClip soundBall;
    [Range(0f, 1f)] public float soundVolume = 1f;

    public enum ShapeType { Normal, Thin, Ball }
    public ShapeType currentShape = ShapeType.Normal;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        audioSource = GetComponent<AudioSource>();
        ChangeToNormal(playSound: false); // al iniciar no suena
    }

    void Update()
    {
        if (GameManager.Instance.currentStage != GameManager.Stage.Shape) return;

        if (Input.GetKeyDown(KeyCode.I)) ChangeToThin();
        if (Input.GetKeyDown(KeyCode.O)) ChangeToBall();
        if (Input.GetKeyDown(KeyCode.P)) ChangeToNormal();
    }

    void FixedUpdate()
    {
        if (GameManager.Instance.currentStage != GameManager.Stage.Shape) return;

        float horizontal = 0f;
        float vertical = 0f;

        if (Input.GetKey(KeyCode.A)) horizontal = -1;
        if (Input.GetKey(KeyCode.D)) horizontal = 1;
        if (Input.GetKey(KeyCode.W)) vertical = 1;
        if (Input.GetKey(KeyCode.S)) vertical = -1;

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

    public void ResetShape(bool playSound = true)
    {
        ChangeToNormal(playSound);
    }

    void ChangeToThin(bool playSound = true)
    {
        currentShape = ShapeType.Thin;

        // Collider
        col.radius = 0.20f;
        col.height = 2.4f;

        // Apariencia
        capsuleVisual.localScale = new Vector3(0.4f, 1.2f, 0.4f);

        if (playSound) PlayShapeSound(soundThin);
    }

    void ChangeToBall(bool playSound = true)
    {
        currentShape = ShapeType.Ball;

        // Collider
        col.radius = 0.70f;
        col.height = 1.4f;

        // Apariencia
        capsuleVisual.localScale = new Vector3(1.4f, 0.7f, 1.4f);

        if (playSound) PlayShapeSound(soundBall);
    }

    void ChangeToNormal(bool playSound = true)
    {
        currentShape = ShapeType.Normal;

        // Collider
        col.radius = 0.50f;
        col.height = 2f;

        // Apariencia
        capsuleVisual.localScale = Vector3.one;

        if (playSound) PlayShapeSound(soundNormal);
    }

    void PlayShapeSound(AudioClip clip)
    {
        if (clip == null || audioSource == null) return;
        audioSource.PlayOneShot(clip, soundVolume);
    }
}