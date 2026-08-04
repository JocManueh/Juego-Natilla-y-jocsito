using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Coin : MonoBehaviour
{
    [Header("Sonido")]
    public AudioClip collectSound;
    [Range(0f, 1f)] public float soundVolume = 1f;

    [Header("Visual (opcional)")]
    public bool rotate = true;
    public float rotateSpeed = 90f;

    void Reset()
    {
        // Asegura que el collider del prefab sea trigger por defecto
        Collider col = GetComponent<Collider>();
        if (col != null) col.isTrigger = true;
    }

    void Update()
    {
        if (rotate)
        {
            transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Collect();
        }
    }

    void Collect()
    {
        if (collectSound != null)
        {
            // Suena aunque el objeto se destruya inmediatamente después
            AudioSource.PlayClipAtPoint(collectSound, transform.position, soundVolume);
        }

        // Aquí puedes sumar puntaje si tu GameManager lo maneja, ej:
        // GameManager.Instance.AddCoin();

        Destroy(gameObject);
    }
}