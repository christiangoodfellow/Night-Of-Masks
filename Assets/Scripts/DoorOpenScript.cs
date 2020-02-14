using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenScript : MonoBehaviour
{

    public bool isOpen;
    public float openSpeed;
    public Transform doorRotatePoint;
    private Quaternion closedQuaternion;
    public Transform doorOpenRotatePoint;
    private Quaternion openQuaternion;
    // Start is called before the first frame update
    void Start()
    {
        closedQuaternion = doorRotatePoint.rotation;
        openQuaternion = doorOpenRotatePoint.rotation;
    }

    // Update is called once per frame
    void Update()
    {

        if (isOpen)
        {
            if (Mathf.Abs(doorRotatePoint.rotation.y - openQuaternion.y) > .01)
            { 
                doorRotatePoint.rotation = Quaternion.Slerp(doorRotatePoint.rotation, openQuaternion, openSpeed * Time.deltaTime);
            }
        }
        else
        {
            if(Mathf.Abs(doorRotatePoint.rotation.y - closedQuaternion.y) > .01)
            {
                doorRotatePoint.rotation = Quaternion.Slerp(doorRotatePoint.rotation, closedQuaternion, openSpeed * Time.deltaTime);
            }
        }
        

    }
}
