using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    // GameObjects controlled by GameManager
    public GameObject attackBird;
    public GameObject clickManager;
    public GameObject healthUI;
    public GameObject mainMenuUI;
    public GameObject owner;

    public AudioSource audioSrc;
    public AudioClip ding;
    public AudioClip woof;
    public AudioClip squawk;

    public int attackBirdLimit = 2;
    public static List<GameObject> attackBirdList = new List<GameObject>();

    private static Vector3 dogPosition;
    private static int level = -1;
    public static float health = 3f;
    private static int spawnRate = 4;

    System.Random rand = new System.Random();

    // Defines what the player lost to
    public enum lossState
    {
        OWNER,
        BIRD,
        POOP
    }

    void LevelLose(lossState loss)
    {
        // Pause all functionality
        Pause();

        switch (loss)
        {
            case lossState.OWNER:
                Debug.Log("Owner Loss");
                break;
            case lossState.BIRD:
                Debug.Log("Bird Loss");
                break;
            case lossState.POOP:
                Debug.Log("Poop Loss");
                break;
        }
        // make restart button visible and interactable
        mainMenuUI.transform.Find("Restart").GetComponent<CanvasGroup>().alpha = 1;
        mainMenuUI.transform.Find("Restart").GetComponent<CanvasGroup>().interactable = true;
        mainMenuUI.transform.Find("Restart").GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
    
    public void LevelRestart()
    {
        // resume functionality
        Resume();


        // Reload scene
        health = 3f;
        levelManager();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        // renable game elements
        GameObject.Find("Hand").GetComponent<HandShake>().enabled = true;
        GameObject.Find("Click Manager").GetComponent<ClickManager>().enabled = true;

    }
    public void LevelWin()
    {
        // increment level and call level manager
        level++;
        levelManager();

        // Reset health
        health = 3f;

        // Play level win animations
        if (level != 0)
        {
            Pause();
            audioSrc.PlayOneShot(ding, 1);
            float wait = audioSrc.clip.length;
            while (wait > 0) { wait -= Time.deltaTime; }
            Resume();
        }

        // Reload scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        mainMenuUI.transform.Find("Restart").GetComponent<CanvasGroup>().interactable = false;
    }

    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        // initialize game elements
        ProgressBar.onProgressComplete.AddListener(LevelWin);
        OwnerLook.OwnerCaughtYou.AddListener(delegate { loseHealth(3, lossState.OWNER); }); // create a delegate for health loss when owner looks at you
        dogPosition = GameObject.Find("Dog").GetComponent<Transform>().position;
        // Manage levels
        levelManager();
    }

    void levelManager()
    {
        if (level != -1)
        {
            Pause();
            ShowPrompt();
        }

        switch (level)
        {
            // Level 0: Main Menu
            case -1:
                // hide all game objects
                GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
                healthUI.SetActive(false);

                // disable click manager
                clickManager.GetComponent<ClickManager>().enabled = false;

                // adjust alpha of all game Objects
                foreach (GameObject go in allObjects)
                {
                    if (go.GetComponent<Renderer>() && !(go.name == "Background") && !(go.name == "Title"))
                    {
                        Color newColor = new Color(1, 1, 1, 0);
                        go.GetComponent<Renderer>().material.color = newColor;
                    }
                }
                // show menu buttons
                mainMenuUI.GetComponent<CanvasGroup>().alpha = 1;
                mainMenuUI.GetComponent<CanvasGroup>().interactable = true;
                mainMenuUI.transform.Find("Title").gameObject.GetComponent<SpriteRenderer>().enabled = true;
                break;
            // Level 1: No Grandpa
            case 0:
                // disable unneeded features
                OwnerLook.enabled = false;
                healthUI.SetActive(false);
                break;
            // Level 2: Grandpa looking
            case 1:
                OwnerLook.enabled = true;
                healthUI.SetActive(false);
                break;
            // Level 3: Decreasing pet level
            case 2:
                OwnerLook.enabled = true;
                healthUI.SetActive(false);
                StartCoroutine("DecreaseProgress");
                break;
            // Level 4: Bird poop hurts half a heart
            case 3:
                OwnerLook.enabled = true;
                healthUI.SetActive(true);
                StartCoroutine("DecreaseProgress");
                GameObject.Find("PoopBird").GetComponent<BirdMove>().enabled = true;
                break;
            // Level 5: Birds attack (hurts dog)
            case 4:
                OwnerLook.enabled = true;
                healthUI.SetActive(true);
                attackBirdList.Clear();
                attackBirdLimit = 1;
                spawnRate = 4;
                StartCoroutine("DecreaseProgress");
                StartCoroutine("SpawnAttackBirds");
                break;
            // Level 6: More birds attack
            case 5:
                OwnerLook.enabled = true;
                healthUI.SetActive(true);
                attackBirdList.Clear();
                attackBirdLimit = 3;
                spawnRate = 7;
                // adjust the Owner difficulty to be easier
                owner.GetComponent<OwnerLook>().owner = new Owner(1.0f, 3.0f, 8.0f);
                GameObject.Find("PoopBird").GetComponent<BirdMove>().enabled = true;
                StartCoroutine("DecreaseProgress");
                StartCoroutine("SpawnAttackBirds");
                break;
            // Level 7: Hella poop
            case 6:
                // disable owner
                owner.SetActive(false);
                healthUI.SetActive(true);
                attackBirdList.Clear();
                attackBirdLimit = 4;
                spawnRate = 3;
                GameObject.Find("PoopBird").GetComponent<BirdMove>().enabled = true;
                GameObject.Find("PoopBird").GetComponent<BirdMove>().spawnRate = 1;
                StartCoroutine("DecreaseProgress");
                StartCoroutine("SpawnAttackBirds");
                break;
            // VICTORY
            case 7:
                SceneManager.LoadScene("WIN");
                break;
        }
    }

    public void loseHealth(float amt, lossState loss)
    {
        health -= amt;
        if (health <= 0)
        {
            LevelLose(loss);
        }
        else
        {
            //GameObject.Find("Click Manager").GetComponent<ClickManager>().handRetract.Invoke();
        }
    }

    private void Pause()
    {
        // Pause all functionality
        Time.timeScale = 0;
        GameObject.Find("Hand").GetComponent<HandShake>().enabled = false;
        GameObject.Find("Click Manager").GetComponent<ClickManager>().enabled = false;
        mainMenuUI.transform.Find("LevelTextContainer").GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    private void ShowPrompt()
    {
        // Show Menu UI and set flavor text
        mainMenuUI.transform.Find("LevelTextContainer").GetComponent<CanvasGroup>().interactable = true;
        mainMenuUI.transform.Find("LevelTextContainer").GetComponent<CanvasGroup>().alpha = 1;
        switch (level)
        {
            case -1:
                mainMenuUI.transform.Find("LevelTextContainer").transform.Find("LevelText").GetComponent<TextMeshProUGUI>().SetText("level -1");
                break;
            case 0:
                mainMenuUI.transform.Find("LevelTextContainer").transform.Find("LevelText").GetComponent<TextMeshProUGUI>().SetText("Old dog Bartholomew has been sitting outside by himself the last few days and you can't stand to see him by himself.\n\nYou take it upon yourself to give that dog some love. \n\nYou reach over to pet him.");
                break;
            case 1:
                mainMenuUI.transform.Find("LevelTextContainer").transform.Find("LevelText").GetComponent<TextMeshProUGUI>().SetText("Old Man Jenkins doesn't take to kindly to you petting his dog. \n\nMake sure he doesn't catch you.");
                break;
            case 2:
                mainMenuUI.transform.Find("LevelTextContainer").transform.Find("LevelText").GetComponent<TextMeshProUGUI>().SetText("Love is like the stock market.\n\nIf you supply too much, the demand goes down.");
                break;
            case 3:
                mainMenuUI.transform.Find("LevelTextContainer").transform.Find("LevelText").GetComponent<TextMeshProUGUI>().SetText("Winter migrations have started and the birds are in full force this morning.\n\nDon't let them shit on your good day.");
                break;
            case 4:
                mainMenuUI.transform.Find("LevelTextContainer").transform.Find("LevelText").GetComponent<TextMeshProUGUI>().SetText("The bird coalition don't take too kindly to you intruding on their pooping zone. They're now armed and dangerous.\n\nDon't let them near Bartholomew.");
                break;
            case 5:
                mainMenuUI.transform.Find("LevelTextContainer").transform.Find("LevelText").GetComponent<TextMeshProUGUI>().SetText("Now they're arming and sending their kids off to the front lines...\n\nGive them a 5-fingered welcome.");
                break;
            case 6:
                mainMenuUI.transform.Find("LevelTextContainer").transform.Find("LevelText").GetComponent<TextMeshProUGUI>().SetText("It's Sunday, and Old Man Jenkins has taken the day off to praise the Lord.\n\nThe birds see this as an opportunity to raise their last offensive. Show them the power of love.");
                break;
        }

        // Start coroutine of listener
        StartCoroutine("WaitPrompt");
    }

    private void Resume()
    {
        Time.timeScale = 1;
        mainMenuUI.transform.Find("Restart").GetComponent<CanvasGroup>().alpha = 0;
        mainMenuUI.transform.Find("Restart").GetComponent<CanvasGroup>().interactable = false;
        GameObject.Find("Hand").GetComponent<HandShake>().enabled = true;
        GameObject.Find("Click Manager").GetComponent<ClickManager>().enabled = true;
        StopCoroutine("WaitPrompt");
    }

    private IEnumerator DecreaseProgress()
    {
        while (true)
        {
            if (GameObject.Find("Slider").GetComponent<ProgressBar>().CurrentValue > 0)
                GameObject.Find("Slider").GetComponent<ProgressBar>().CurrentValue-= 0.002f;
            yield return new WaitForSeconds(0.01f);
        }
    }

    // Spawns attack birds
    private IEnumerator SpawnAttackBirds()
    {
        float timer = 0f;
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
                    float randSpeed = Random.Range(1.5f, 5);
                    birdInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(vectorToDog.x * randSpeed, vectorToDog.y * randSpeed);
                    attackBirdList.Add(birdInstance);

                    // flip attack bird if spawned on right side
                    Vector3 lTemp = birdInstance.GetComponent<Transform>().localScale; 
                    lTemp.y *= System.Math.Sign(-x_pos);
                    birdInstance.GetComponent<Transform>().localScale = lTemp;

                    // reset timer
                    timer = Random.Range(0.5f, spawnRate);
                    Debug.Log("Timer " + timer);
                }
            }
            yield return null;
        }
    }
    public IEnumerator WaitPrompt()
    {
        // Create listener for WaitForUIButtons object
        var waitForButton = new WaitForUIButtons(mainMenuUI.transform.Find("LevelTextContainer").transform.Find("Continue").GetComponent<Button>());
        yield return waitForButton.Reset();

        // resume game and hide LevelTextContainer
        if (waitForButton.PressedButton == mainMenuUI.transform.Find("LevelTextContainer").transform.Find("Continue").GetComponent<Button>())
        {
            mainMenuUI.transform.Find("LevelTextContainer").GetComponent<CanvasGroup>().interactable = false;
            mainMenuUI.transform.Find("LevelTextContainer").GetComponent<CanvasGroup>().alpha = 0;
            Resume();
        }
    }

    public IEnumerator FadeTo(float aValue, float aTime)
    {
        // Access every gameobject and reduce their alpha value
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();


        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            foreach (GameObject go in allObjects)
            {
                if (go.GetComponent<Renderer>() && !(go.name == "Background"))
                {
                    float alpha = go.GetComponent<Renderer>().material.color.a;
                    Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, 0.2f * Mathf.Pow(t, 2)));
                    go.GetComponent<Renderer>().material.color = newColor;
                }
            }
            yield return null;
        }
        
        // re-enable click manager once fading is done
        clickManager.GetComponent<ClickManager>().enabled = true;

        // Disable main menu
        mainMenuUI.transform.Find("Start").gameObject.SetActive(false);
        mainMenuUI.transform.Find("Quit").gameObject.SetActive(false);
        mainMenuUI.transform.Find("Title").gameObject.SetActive(false);

        LevelWin();
    }

        // Update is called once per frame
    void Update()
    {
        
    }

}
