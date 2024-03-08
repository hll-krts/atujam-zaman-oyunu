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

   

    void OnTriggerStay(Collider collision)
    {
        Debug.Log("Collision detected with " + collision.gameObject.name);
        
        
        switch (this.tagID)
        {
            case 0://Slow
                TimeManager._timeModifier = 0.5f;
                break;
            case 1://Fast
                TimeManager._timeModifier = 2f;
                break;
            default:
                TimeManager._timeModifier = 1f;
                break;
        }

        
    }
    void OnTriggerExit(Collider collision) 
    {
        //TimeManager._timeModifier = Mathf.Clamp(TimeManager._timeModifier - acceleration * Time.deltaTime, minSpeed, maxSpeed);
        switch (this.tagID)
        {
            case 0://Slow
                StartCoroutine(IncreaseValueOverTime());
                break;
            case 1://Fast
                StartCoroutine(ReduceValueOverTime());
                break;
            default:
                TimeManager._timeModifier = 1f;
                break;
        }
        StartCoroutine(ReduceValueOverTime()); 
    }

    IEnumerator ReduceValueOverTime()
    {
        while (TimeManager._timeModifier >= 1f)
        {
            // Reduce the value over time
            TimeManager._timeModifier -= .2f * Time.deltaTime;

            // Optionally, perform actions or update UI based on the reduced value
            

            yield return null; 
        }

        // The value has reached or gone below zero, you can perform additional actions here if needed
        Debug.Log("Value reduced to or below zero.");
    }
    IEnumerator IncreaseValueOverTime()
    {
        while (TimeManager._timeModifier <= 1f)
        {
            // Reduce the value over time
            TimeManager._timeModifier += .6f * Time.deltaTime;

            // Optionally, perform actions or update UI based on the reduced value


            yield return null;
        }

        // The value has reached or gone below zero, you can perform additional actions here if needed
        Debug.Log("Value reduced to or below zero.");
    }

}
