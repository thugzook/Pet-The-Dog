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
        StartCoroutine(FadeTo(1f, 3f));
    }

    IEnumerator FadeTo(float aValue, float aTime)
    {
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            foreach (GameObject go in allObjects)
            {
                if (go.GetComponent<Renderer>() && !(go.name=="Background"))
                {
                    float alpha = go.GetComponent<Renderer>().material.color.a;
                    Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, 0.2f*Mathf.Pow(t,2)));
                    go.GetComponent<Renderer>().material.color = newColor;
                }
            }
            yield return null;
        }
        // re-enable click manager once fading is done
        clickManager.GetComponent<ClickManager>().enabled = true;
        GameObject.Find("Game Manager").GetComponent<GameManager>().LevelWin();
    }
}
