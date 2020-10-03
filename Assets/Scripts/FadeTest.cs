using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FadeTest : MonoBehaviour
{
    public GameObject clickManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fade()
    {
        // Fade game objects
        StartCoroutine(GameObject.Find("Game Manager").GetComponent<GameManager>().FadeTo(1f, 3f));

        // Hide menu options
        transform.parent.gameObject.GetComponent<CanvasGroup>().alpha = 0f;
        transform.parent.gameObject.GetComponent<CanvasGroup>().interactable = false;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
