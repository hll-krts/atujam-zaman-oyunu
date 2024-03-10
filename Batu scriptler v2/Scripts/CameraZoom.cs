using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

public class CameraZoom : MonoBehaviour
{

    [SerializeField] private float fieldOfViewMin=20f;
    [SerializeField] private float fieldOfViewMax = 80f;
    CinemachineVirtualCamera _vCam;
    

    float _fov=60f;
    bool IsRotating = false;
    public Transform player;
    
    

    void Start()
    {
       _vCam= GetComponent<CinemachineVirtualCamera>();
        
    }

    void Update()
    {
        if (Input.mouseScrollDelta.y>0 && _fov < fieldOfViewMax) 
        {
            //targetFieldOfView += 5f;
            _fov += 5f;
           
        }

        if (Input.mouseScrollDelta.y < 0 && _fov> fieldOfViewMin)
        {
            //targetFieldOfView -= 5f;
            _fov -= 5f;

        }

        if (Input.GetMouseButtonDown(2) && !IsRotating)
        {
            
            IsRotating = true;
            CamRot();

            
        }
        _vCam.m_Lens.FieldOfView = Mathf.Lerp(_vCam.m_Lens.FieldOfView,_fov,Time.deltaTime* 1f);

        

    }

    private void CamRot()
    {

        float targetRotation = transform.eulerAngles.y + 90f;

        // Rotate smoothly towards the target rotation
        StartCoroutine(SmoothRotate(targetRotation));

    }

    IEnumerator SmoothRotate(float targetRotation)
    {

        float currentRotation = transform.eulerAngles.y;

        while (currentRotation < targetRotation)
        {
            
            float rotationAmount = 90f * Time.deltaTime;
            player.transform.Rotate(Vector3.up, rotationAmount);
            currentRotation += rotationAmount;

            yield return null;
        }

        // Ensure the exact target rotation is reached
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, targetRotation, transform.eulerAngles.z);
        IsRotating = false;
    }

}
