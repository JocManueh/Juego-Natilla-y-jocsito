using UnityEngine;

public class GameManager : MonoBehaviour

{

    public PlayerController playerController;
    public FlappyController flappyController;

    public static GameManager Instance;


    public enum Stage
    {
        Normal,
        Flappy,
        WallWalk,
        Shape,
        Ship,
        Launch,
        Finish
    }

    [Header("Estado del juego")]
    public Stage currentStage = Stage.Normal;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void ChangeStage(Stage newStage)
    {
        currentStage = newStage;

        Debug.Log("Etapa actual: " + currentStage);

        switch (currentStage)
        {
            case Stage.Normal:

                playerController.enabled = true;
                flappyController.enabled = false;

                break;

            case Stage.Flappy:

                playerController.enabled = false;
                flappyController.enabled = true;

                break;
        }
    }
}