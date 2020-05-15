using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmManager : MonoBehaviour
{
    private static Stack<GameObject> armStack;
    private static GameObject hand;
    public GameObject armInstance;
    public static Queue<IEnumerator> armDrawCalls;
    private bool isDrawing;

    // Start is called before the first frame update
    void Start()
    {
        // instantiate armList to manage all instances of arm
        armStack = new Stack<GameObject>();

        // Create queue of coroutines for Arm draw calls
        armDrawCalls = new Queue<IEnumerator>();
        StartCoroutine("drawQueue");

        hand = GameObject.Find("Hand");
        isDrawing = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Manages a queue of Coroutines to ensure drawing of entities is in order
    private IEnumerator drawQueue()
    {
        while (true)
        {
            if (armDrawCalls.Count > 0 && !isDrawing)
            {
                // wait until previous draw is done
                StartCoroutine(armDrawCalls.Dequeue());

            }
            yield return null;
        }
    }

    // Equally divide and instantiate arm objects in between two points
    public IEnumerator drawArm(Vector2 p1, Vector2 p2)
    {
        // turn on lock
        isDrawing = true;

        // Determine difference vector and number objects to draw based on distance
        Vector2 vectorToPoint = p2 - p1;
        int numObj = (int) Mathf.Ceil(vectorToPoint.magnitude / 0.5f);
        numObj++;

        for (int j = 1; j < numObj; j++)
        {
            // calculate position of next arm
            Vector3 position = p1 + j * vectorToPoint / numObj;

            // Instantiate an Arm prefab, Push instance into armList and rotate it towards end vector
            float angle = Mathf.Atan2(vectorToPoint.y, vectorToPoint.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            GameObject armObject = Instantiate(armInstance, position, q);
            armStack.Push(armObject);

            // reposition hand right in front of arm
            hand.transform.position = position;

            yield return null;
        }

        // turn off lock
        isDrawing = false;

        yield return null;
        // Debug.Log("Size of list: " + armList.Count);
    }

    // Destroys all arm entities drawn
    public IEnumerator destroyArm()
    {
        // stop all coroutines from running
        while (isDrawing) { yield return null; }

        // Remove all elements from armDrawCalls
        armDrawCalls.Clear();

        // in order, destroy arm elements
        while (armStack.Count > 0)
        {
            Destroy(armStack.Pop());
        }

        yield return null;
    }
}
