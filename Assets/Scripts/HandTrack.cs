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

        // Enqueue IEnumerators to armDrawCalls to draw arms
        // if lastPos == originPos, pass originPos as origin point to drawArm
        if (lastPos.Equals(originPos))
        {
            ArmManager.armDrawCalls.Enqueue(ClickManager.GameController.GetComponent<ArmManager>().drawArm(new Vector2(originPos.x - 0.5f, originPos.y - 0.5f), mousePos));
        }
        // else if distance is insignificant, do not draw
        else if (Vector3.Distance(lastPos, mousePos) < 0.2f)
        {
            return;
        }
        // draw arm segment from last pos
        else
        {
            ArmManager.armDrawCalls.Enqueue(ClickManager.GameController.GetComponent<ArmManager>().drawArm(lastPos, mousePos));
        }

        // update last clicked position
        lastPos = mousePos;
    }

    public void RetractHand()
    {
        // Reset hand to original state and position
        gameObject.transform.position = originPos;
        ClickManager.isPet = false;
        ClickManager.isExtend = false;

        // Reset lastPos and destroy all arm instances
        lastPos = originPos;
        StartCoroutine(ClickManager.GameController.GetComponent<ArmManager>().destroyArm());
    }
}
