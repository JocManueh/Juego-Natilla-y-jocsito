using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class LightningController : MonoBehaviour
{
    public float speed = 8f;
    private Rigidbody rb;
    private bool started = false;

    // Dirección inicial (izquierda)
    private Vector3 direction = Vector3.forward;
    private TrailRenderer trail;

    [Header("Sonidos")]
    public AudioClip soundStart;
    public AudioClip soundWallHit;
    public AudioClip soundGoal;
    [Range(0f, 1f)] public float soundVolume = 1f;
    private AudioSource audioSource;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        trail = GetComponentInChildren<TrailRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    public void ResetLightning()
    {
        started = false;
        direction = Vector3.forward;
        if (trail != null)
        {
            trail.emitting = false;
            trail.Clear();
            StartCoroutine(ResetTrail());
        }
    }

    private IEnumerator ResetTrail()
    {
        yield return null; // Espera un frame
        trail.Clear();
        trail.emitting = true;
    }

    void Update()
    {
        if (!enabled)
            return;

        if (!started)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                direction = Vector3.forward;
                started = true;
                PlaySound(soundStart);
                Debug.Log("¡Comenzó el laberinto!");
            }
            return;
        }

        // W
        if (Input.GetKeyDown(KeyCode.W) && direction != Vector3.up)
            direction = Vector3.down;
        // S
        if (Input.GetKeyDown(KeyCode.S) && direction != Vector3.down)
            direction = Vector3.up;
        // A
        if (Input.GetKeyDown(KeyCode.A) && direction != Vector3.back)
            direction = Vector3.forward;
        // D
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

    private void OnTriggerEnter(Collider other)
    {
        if (!enabled)
            return;

        // Chocó con una pared
        if (other.CompareTag("LightningWall"))
        {
            PlaySound(soundWallHit);
            Debug.Log("¡Perdiste!");
            GameManager.Instance.RestartLightning();
        }

        // Llegó a la salida
        if (other.CompareTag("LightningGoal"))
        {
            Debug.Log("¡Laberinto completado!");

            // El objeto con el tag LightningGoal debe tener un hijo
            // llamado ExitPoint que indica dónde reaparece el cilindro.
            Transform exitPoint = other.transform.Find("ExitPoint");
            if (exitPoint != null)
            {
                PlaySound(soundGoal);
                GameManager.Instance.EndLightningMode(exitPoint.position);
            }
            else
            {
                Debug.LogError("LightningGoal no tiene un hijo llamado ExitPoint.");
            }
        }
    }

    void PlaySound(AudioClip clip)
    {
        if (clip == null || audioSource == null) return;
        audioSource.PlayOneShot(clip, soundVolume);
    }
}