using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField]
    private Canvas canvas;

    private GameObject gameoverscreen;
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
            gameoverscreen = GameObject.Find("GameOverScreen");
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void showGameOverScreen(){

        gameoverscreen.SetActive(true);
    }

    public void innitUI(){
        gameoverscreen.SetActive(false);
    }

    public void retry(){
        GameManager.instance.Retry();
    }

}
