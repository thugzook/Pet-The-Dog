﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMove : MonoBehaviour
{
    public GameObject poopObject;

    public float spawnRate = 3f; // lower value increases frequency
    public bool isPoop = false;
    public static bool enabled = false;

    private static float timer; // determines the frequency of the pooping
    private Rigidbody2D rb2d;
    private float SPEED = 5f;

    private GameObject poopInstance;
    private List<GameObject> attackInstance;

    System.Random rand = new System.Random();
    
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        // initialize bird move right
        if (isPoop && enabled)
        {
            rb2d.velocity = new Vector2(SPEED, 0);
            StartCoroutine("PoopHandler");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Detects if bird hits the boundary walls
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Wall"))
        {
            // Flip the bird image and velocity
            Vector3 lTemp = transform.localScale;
            lTemp.x *= -1;
            transform.localScale = lTemp;
            rb2d.velocity = new Vector2(-rb2d.velocity.x, 0);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        // if hit by player, send bird away and disable rigid body
        Vector3 lTemp = transform.localScale;
        lTemp.x *= -1;
        transform.localScale = lTemp;
    }

    // Drops a poop at bird location
    private IEnumerator PoopHandler()
    {
        while (true)
        {
            // if poop object is below scene, destroy it
            if (poopInstance)
            {
                if (poopInstance.GetComponent<Transform>().position.y < -10.0f) GameObject.Destroy(poopInstance);
            }
            else
            {
                // change state of owner
                timer -= Time.deltaTime;
                if (timer < 0)
                {
                    if (rb2d == null)
                    {
                        Debug.Log("NULL");
                    }
                    poopInstance = Instantiate(poopObject, rb2d.position, rb2d.transform.rotation);
                    timer = (float)(rand.NextDouble() * (spawnRate - 0f) + 0f);
                }
            }
            yield return null;
        }
    }
}