using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandShake : MonoBehaviour
{
    // controls shaking of hand
    private static float amount = .05f;
    private static Vector3 startingPos;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (!ClickManager.isExtend)
        {
            IdleShake();
        }
        
    }

    void IdleShake()
    {
        gameObject.transform.position = new Vector2(startingPos.x + Random.insideUnitCircle.x * amount,
                                             startingPos.y + Random.insideUnitCircle.y * amount);
    }
}
