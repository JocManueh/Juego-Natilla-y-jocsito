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
        Finish,
        Lightning // <-- Añadido para corregir el error CS0117
    }
    public GameObject capsuleVisual;
    public GameObject sphereVisual;

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

        if (currentStage == Stage.Normal)
        {
            ShapeController shape = player.GetComponent<ShapeController>();

            if (shape != null)
            {
                shape.ResetShape();
            }
        }

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
    public void StartLightningMode(Vector3 spawnPosition)
    {
        ChangeStage(Stage.Lightning);

        player.transform.position = spawnPosition;

        capsuleVisual.SetActive(false);
        sphereVisual.SetActive(true);

        Rigidbody rb = player.GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        Debug.Log("Modo Lightning");
    }

}