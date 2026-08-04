using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class FlappyController : MonoBehaviour
{
    public float forwardSpeed = 6f;
    public float jumpForce = 7f;

    [Header("Jump Sound")]
    public AudioClip[] jumpClips;       // uno o varios clips para variar
    [Range(0f, 1f)] public float jumpVolume = 0.8f;

    private Rigidbody rb;
    private AudioSource audioSource;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void FixedUpdate()
    {
        if (GameManager.Instance.currentStage != GameManager.Stage.Flappy)
            return;

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

            PlayJumpSound();
        }
    }

    void PlayJumpSound()
    {
        if (jumpClips.Length == 0) return;
        int index = Random.Range(0, jumpClips.Length);
        audioSource.PlayOneShot(jumpClips[index], jumpVolume);
    }
}