using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeGun : MonoBehaviour
{
    public GameObject[] bubbles;
    public Camera cam;
    Ray _ray;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot(bubbles[0]);
        }
        else if (Input.GetMouseButtonDown(1)) 
        {
            Shoot(bubbles[1]);
        }
    }

    
    private void Shoot(GameObject objectToSpawn) 
    {
        RaycastHit _hitInfo;
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_ray,out _hitInfo, 100)) 
        {
            Instantiate(objectToSpawn, _hitInfo.point+(Vector3.up*2), objectToSpawn.transform.rotation/*Quaternion.LookRotation(_hitInfo.normal)*/);
        }
    } 
}
