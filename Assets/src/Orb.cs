using UnityEditor.UI;
using UnityEngine;

public class Orb : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Attack") && Input.GetMouseButtonDown(0))
        {
            GameManager.instance.refreshJump();
            Destroy(gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D collision){
        if (collision.CompareTag("Attack") && Input.GetMouseButtonDown(0)){
            GameManager.instance.refreshJump();
            Destroy(gameObject);
        }
    }
}
