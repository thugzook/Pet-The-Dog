using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

class Owner
{
    /*
     * _timeMin and _timeMax adjust the length of READY state of owner
     * _timeRest adjusts length of IDLE state
     */
    public float _timeMin { get; set; }
    public float _timeMax { get; set; }
    public float _timeRest { get; set; }
    public OwnerLook.state _state { get; set; }

    public Owner(float timeMin , float timeMax, float timeRest)
    {
        _timeMin = timeMin;
        _timeMax = timeMax;
        _timeRest = timeRest;
        _state = OwnerLook.state.IDLE;
    }
}

public class OwnerLook : MonoBehaviour
{
    // track state of owner
    public enum state
    {
        IDLE,
        READY,
        LOOKING
    }

    // Invokes game end state
    public static UnityEvent OwnerCaughtYou;

    // Define difficulty scene-by-scene
    public static int Difficulty;

    // Determines if grandpa looking is enabled
    public static bool enabled;

    // Switches owner state when needed
    private static float timer;
    SpriteRenderer sprite;
    System.Random rand = new System.Random();
    Owner owner;

    // Start is called before the first frame update
    void Awake()
    {
        // Initialize OwnerCaughtYou
        if (OwnerCaughtYou == null)
            OwnerCaughtYou = new UnityEvent();

        // difficulty goes here
        if (true)
        {
            owner = new Owner(2.0f, 3.0f, 4.0f);
        }

        // initialize timer
        timer = (float)owner._timeRest;

        sprite = GetComponent<SpriteRenderer>();
        sprite.color = Color.green;
    }

    // Update is called once per frame
    void Update()
    {
        CheckTimer();

        // Player petting while owner is looking
        if (ClickManager.isPet && owner._state == state.LOOKING)
        {
            OwnerCaughtYou.Invoke();
            ClickManager.isPet = false;
        }       
    }

    void CheckTimer()
    {
        // change state of owner
        timer -= Time.deltaTime;
        if (timer < 0 && enabled)
        {
            if (owner._state == state.IDLE)
            {
                // Ready time equals some random float between timeMax and timeMin
                timer = (float)(rand.NextDouble() * (owner._timeMax - owner._timeMin) + owner._timeMin);
                owner._state = state.READY;
                sprite.color = Color.yellow;
            }
            else if (owner._state == state.READY)
            {
                // Looking time equals some random float between 3.0f and 1.0f
                timer = (float)(rand.NextDouble() * (3.0f - 1.0f) + 1.0f);
                owner._state = state.LOOKING;
                sprite.color = Color.red;
                sprite.sprite = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Art/grandpaangry.png", typeof(Sprite));
            }
            else // owner._state == state.LOOKING
            {
                // Idle time ranges between timeRest and timeRest / 2
                timer = (float)(rand.NextDouble() * (owner._timeRest - owner._timeRest / 2) + owner._timeRest / 2);
                owner._state = state.IDLE;
                sprite.color = Color.green;
                sprite.sprite = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Art/grandpacalm.png", typeof(Sprite));
            }
        }
    }
}
