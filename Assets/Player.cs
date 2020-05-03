using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPet : MonoBehaviour
{
    public Sprite idleAnim;
    public Sprite petAnim;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<SpriteRenderer>().sprite = idleAnim;
    }

    // Update is called once per frame
    void Update()
    {
        if (ClickManager.isPet)
            this.GetComponent<SpriteRenderer>().sprite = petAnim;
        else
            this.GetComponent<SpriteRenderer>().sprite = idleAnim;
    }
}
