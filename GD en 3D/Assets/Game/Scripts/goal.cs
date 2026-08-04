using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    public GameObject finishPanel;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        Debug.Log("GANASTE");

        finishPanel.SetActive(true);

        Time.timeScale = 0f;
    }
}