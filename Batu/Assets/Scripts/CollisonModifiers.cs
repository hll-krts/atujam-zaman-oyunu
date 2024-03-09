using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisonModifiers : MonoBehaviour
{
    private int tagID;
    
    void Start()
    {
        if (this.gameObject.CompareTag("Slow"))
        {
            this.tagID = 0;//Slow
        }
        else 
        { 
            this.tagID = 1;//Fast
            //Debug.Log(this.gameObject.name+" Tag ID set to: "+this.tagID );
        } ;
    }

   

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision detected on"+ gameObject.name+" with " + other.gameObject.name);
        if (other.tag == "Enemy") 
        {
            other.gameObject.GetComponent<Rigidbody>().drag=5;
        }
        else if (other.tag == "Enemy" && this.tagID == 1)
        {
            other.gameObject.GetComponent<Rigidbody>().mass = 8;
        }

        switch (this.tagID)
        {
            case 0://Slow

                other.gameObject.GetComponent<SpeedController>().thisObjectsTimeScale = 0.5f;
                break;
            case 1://Fast
                other.gameObject.GetComponent<SpeedController>().thisObjectsTimeScale = 2f;
                break;
            default:
                other.gameObject.GetComponent<SpeedController>().thisObjectsTimeScale = 1f;
                break;
        }
        


    }

    void OnTriggerExit(Collider other) 
    {
        if (other.tag == "Enemy" && this.tagID == 0)
        {
            other.gameObject.GetComponent<Rigidbody>().drag = 0;
        }
        else if (other.tag == "Enemy" && this.tagID == 1) 
        {
            other.gameObject.GetComponent<Rigidbody>().mass = 2;
        }
        switch (this.tagID)
        {
            case 0://Slow
                StartCoroutine(IncreaseValueOverTime(other));
                break;
            case 1://Fast
                StartCoroutine(ReduceValueOverTime(other));
                break;
            default:
                other.gameObject.GetComponent<SpeedController>().thisObjectsTimeScale = 1f;
                break;
        }
        StartCoroutine(ReduceValueOverTime(other)); 
    }

    


    IEnumerator ReduceValueOverTime(Collider other)
    {
        while (other.gameObject.GetComponent<SpeedController>().thisObjectsTimeScale >= 1f)
        {
            // Reduce the value over time
            other.gameObject.GetComponent<SpeedController>().thisObjectsTimeScale -= .3f * Time.deltaTime;

            yield return null; 
        }

        yield break;
        // The value has reached or gone below zero, you can perform additional actions here if needed
        //Debug.Log("Value reduced to or below zero.");
    }
    IEnumerator IncreaseValueOverTime(Collider other)
    {
        while (other.gameObject.GetComponent<SpeedController>().thisObjectsTimeScale <= 1f)
        {
            // Increase the value over time
            other.gameObject.GetComponent<SpeedController>().thisObjectsTimeScale += .6f * Time.deltaTime;

            


            yield return null;
        }

        yield break;
        // The value has reached or gone below zero, you can perform additional actions here if needed
        //Debug.Log("Value reduced to or below zero.2");
    }

}
