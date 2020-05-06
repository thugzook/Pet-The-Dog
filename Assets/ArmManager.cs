using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmManager : MonoBehaviour
{
    private static List<GameObject> armList;
    public GameObject armInstance;

    // Start is called before the first frame update
    void Start()
    {
        // instantiate armList to manage all instances of arm
        armList = new List<GameObject>();
        //StartCoroutine(drawArm(0.02f, numberObjects));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Equally divide and instantiate arm objects in between two points
    public IEnumerator drawArm(Vector2 p1, Vector2 p2)
    {
        // Determine difference vector and number objects to draw based on distance
        Vector2 vectorToPoint = p2 - p1;
        int numObj = (int) Mathf.Ceil(vectorToPoint.magnitude / 0.5f);
        // Debug.Log("p1: " + p1 + "\np2: " + p2 + "\nvec2pnt: " + vectorToPoint + "\nMagnitude: " + vectorToPoint.magnitude);
        numObj++;

        for (int j = 1; j < numObj; j++)
        {
            // calculate position of next arm
            Vector3 position = p1 + j * vectorToPoint / numObj;
            yield return null;
            // Instantiate an Arm prefab, load that instance into armList and rotate it towards end vector
            float angle = Mathf.Atan2(vectorToPoint.y, vectorToPoint.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            GameObject armObject = Instantiate(armInstance, position, q);
            armList.Add(armObject);
        }

        yield return null;
        // Debug.Log("Size of list: " + armList.Count);
    }

    // Destroys all arm entities drawn
    public IEnumerator destroyArm()
    {
        armList.ForEach(delegate (GameObject armInstance)
        {
            Destroy(armInstance);

        });
        yield return null;
        armList.Clear();
    }
}
