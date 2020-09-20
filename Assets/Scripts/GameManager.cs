using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public UnityEvent Win;
    public UnityEvent Lose;
    public GameObject attackBird;
    public int attackBirdLimit = 2;
    private List<GameObject> attackBirdList = new List<GameObject>();
    private static float timer;
    private static Vector3 dogPosition;

    System.Random rand = new System.Random();

    void LevelLose()
    {
        Lose.Invoke();
    }

    void LevelWin()
    {
        Win.Invoke();
        SceneManager.LoadScene(sceneBuildIndex: 1);
    }

    // Start is called before the first frame update
    void Start()
    {
        // Invoke according to game state
        ProgressBar.onProgressComplete.AddListener(LevelWin);
        OwnerLook.OwnerCaughtYou.AddListener(LevelLose);
        dogPosition = GameObject.Find("Dog").GetComponent<Transform>().position;
        StartCoroutine("SpawnAttackBirds");


        //GameObject.Find("Bird").GetComponent<BirdMove>().spawnRate -= 10.0f; ;
    }

    // Spawns attack birds
    private IEnumerator SpawnAttackBirds()
    {
        while (true)
        {
            if (attackBirdList.Count < attackBirdLimit)
            {
                timer -= Time.deltaTime;
                if (timer < 0)
                {
                    // spawn an attackbird with velocity towards Dog
                    float x_pos = (float)(rand.NextDouble() * (13f + 1f + 13f) - 13f);
                    float y_pos = 6.5f;


                    Vector3 birdPosition = new Vector3(x_pos, y_pos, 0);
                    Vector3 vectorToDog = dogPosition - birdPosition;
                    float angle = Mathf.Atan2(vectorToDog.y, vectorToDog.x) * Mathf.Rad2Deg;
                    Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);

                    GameObject birdInstance = Instantiate(attackBird, new Vector2(x_pos, y_pos), q);

                    vectorToDog.Normalize();

                    birdInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(vectorToDog.x, vectorToDog.y);
                    attackBirdList.Add(birdInstance);

                    // flip attack bird if spawned on right side
                    Vector3 lTemp = birdInstance.GetComponent<Transform>().localScale; 
                    lTemp.y *= System.Math.Sign(-x_pos);
                    birdInstance.GetComponent<Transform>().localScale = lTemp;

                    // reset timer
                    //timer = (float)(rand.NextDouble() * (spawnRate - 0f) + 0f);
                }
            }
            yield return new WaitForSeconds(1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
