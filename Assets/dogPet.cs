using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dogPet : MonoBehaviour
{
    SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ClickManager.isPet)
        {
            sprite.color = new Color(0.3f, 0.3f, 0.3f);
        }
        else
        {
            sprite.color = new Color(1.0f, 1.0f, 1.0f);
        }
    }
}
