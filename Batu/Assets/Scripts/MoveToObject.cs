using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToObject : MonoBehaviour
{
    public Transform target;
    public float speed;
    private float _timeMod;

    
    private void FixedUpdate()
    {
        _timeMod = GetComponent<SpeedController>().thisObjectsTimeScale;
        this.transform.position = Vector3.MoveTowards(transform.position,target.position,speed*Time.deltaTime*_timeMod);
    }
}
