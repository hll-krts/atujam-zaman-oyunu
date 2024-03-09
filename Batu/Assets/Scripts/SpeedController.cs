using UnityEngine;

public class SpeedController : MonoBehaviour
{
    public float thisObjectsTimeScale=1f;

}
/*
    public float initialSpeed = 1f;  // The initial speed of the rigidbody
    public float targetSpeed = 10f;  // The target speed to reach
    public float accelerationRate = 2f;  // The rate at which the speed changes
    public float decelerationRate = 2f;  // The rate at which the speed decreases

    

    private Rigidbody targetRb;
    private float currentSpeed;

    void Start()
    {
        // Assuming the target object has a Rigidbody component
        
        currentSpeed = initialSpeed;
    }

    void Update()
    {
        // Apply the current speed to the target rigidbody
        Vector3 velocity = targetRb.velocity.normalized * currentSpeed;
        targetRb.velocity = velocity;
    }

    void OnTriggerEnter(Collider other)
    {
        targetRb=other.GetComponent<Rigidbody>();
        // Example: Accelerate on trigger enter
        if (other.CompareTag("Fast"))
        {
            Accelerate();
        }

        // Example: Decelerate on trigger enter
        if (other.CompareTag("Slow"))
        {
            Decelerate();
        }
    }
    void OnTriggerExit(Collider other)
    {
        
            GainVelocity();
        
    }


    void Accelerate()
    {
        currentSpeed += accelerationRate * Time.deltaTime;
        currentSpeed = Mathf.Clamp(currentSpeed, initialSpeed, targetSpeed);
    }

    void Decelerate()
    {
        currentSpeed -= decelerationRate * Time.deltaTime;
        currentSpeed = Mathf.Clamp(currentSpeed, initialSpeed, targetSpeed);
    }
    void GainVelocity()
    {
        // Example: Add velocity to the target object when exiting the trigger
        targetRb.AddForce(Vector3.forward * 5f, ForceMode.VelocityChange);
    }*/