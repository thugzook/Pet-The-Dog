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

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Health is on a 3 heart scale but .5 increments
        for (int i = 0; i < 3; i ++)
        {
            if (i < GameManager.health)
            {
                if (i < GameManager.health - 0.5f) // meaning health incremented by a half is a full heart, assign a half heart
                    Hearts[i].sprite = fullHeart;
                else
                    Hearts[i].sprite = halfHeart;
            }
            else
            {
                Hearts[i].sprite = noHeart;
            }
        }
    }
}
