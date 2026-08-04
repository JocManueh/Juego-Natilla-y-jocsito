using UnityEngine;

public class LightningTrigger : MonoBehaviour
{
    public Transform mazeSpawn;

    private bool playerInside = false;

    private void Update()
    {
        if (playerInside && Input.GetKeyDown(KeyCode.E))
        {
            GameManager.Instance.StartLightningMode(mazeSpawn.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            Debug.Log("Presiona E");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            Debug.Log("Salió del trigger");
        }
    }
}