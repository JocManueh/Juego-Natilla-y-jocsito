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
    public CapsuleCollider capsuleCollider;
    public SphereCollider sphereCollider;

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

        currentStage = Stage.Normal;

        Debug.Log("Etapa inicial: " + currentStage);
    }

    public void ChangeStage(Stage newStage)
    {
        currentStage = newStage;

        // Activar solo el controlador correspondiente
        PlayerController playerController = player.GetComponent<PlayerController>();
        FlappyController flappy = player.GetComponent<FlappyController>();
        ShapeController shape = player.GetComponent<ShapeController>();
        LightningController lightning = player.GetComponent<LightningController>();

        if (playerController != null)
            playerController.enabled = (newStage == Stage.Normal);

        if (flappy != null)
            flappy.enabled = (newStage == Stage.Flappy);

        if (shape != null)
            shape.enabled = (newStage == Stage.Shape);

        if (lightning != null)
            lightning.enabled = (newStage == Stage.Lightning);

        if (newStage == Stage.Normal && shape != null)
        {
            shape.ResetShape();
            capsuleVisual.SetActive(true);
            sphereVisual.SetActive(false);

            capsuleCollider.enabled = true;
            sphereCollider.enabled = false;
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

        capsuleCollider.enabled = false;
        sphereCollider.enabled = true;

        Rigidbody rb = player.GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        LightningController lightning = player.GetComponent<LightningController>();

        if (lightning != null)
        {
            lightning.ResetLightning();
        }

        Debug.Log("Modo Lightning");
    }

}