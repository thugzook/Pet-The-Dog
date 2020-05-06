using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClickManager : MonoBehaviour
{
    private static GameObject player;
    public static GameObject GameController;
    public static bool isPet; // true if player clicking dog
    public static bool isExtend; // true if player clicks

    public UnityEvent handTrack;
    public UnityEvent handRetract;

    // Start is called before the first frame update
    void Start()
    {
        // initialize Player and gameState variables
        player = GameObject.Find("Player");
        GameController = GameObject.FindWithTag("GameController");
        isPet = false;
        isExtend = false;

        // Initialize handTrack and handRetract
        if (handTrack == null)
            handTrack = new UnityEvent();
        if (handRetract == null)
            handRetract = new UnityEvent();

    }

    // Update is called once per frame
    void Update()
    {
        // If mouse clicked, determine if dog is clicked
        // If dog is not clicked, move hand to position
        if (Input.GetMouseButton(0))
        {
            handTrack.Invoke();
        }
        // return hand to default position
        else if (Input.GetMouseButtonUp(0))
        {
            handRetract.Invoke();
        }
    }
}
