using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public List<Transform> Walkpoints;
    public float viewAngle;
    public float viewDistance;
    
    private int Currentpoint;
    private int nexpointnum;
    private float basespeed;
    public  float turnspeed;
    public Transform playerspawnpoint;
    private Vector3 lookdirection;
    private Quaternion lookrotation;
    private Transform targetpoint;
    private NavMeshAgent nav;

    RaycastHit whatSeen;
    private Vector3 playerDirection;
    float sightoffset;
    bool turn = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        nav = this.GetComponent<NavMeshAgent>();
        nexpointnum = 0;
        targetpoint = Walkpoints[nexpointnum];
        basespeed = nav.speed;
        nav.SetDestination(new Vector3(targetpoint.position.x, targetpoint.position.y, targetpoint.position.z));
    }

    // Update is called once per frame
    void Update()
    {
        if (turn)
        {
            lookdirection = (targetpoint.position - transform.position).normalized;
            lookrotation = Quaternion.LookRotation(lookdirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookrotation, turnspeed * Time.deltaTime);
            if (Mathf.Abs(Mathf.Abs(transform.rotation.y) - Mathf.Abs(lookrotation.y)) <= .001)
            {
               // Debug.Log("turnstop");
                turn = false;
                nav.enabled = true;
            }

        }

        if (nav.enabled)
        {

            nav.SetDestination(new Vector3(targetpoint.position.x, targetpoint.position.y, targetpoint.position.z));

        }

        if (Vector3.Distance(this.transform.position, targetpoint.position) <= .1 && Vector3.Distance(this.transform.position, targetpoint.position) >= -.1)
        {
            //Debug.Log("turn");
            turn = true;
            nav.enabled = false;
            nexpointnum++;

            if(nexpointnum > Walkpoints.Count)
            {
                nexpointnum = 0;
            }
            
        }

        targetpoint = Walkpoints[nexpointnum];
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerDirection = other.gameObject.transform.position - transform.position;
            if(Physics.Raycast(transform.position, playerDirection, out whatSeen, viewDistance))
            {
                if (whatSeen.collider.gameObject.CompareTag("Player"))
                {
                    sightoffset = Vector3.Angle(transform.forward, playerDirection);
                    if ( Mathf.Abs(sightoffset) <= viewAngle/2.0f)
                    {
                        PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
                        if (playerController.gameOver == false)
                        {
                            playerController.spawnPoint = playerspawnpoint;
                            playerController.GameOver();
                            //Debug.Log("Got You");
                        }
                    }
                }
            }
        }
    }
    
}
