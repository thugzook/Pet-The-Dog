using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HandTrack : MonoBehaviour
{
    public GameObject arm;
    private Vector3 originPos; // retract position of hand
    private static Vector3 lastPos; // last arm position
    private ArmManager armManager;

    // Start is called before the first frame update
    void Start()
    {
        // initialize originPos and lastPos
        originPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 1);

        // is equal when hand is retracted
        lastPos = originPos;

        // initialize armManager script
        //armManager = ClickManager.GameController.GetComponent<ArmManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TrackHand()
    {
        // get position of mouse click
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        // Draw arm segments
        // if lastPos == originPos, pass originPos as origin point to drawArm
        if (lastPos.Equals(originPos))
        {
            StartCoroutine(ClickManager.GameController.GetComponent<ArmManager>().drawArm(new Vector2(originPos.x - 0.5f, originPos.y - 0.5f), mousePos));
            // ClickManager.GameController.GetComponent<ArmManager>().drawArm(new Vector2(originPos.x - 0.5f, originPos.y - 0.5f), mousePos);
        }
        // else, pass lastPos as originPoint
        else
        {
            StartCoroutine(ClickManager.GameController.GetComponent<ArmManager>().drawArm(lastPos, mousePos));
            // ClickManager.GameController.GetComponent<ArmManager>().drawArm(lastPos, mousePos);
        }

        // update last clicked position
        lastPos = mousePos;

        /// TODO: Redo this raycasting system to utilize colliders for the hand, rather than click position
        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

        // Manage what the hand has collided with
        if (hit.collider != null)
        {
            gameObject.transform.position = new Vector3(mousePos.x, mousePos.y, -1);
            ClickManager.isPet = true;
        }
        else
        {
            gameObject.transform.position = new Vector3(mousePos.x, mousePos.y, -1);
            ClickManager.isPet = false;
            ClickManager.isExtend = true;

            /// Access outside script
            //HandShake handScript = hand.GetComponent<HandShake>();
            //HandScript.MovingShake(hand.transform.position);
        }
    }

    public void RetractHand()
    {
        // Reset hand to original state and position
        gameObject.transform.position = originPos;
        ClickManager.isPet = false;
        ClickManager.isExtend = false;

        // Reset lastPos 
        lastPos = originPos;
        StartCoroutine(ClickManager.GameController.GetComponent<ArmManager>().destroyArm());
    }
}
