using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Sprites used for health management
    // health comes from GameManager.health
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite noHeart;

    public Image[] Hearts;

    private const float INCREMENT = .5f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Health is on a 3 heart scale but .5 increments
        for (int i = 0; i < 3 / INCREMENT; i++)
        {
           
        }
    }
}
