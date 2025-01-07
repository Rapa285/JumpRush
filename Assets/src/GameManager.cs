using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    private GameObject player;


    private int time;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake(){
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
    private void OnEnable(){
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnDisable(){
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode){
        if(scene.name == "Gameplay"){
            Instantiate(player);
            UIManager.instance.innitUI();
            resumeGame();
        }
    }

    public void pauseGame(){
        Time.timeScale = 0f;
    }

    public void resumeGame(){
        Time.timeScale = 1f;
    }

    public void playerDie(){
        Debug.Log("player ded");
        pauseGame();
        UIManager.instance.showGameOverScreen();
    }
}
