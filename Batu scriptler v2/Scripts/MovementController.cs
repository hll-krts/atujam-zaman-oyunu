using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
//Automaticly add rigidbody to the gameobject
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class MovementController : MonoBehaviour
{
    public float _speed = 6;
    public float _jumpForce = 6;
    public Animator playerAnimator;
    private Rigidbody _rig;
    private Vector2 _input;
    private Vector3 _movementVector;

    private float _timeMod;

    private GameObject character;
    Vector3 hitPos;
    public LayerMask ignoreMask;

    private bool effectedByExp=false;

    private void Start()
    {
        _rig = GetComponent<Rigidbody>();
        //Need to freez rotation so the player do not flip over
        _rig.freezeRotation = true;
        character=this.gameObject.transform.GetChild(0).gameObject;
    }
    private void Update()
    {
        if (!effectedByExp) 
        {
            //Cleanerway to get input
            _input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            //rotateToMouse();

            if (Input.GetButtonDown("Jump") && IsGrounded())
            {
                _rig.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            }
        }
        
        

    }
    private void FixedUpdate()
    {
        _timeMod = GetComponent<SpeedController>().thisObjectsTimeScale;
        effectedByExp = GetComponent<SpeedController>().isEffectedByExp;
        playerAnimator.SetFloat("RunSpeedMultiplier", _timeMod);
        if (_input!=Vector2.zero) 
        {
            Move();
            playerAnimator.SetBool("IsMoving", true);

        }
        else 
        {
            playerAnimator.SetBool("IsMoving", false);
        }
        
    }
    private void Move()
    {

        //Keep the movement vector aligned with the player rotation
        _movementVector = _input.x * transform.right * _speed * _timeMod + _input.y * transform.forward * _speed * _timeMod;
        //Apply the movement vector to the rigidbody without effecting gravity
        _rig.velocity = new Vector3(_movementVector.x, _rig.velocity.y, _movementVector.z);

        


        if (_movementVector.sqrMagnitude == 0f) //Look rotation viewing vector is zero - Hata çözümü
        {
            return;
        }

        Quaternion toRotation = Quaternion.LookRotation(_movementVector);

        if (toRotation.eulerAngles != _movementVector)
        {
            character.transform.rotation = Quaternion.RotateTowards(character.transform.rotation, toRotation, Time.deltaTime * 500f);
        }




    }

    /*Ray _ray;
    private void rotateToMouse() 
    {
        
        Quaternion toRotation = Quaternion.LookRotation(Camera.main.ScreenToWorldPoint(mousePosition));

        if (toRotation.eulerAngles != _movementVector)
        {
            character.transform.rotation = Quaternion.RotateTowards(character.transform.rotation, toRotation, Time.deltaTime * 50000f);
        }


        Vector3 mousePosition = Input.mousePosition;
        Ray ray =Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, 100f, ~ignoreMask)) 
        {
            hitPos = hit.point;
            
        }
        hitPos.y = (hit.point.y );
        Vector3 charLook = hitPos - character.transform.position;
        if (Vector3.Distance(charLook, character.transform.position) > 0.8f) 
        {
            character.transform.LookAt(charLook, Vector3.up);
        }
    }
     */


    private bool IsGrounded()
    {
        //Simple way to check for ground
        if (Physics.Raycast(transform.position, Vector3.down, 1.5f))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    
}

