using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandShake : MonoBehaviour
{
    // controls shaking of hand
    private static float amount = .05f;
    public GameObject hand;
    private static Vector3 startingPos;
    // private static int frame_count = 0;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = new Vector3(hand.transform.position.x, hand.transform.position.y, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (!ClickManager.isExtend && !ClickManager.isPet)
        {
            IdleShake();
        }
        
    }

    void IdleShake()
    {
        hand.transform.position = new Vector3(startingPos.x + Random.insideUnitCircle.x * amount,
                                             startingPos.y + Random.insideUnitCircle.y * amount,
                                               1);
    }

    public void MovingShake(Vector3 pos)
    {
        hand.transform.position = new Vector3(pos.x + Random.insideUnitCircle.x * amount,
                                              pos.y + Random.insideUnitCircle.y * amount,
                                               1);
    }
}
