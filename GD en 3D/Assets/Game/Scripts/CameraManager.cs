using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform player;

    [Header("Suavidad")]
    public float smoothSpeed = 5f;

    [Header("Posición Normal")]
    public Vector3 normalOffset = new Vector3(0f, 3f, -6f);

    [Header("Posición Flappy")]
    public Vector3 flappyOffset = new Vector3(-8f, 2f, 0f);

    [Header("Launch")]
    public Transform launchCameraPoint;
    public Transform launchTarget;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void LateUpdate()
    {
        if (player == null)
            return;

        Vector3 desiredPosition;

        

        if (GameManager.Instance.currentStage == GameManager.Stage.Launch)
        {
           
            desiredPosition = launchCameraPoint.position;
        }
        else
        {
            Vector3 targetOffset = normalOffset;

            switch (GameManager.Instance.currentStage)
            {
                case GameManager.Stage.Normal:
                    targetOffset = normalOffset;
                    break;

                case GameManager.Stage.Flappy:
                    targetOffset = flappyOffset;
                    break;

                case GameManager.Stage.Shape:
                    targetOffset = normalOffset;
                    break;
            }

            desiredPosition = player.position + targetOffset;
        }

        cam.transform.position = Vector3.Lerp(
            cam.transform.position,
            desiredPosition,
            smoothSpeed * Time.deltaTime);

      
        if (GameManager.Instance.currentStage == GameManager.Stage.Launch)
        {
            cam.transform.LookAt(launchTarget);
        }
        else
        {
            cam.transform.LookAt(player);
        }
    }
}