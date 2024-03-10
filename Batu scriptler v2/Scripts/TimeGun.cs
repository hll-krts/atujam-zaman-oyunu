using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TimeGun : MonoBehaviour
{
    public TimeManager timeManager;
    public LayerMask BubbleLayer;
    public GameObject[] bubbles;
    public int currentBubble = 0,maxBubble=2;
   // public GameObject[] spawnedSlowBubble = new GameObject[2]; //0,1
    List<GameObject> spawnedBubbles = new List<GameObject>();
    public Camera cam;
    Ray _ray;



    float radius;
    float distance;
    private Vector3 midPoint;
    GameObject cube;
    public float expForce, expRadius;
    

    private void Start()
    {
         cube= GameObject.CreatePrimitive(PrimitiveType.Sphere);
         cube.transform.position= new Vector3(0,-5,0);
         
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot(bubbles[0]);//fast
        }
        else if (Input.GetMouseButtonDown(1)) 
        {
            Shoot(bubbles[1]);//slow
        }
    }

    
    private void Shoot(GameObject objectToSpawn) 
    {
        RaycastHit _hitInfo;
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);


        

        if (Physics.Raycast(_ray, out _hitInfo, 100f, ~BubbleLayer) && currentBubble <= maxBubble)
        {


            spawnedBubbles.Add(Instantiate(objectToSpawn, _hitInfo.point + (Vector3.up * 2), objectToSpawn.transform.rotation));
            currentBubble++;
            //print(currentBubble);

                

                if (currentBubble == maxBubble ) 
                {
                    GameObject bubble1 = spawnedBubbles[0]; GameObject bubble2 = spawnedBubbles[1];
                    string tag1 = bubble1.tag; string tag2 = bubble2.tag;

                    distance = Vector3.Distance(bubble1.transform.position, bubble2.transform.position);

                    radius = bubble2.GetComponent<SphereCollider>().radius* bubble2.transform.localScale.x;

                    midPoint = Vector3.Lerp(bubble1.transform.position, bubble2.transform.position, 0.5f);

                    if ((radius * 2) <= distance && tag1 != tag2)
                    {
                        print("girismiyor tipler farkli dist:" + distance);
                    }
                    else if ((radius * 2) <= distance && tag1 == tag2)
                    {
                        print("girismiyor tipleri ayni dist:" + distance);
                    }
                    else if ((radius * 2) > distance && tag1 != tag2)
                    {
                        print("girisiyor tipler farkli dist:" + distance);
                        Instantiate(cube, midPoint, objectToSpawn.transform.rotation);
                        Knockback();
                        foreach (GameObject destroy in spawnedBubbles)
                        {
                            Destroy(destroy);
                        }
                        spawnedBubbles.Clear();
                        currentBubble = 0;

                    }
                    else if ((radius * 2) > distance && tag1 == tag2)
                    {
                        print("girisiyor tipleri ayni dist:" + distance);
                        
                    }
                    
                }
                else if (currentBubble == maxBubble + 1)
                {
                GameObject destroyGameObject = spawnedBubbles[0];
                Destroy(destroyGameObject);//degistirilebilir
                spawnedBubbles.Remove(destroyGameObject);
                currentBubble=spawnedBubbles.Count;

                GameObject bubble1 = spawnedBubbles[0]; GameObject bubble2 = spawnedBubbles[1];
                    string tag1 = bubble1.tag; string tag2 = bubble2.tag;

                    distance = Vector3.Distance(bubble1.transform.position, bubble2.transform.position);

                    radius = bubble1.GetComponent<SphereCollider>().radius*bubble1.transform.localScale.x;

                    midPoint = Vector3.Lerp(bubble1.transform.position, bubble2.transform.position, 0.5f);

                    if ((radius * 2) <= distance && tag1 != tag2)
                    {
                        print("girismiyor tipler farkli dist:" + distance);
                    }
                    else if ((radius * 2) <= distance && tag1 == tag2)
                    {
                        print("girismiyor tipleri ayni dist:" + distance);
                    }
                    else if ((radius * 2) > distance && tag1 != tag2)
                    {
                        print("girisiyor tipler farkli dist:" + distance);
                        Instantiate(cube, midPoint , objectToSpawn.transform.rotation);
                        Knockback();
                        foreach (GameObject destroy in spawnedBubbles) 
                        {
                            Destroy(destroy);
                        }
                        spawnedBubbles.Clear();
                        currentBubble = 0;


                    }
                    else if ((radius * 2) > distance && tag1 == tag2)
                    {
                        print("girisiyor tipleri ayni dist:" + distance);
                        
                    }
                    
                    
                    
                    currentBubble = spawnedBubbles.Count;
                    print(currentBubble);

                }


            
           



        }
        
    }

    private void Knockback() 
    {
        Collider[] colliders = Physics.OverlapSphere(midPoint, expRadius);

        foreach (Collider nearby in colliders) 
        {
            Rigidbody rb = nearby.GetComponent<Rigidbody>();
            if (rb != null) 
            {
                rb.AddExplosionForce(expForce,midPoint,expRadius*(expRadius - Vector3.Distance(nearby.transform.position,midPoint)));
                nearby.GetComponent<SpeedController>().thisObjectsTimeScale = 1f;
                nearby.GetComponent<SpeedController>().isEffectedByExp = true;
                StartCoroutine(Timer(1,nearby));

            }
        }
    }
    IEnumerator Timer(int seconds,Collider nearby)
    {
        timeManager.DoSlowmotion();

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(seconds);

        nearby.GetComponent<SpeedController>().isEffectedByExp= false;
        yield return null;

    }

}
