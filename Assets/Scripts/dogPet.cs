using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class dogPet : MonoBehaviour
{
    public Sprite idleAnim;
    public Sprite petAnim;
    private AudioSource dogPettingSound;
    private Animation anim;
    void Pet()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        dogPettingSound = GetComponent<AudioSource>();
        gameObject.GetComponent<SpriteRenderer>().sprite = idleAnim;
    }

    // Update is called once per frame
    void Update()
    {
        if (ClickManager.isPet)
        {
            // Prevent animation fromplaying if playing already
            /*if (!anim.IsPlaying(petAnim.name))
            {
                anim.clip = petAnim;
                anim.Play();
            }*/
            if (!dogPettingSound.isPlaying)
                dogPettingSound.Play(0);
            gameObject.GetComponent<SpriteRenderer>().sprite = petAnim;
        }

        else
        {
            /*if (!anim.IsPlaying(idleAnim.name))
            {
                anim.clip = idleAnim;
                anim.Play();
            }*/
            dogPettingSound.Stop();
            gameObject.GetComponent<SpriteRenderer>().sprite = idleAnim;
        }
    }

    // Detects if player collides with Dog
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.name == "Hand")
        {
            ClickManager.isPet = true;
        }
    }
    void OnTriggerExit2D(Collider2D coll)
    {
        ClickManager.isPet = false;
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.collider.name == "Hand")
        {
            ClickManager.isPet = true;
        }
        
    }
}
