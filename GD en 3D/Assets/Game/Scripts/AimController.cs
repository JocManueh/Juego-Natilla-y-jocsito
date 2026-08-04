using UnityEngine;
using TMPro;

public class CrosshairController : MonoBehaviour
{
    public RectTransform crosshair;
    public TextMeshProUGUI infoText;

    public float range = 220f;
    public float speed = 250f;

    [HideInInspector]
    public float normalizedX;

    [HideInInspector]
    public float normalizedY;

    private bool lockX = false;
    private bool lockY = false;

    private float direction = 1;

    void Update()
    {
        if (GameManager.Instance.currentStage != GameManager.Stage.Launch)
            return;

        Vector2 pos = crosshair.anchoredPosition;

        // Movimiento Horizontal
        if (!lockX)
        {
            pos.x += direction * speed * Time.deltaTime;

            if (pos.x > range)
            {
                pos.x = range;
                direction = -1;
            }

            if (pos.x < -range)
            {
                pos.x = -range;
                direction = 1;
            }

            if (Input.GetMouseButtonDown(0))
            {
                lockX = true;
                direction = 1;
                infoText.text = "CLICK PARA FIJAR Y";
            }
        }

        // Movimiento Vertical
        else if (!lockY)
        {
            pos.y += direction * speed * Time.deltaTime;

            if (pos.y > range)
            {
                pos.y = range;
                direction = -1;
            }

            if (pos.y < -range)
            {
                pos.y = -range;
                direction = 1;
            }

            if (Input.GetMouseButtonDown(0))
            {
                lockY = true;

                normalizedX = crosshair.anchoredPosition.x / range;
                normalizedY = crosshair.anchoredPosition.y / range;

                infoText.text = "DISPARANDO...";

                FindFirstObjectByType<LaunchController>().Launch();
            }
        }

        crosshair.anchoredPosition = pos;
    }

    public Vector3 GetAim()
    {
        return new Vector3(normalizedX, normalizedY, 1f).normalized;
    }
    public void ResetAim()
    {
        lockX = false;
        lockY = false;

        direction = 1;

        normalizedX = 0;
        normalizedY = 0;

        crosshair.anchoredPosition = Vector2.zero;

        infoText.text = "CLICK PARA FIJAR X";
    }
}