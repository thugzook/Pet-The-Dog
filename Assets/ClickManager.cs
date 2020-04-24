using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    private GameObject hand;
    private GameObject player;
    public static bool isPet; // true if player clicking dog

    // Start is called before the first frame update
    void Start()
    {
        hand = GameObject.Find("Hand");
        player = GameObject.Find("Player");
        isPet = false;
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
                SpriteRenderer sprite;
                sprite = hit.collider.gameObject.GetComponent<SpriteRenderer>();
                sprite.color = new Color(0.3f, 0.3f, 0.3f);
                hand.transform.position = new Vector3(mousePos.x, mousePos.y, -1);
                isPet = true;
            }
            else
            {
                hand.transform.position = new Vector3(mousePos.x, mousePos.y, -1);
                isPet = false;
            }
        }
        // return hand to default position
        else if (Input.GetMouseButtonUp(0))
        {
            hand.transform.position = new Vector3(player.transform.position.x + 0.5f, player.transform.position.y, 0);
            isPet = false;
        }
    }
}
