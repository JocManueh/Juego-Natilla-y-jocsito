using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("GANASTE");

            GameManager.Instance.ChangeStage(GameManager.Stage.Finish);
        }
    }
}