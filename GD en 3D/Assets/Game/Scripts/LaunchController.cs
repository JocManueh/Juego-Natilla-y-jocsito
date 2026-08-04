using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class LaunchController : MonoBehaviour
{
    [Header("UI")]
    public GameObject launchUI;

    [Header("Camera")]
    public CameraManager cameraManager;
    public Camera mainCamera;
    public Transform launchCameraPoint;
    public Transform launchTarget;

    [Header("Launch")]
    public Transform launchSpawn;
    public CrosshairController crosshair;
    public float launchForce = 15f;

    private Rigidbody rb;
    private bool initialized = false;
    private bool launched = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (GameManager.Instance.currentStage != GameManager.Stage.Launch)
            return;

        if (initialized)
            return;

        initialized = true;

        // Detener completamente al jugador
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Llevar al jugador al punto de lanzamiento
        transform.position = launchSpawn.position;
        transform.rotation = launchSpawn.rotation;

        // Guardar checkpoint
        GameManager.Instance.SetCheckpoint(launchSpawn.position);

        // Desactivar seguimiento de cámara
        cameraManager.enabled = false;

        // Colocar cámara
        mainCamera.transform.position = launchCameraPoint.position;
        mainCamera.transform.rotation = launchCameraPoint.rotation;
        mainCamera.transform.LookAt(launchTarget);

        // Mostrar interfaz
        launchUI.SetActive(true);

        Debug.Log("Launch iniciado");
    }

    public void Launch()
    {
        if (launched)
            return;

        launched = true;

        float x = crosshair.normalizedX;
        float y = crosshair.normalizedY;

        // Dirección basada en la orientación de la cámara
        Vector3 direction =
            mainCamera.transform.forward +
            mainCamera.transform.right * x +
            mainCamera.transform.up * y;

        direction.Normalize();

        // Ocultar interfaz
        launchUI.SetActive(false);

        // Limpiar velocidades
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Lanzar
        rb.AddForce(direction * launchForce, ForceMode.Impulse);

        // Volver a activar el seguimiento de cámara
        cameraManager.enabled = true;

        Debug.Log("Jugador lanzado");
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (GameManager.Instance.currentStage != GameManager.Stage.Launch)
            return;

        if (!launched)
            return;

        if (collision.gameObject.CompareTag("wall"))
        {
            Invoke(nameof(RestartLaunch), 1f);
        }
    }
    void RestartLaunch()
    {
        launched = false;

        // Detener movimiento
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Llevar al punto inicial
        transform.position = launchSpawn.position;
        transform.rotation = launchSpawn.rotation;

        // Mover nuevamente la cámara
        cameraManager.enabled = false;

        mainCamera.transform.position = launchCameraPoint.position;
        mainCamera.transform.rotation = launchCameraPoint.rotation;
        mainCamera.transform.LookAt(launchTarget);

        // Mostrar nuevamente la UI
        launchUI.SetActive(true);

        // Reiniciar la mira
        crosshair.ResetAim();
    }
}