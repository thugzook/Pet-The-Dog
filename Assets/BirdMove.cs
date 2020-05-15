using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMove : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private float SPEED = 5f;

    // Start is called before the first frame update
    void Start()
    {
        // initialize bird move right
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(SPEED, 0);
    }

    // Update is called once per frame
    void Update()
    {
    }

    // Detects if bird collides with walls
    void OnCollisionEnter2D(Collision2D coll)
    {
        // Flip the bird image
        Vector3 lTemp = transform.localScale;
        lTemp.x *= -1;
        transform.localScale = lTemp;
    }

    // Detects if bird collides with Player
    void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log("Collision: " + coll.name);
    }
}
