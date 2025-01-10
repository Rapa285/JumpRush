using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    private GameObject playerPrefab; // Reference to the player prefab
    [SerializeField]
    private CameraFollow cameraFollow; // Reference to the CameraFollow script

    private int time;
    private int jump = 2; 

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // Prevent duplicate GameManager instances
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Gameplay" || scene.name == "Tutorial")
        {
            cameraFollow = Object.FindFirstObjectByType<CameraFollow>();
            // Instantiate the player
            GameObject playerInstance = Instantiate(playerPrefab);
            // Assign the player's Transform to the CameraFollow script
            if (cameraFollow != null)
            {
                cameraFollow.player = playerInstance.transform;
            }
            else
            {
                Debug.LogWarning("CameraFollow script is not assigned to GameManager!");
            }

            // Initialize UI and resume game
            UIManager.instance.innitUI();
            resumeGame();
        }
    }

    public void pauseGame()
    {
        Time.timeScale = 0f;
    }

    public void resumeGame()
    {
        Time.timeScale = 1f;
    }

    public void playerDie()
    {
        Debug.Log("player ded");
        pauseGame();
        UIManager.instance.showGameOverScreen();
    }
    public void Retry()
    {
        // Reload the current scene
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
        // Resume the game after reloading the scene
        resumeGame();
    }

    public void refreshJump(){
        Debug.Log("jumps refreshed");
        jump = 2;
    }

    public int getJump(){
        return jump;
    }

    public void decJump(){
        jump-=1;
    }
}
