using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    private GameObject hand;
    private GameObject player;
    public static bool isPet; // true if player clicking dog
    public static bool isExtend; // true if player clicks
    private static Vector3 handPos;


    // Start is called before the first frame update
    void Start()
    {
        hand = GameObject.Find("Hand");
        player = GameObject.Find("Player");
        handPos = new Vector3(hand.transform.position.x, hand.transform.position.y, 1);
        isPet = false;
        isExtend = false;
    }

    // Update is called once per frame
    void Update()
    {
        // If mouse clicked, determine if dog is clicked
        // If dog is not clicked, move hand to position
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null)
            {
                hand.transform.position = new Vector3(mousePos.x, mousePos.y, -1);
                isPet = true;
            }
            else
            {
                hand.transform.position = new Vector3(mousePos.x, mousePos.y, -1);
                isPet = false;
                isExtend = true;

                /// Access outside script
                //HandShake handScript = hand.GetComponent<HandShake>();
                //andScript.MovingShake(hand.transform.position);
            }
        }
        // return hand to default position
        else if (Input.GetMouseButtonUp(0))
        {
            hand.transform.position = handPos;
            isPet = false;
            isExtend = false;
        }
    }
}
