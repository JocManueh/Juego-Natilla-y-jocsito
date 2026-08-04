using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 6f;

    [Header("Footsteps")]
    public AudioClip[] footstepClips;   // arrastra varios clips para variar el sonido
    public float stepInterval = 0.4f;   // tiempo entre pasos
    [Range(0f, 1f)] public float stepVolume = 0.6f;

    private Rigidbody rb;
    private AudioSource audioSource;
    private float stepTimer;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void FixedUpdate()
    {
        if (GameManager.Instance.currentStage != GameManager.Stage.Normal)
            return;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 velocity = rb.linearVelocity;
        velocity.x = horizontal * moveSpeed;
        velocity.z = vertical * moveSpeed;
        rb.linearVelocity = velocity;

        // Detecta si el jugador se está moviendo
        bool isMoving = new Vector2(horizontal, vertical).sqrMagnitude > 0.01f;

        HandleFootsteps(isMoving);
    }

    void HandleFootsteps(bool isMoving)
    {
        if (!isMoving || footstepClips.Length == 0)
        {
            stepTimer = 0f; // reinicia el timer cuando se detiene
            return;
        }

        stepTimer += Time.fixedDeltaTime;

        if (stepTimer >= stepInterval)
        {
            stepTimer = 0f;
            PlayFootstep();
        }
    }

    void PlayFootstep()
    {
        int index = Random.Range(0, footstepClips.Length);
        audioSource.PlayOneShot(footstepClips[index], stepVolume);
    }
}