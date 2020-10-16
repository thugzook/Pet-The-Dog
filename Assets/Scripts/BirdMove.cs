using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMove : MonoBehaviour
{
    public GameObject poopObject;
    public Sprite deadBird;

    public float spawnRate = 3f; // lower value increases frequency
    public bool isPoop = false;

    private static float timer; // determines the frequency of the pooping
    private Rigidbody2D rb2d;
    private float SPEED = 5f;

    private GameObject poopInstance;
    private List<GameObject> attackInstance;

    System.Random rand = new System.Random();

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        // initialize bird move right
        if (isPoop)
        {
            rb2d.velocity = new Vector2(SPEED, 0);
            StartCoroutine("PoopHandler");
        }
        else
        {
            // Destroy after a set amount of time (i.e. has been off screen for too long)
            Destroy(gameObject, 20f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        // remove from attack bird list if found
        if (GameManager.attackBirdList.Contains(gameObject))
            GameManager.attackBirdList.Remove(gameObject);
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
        else if (coll.name == "Hand")
        {
            rb2d.velocity = new Vector2(0, 0);
            rb2d.gravityScale = 1;
            gameObject.GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
            // disable further collisions
            gameObject.GetComponent<BoxCollider2D>().size = Vector2.zero;
            gameObject.GetComponent<SpriteRenderer>().sprite = deadBird;
            // remove the bird from the list
            GameManager.attackBirdList.Remove(gameObject);
        }
        else if (coll.name == "Dog")
        {
            GameObject.Find("Game Manager").GetComponent<GameManager>().loseHealth(1, GameManager.lossState.BIRD);
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
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
