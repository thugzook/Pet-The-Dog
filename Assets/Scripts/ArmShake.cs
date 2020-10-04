using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmShake : MonoBehaviour
{
    private Vector3 startingPos;
    private static float amount = .02f;
    public GameObject clickManager;
    static float prevTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 1);
    }

    // Update is called once per frame
    void Update()
    {
        // Shake when arm object exists
        Shake();
    }

    void Shake()
    {
        gameObject.transform.position = new Vector3(startingPos.x + Random.insideUnitCircle.x * amount,
                                             startingPos.y + Random.insideUnitCircle.y * amount,
                                               1);
    }

    // Detects if player collides with anything
    void OnTriggerEnter2D(Collider2D coll)
    {
        // lose health on contact and time since prevTime is > 0.25sec
        if (coll.name == "Poop(Clone)" && (Time.time - prevTime) > 0.25f )
        {
            prevTime = Time.time;
            ClickManager.isExtend = false;
            clickManager.GetComponent<ClickManager>().handRetract.Invoke();
            GameObject.Find("Game Manager").GetComponent<GameManager>().loseHealth(0.5f, GameManager.lossState.POOP);
        }
        // Destroy the Object and reset timer
        Destroy(coll.gameObject);
    }
}
