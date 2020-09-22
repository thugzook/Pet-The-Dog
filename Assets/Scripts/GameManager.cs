using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
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
    private static int level = 2;
    private static float health = 3f;


    System.Random rand = new System.Random();

    void LevelLose()
    {
        Lose.Invoke();
    }

    void LevelWin()
    {
        // increment level and call level manager
        Win.Invoke();
        level++;
        levelManager();
        GameObject.Find("LevelText").GetComponent<UnityEngine.UI.Text>().text = "Level " + (level + 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Start is called before the first frame update
    void Start()
    {
        // initialize game elements
        ProgressBar.onProgressComplete.AddListener(LevelWin);
        OwnerLook.OwnerCaughtYou.AddListener(LevelLose);
        dogPosition = GameObject.Find("Dog").GetComponent<Transform>().position;
        // Manage levels
        levelManager();
        GameObject.Find("LevelText").GetComponent<UnityEngine.UI.Text>().text = "Level " + (level + 1);
    }

    void levelManager()
    {
        switch (level)
        {
            // Level 1: No Grandpa
            case 0:
                // disable unneeded features
                OwnerLook.enabled = false;
                GameObject.Find("Health").SetActive(false);
                GameObject.Find("Vacuum").SetActive(false);
                BirdMove.enabled = false;
                break;
            // Level 2: Grandpa looking
            case 1:
                OwnerLook.enabled = true;
                break;
            // Level 3: Decreasing pet level
            case 2:
                StartCoroutine("DecreaseProgress");
                break;
            // Level 4: Bird poop retracts player's hand
            case 3:
                BirdMove.enabled = true;
                break;
            // Level 5: Birds attack (hurts dog)
            case 4:
                StartCoroutine("SpawnAttackBirds");
                break;
            // Level 6: More birds attack
            case 5:
                StartCoroutine("SpawnAttackBirds");
                break;
            // Level 7: Birds take over grandpa's body
            case 6:
                break;
            // level 8: INSANE bird level
            case 7:
                break;
            // level 9: NUKE. Time limit
            case 8:
                break;
        }
    }

private IEnumerator DecreaseProgress()
    {
        while (true)
        {
            if (GameObject.Find("Slider").GetComponent<ProgressBar>().CurrentValue > 0)
                GameObject.Find("Slider").GetComponent<ProgressBar>().CurrentValue-= 0.003f;
            yield return new WaitForSeconds(0.01f);
        }
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
