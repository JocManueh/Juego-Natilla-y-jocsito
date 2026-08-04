using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Obstacle : MonoBehaviour
{
    [Header("Sonido")]
    public AudioClip hitSound;
    [Range(0f, 1f)] public float soundVolume = 1f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ShapeController shape = collision.gameObject.GetComponent<ShapeController>();
            if (shape != null)
            {
                shape.ResetShape(false); // resetea forma SIN sonido
            }

            PlayHitSound();
            GameManager.Instance.RespawnPlayer();
        }
    }

    void PlayHitSound()
    {
        if (hitSound == null) return;
        AudioSource.PlayClipAtPoint(hitSound, transform.position, soundVolume);
    }
}