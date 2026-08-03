using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject player;

    private Vector3 checkpointPosition;

    public enum Stage
    {
        Normal,
        Flappy,
        Shape,
        Ship,
        Launch,
        Finish
    }

    public Stage currentStage = Stage.Normal;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        checkpointPosition = player.transform.position;
    }

    public void ChangeStage(Stage newStage)
    {
        currentStage = newStage;

        Debug.Log("Etapa actual: " + currentStage);

        
    }

    public void SetCheckpoint(Vector3 position)
    {
        checkpointPosition = position;
    }

    public void RespawnPlayer()
    {
        player.transform.position = checkpointPosition;

        Rigidbody rb = player.GetComponent<Rigidbody>();

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        ShapeController shape = player.GetComponent<ShapeController>();

        if (shape != null)
        {
            shape.ResetShape();
        }
    }
}