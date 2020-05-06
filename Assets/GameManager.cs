using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public UnityEvent Win;
    public UnityEvent Lose;

    void LevelLose()
    {
        Lose.Invoke();
    }

    void LevelWin()
    {
        Win.Invoke();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Invoke according to game state
        ProgressBar.onProgressComplete.AddListener(LevelWin);
        OwnerLook.OwnerCaughtYou.AddListener(LevelLose);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
