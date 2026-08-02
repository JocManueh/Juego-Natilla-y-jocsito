using UnityEngine;

public class StageTrigger : MonoBehaviour
{
    public GameManager.Stage nextStage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.ChangeStage(nextStage);
        }
    }
}