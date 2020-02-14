using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open_Door : MonoBehaviour
{
    public Transform Door;
    float oof = 0;
    public bool Locked = true;
    private int timer = 0;
    public float speed = 30;

    // Start is called before the first frame update
    void Start(){ }
    // Update is called once per frame
    void Update()
    {
        timer++;
        if (!Locked)
        {
            Vector3 vectorToTarget = Door.position - transform.position;
            //instant opens if already unlocked and loaded
            if (timer < 4)
            {
                oof = Mathf.Atan2(vectorToTarget.x, vectorToTarget.z);
                Quaternion desiredRotaion = Quaternion.LookRotation(vectorToTarget);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotaion, 5000 * Time.deltaTime);
            }
            //normal opening 
            else
            {
                oof = Mathf.Atan2(vectorToTarget.x, vectorToTarget.z);
                Quaternion desiredRotaion = Quaternion.LookRotation(vectorToTarget);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotaion, speed * Time.deltaTime);
            }
        }
    }
}