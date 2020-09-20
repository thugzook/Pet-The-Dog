using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumMove : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private float timer = 0.0f;
    private float maxTime = 1.0f;
    private float SPEED = 5f;
    private bool dir = true; // left is 0 right is one

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(SPEED, 0);
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > maxTime)
        {

            // Remove the recorded 2 seconds.
            timer = timer - maxTime;
            Time.timeScale = 1.0f;

            dir = !dir;

            if (dir == false) // left
                rb2d.velocity = new Vector2(-SPEED, 0);
            else
                rb2d.velocity = new Vector2 (SPEED, 0);
            Vector3 lTemp = transform.localScale;
            lTemp.x *= -1;
            transform.localScale = lTemp;
        }
    }
}
