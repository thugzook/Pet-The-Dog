using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmShake : MonoBehaviour
{
    private Vector3 startingPos;
    private static float amount = .02f;

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
}
