using System;
using System.Collections;
using PathCreation.Examples;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using DG.Tweening;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PathFollower pathFollower;
    
    [SerializeField] private float xRangeRight,xRangeLeft;
    [SerializeField] private float sensitivity;
    [SerializeField] private float speed;
    private float horizontalInput;
    private Vector3 movement;
    private Rigidbody rb;

    private float xRangeOffset;
  
    private float horizontal;
    private Vector3 prevPosition;


    public float controllSensitivity,rotationValue,rotationSpeed;
    private float playerRotation;
    
    public Text scoreUI;


    public float height = 0.5f;
    public float heightPadding = 0.05f;
    public LayerMask ground;
    public float maxGroundAngle = 120;
    public bool debug;

    private Vector2 input;
    private float angle;
    private float groundAngle;

    private Quaternion targetRotation;

    private Vector3 forward;
    private RaycastHit hitInfo,sideRayInfo;
    private bool grounded;
     


 

    // Update is called once per frame
    void Update()
    {

        HorizontalMovement(transform,20);
        ForwardMovement();
        RotationInput();
        Rotation(transform.GetChild(0).transform,rotationSpeed);
        CalculateForward();
        CalculateGroundAngle();
        CheckGround();
        ApplyGravity();
       
    }

 

    public void Rotation(Transform player, float easeRotation)
    {
        playerRotation = Mathf.Atan2(horizontal, 1);
        playerRotation = Mathf.Rad2Deg * playerRotation;
        playerRotation = Mathf.Clamp(playerRotation, -rotationValue, rotationValue);
        var targetRotation = Quaternion.Euler(0,playerRotation,0);
        player.localRotation =
            Quaternion.Slerp(player.localRotation, targetRotation, easeRotation * Time.deltaTime);
    }

    void RotationInput()
    {
        

        if (Input.GetMouseButtonDown(0))
        {
            prevPosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
           
            Vector3 touchDelta = Input.mousePosition - prevPosition;
            var positionDelta = touchDelta * controllSensitivity;
            positionDelta.x /= Screen.width / 2f;
            horizontal = positionDelta.x;
            prevPosition = Input.mousePosition;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            horizontal = 0;
        }
    }


    


    public void HorizontalMovement(Transform player,float easeValue)
    {
        horizontalInput = CrossPlatformInputManager.GetAxis("Horizontal") * sensitivity * Time.deltaTime;
        var localPosition = player.localPosition;
        Debug.DrawRay(transform.position,transform.right * 0.2f,Color.red);
        if (Physics.Raycast(player.transform.position, transform.right, out sideRayInfo, 5, ground))
        {
            xRangeOffset =  -0.7f;
        
        }
        else
        {
            xRangeOffset = xRangeRight;
           
        }

        
        movement.Set(Mathf.Clamp(movement.x+horizontalInput,xRangeLeft,xRangeOffset), localPosition.y,localPosition.z);
        transform.localPosition = movement;
        player.localPosition = Vector3.MoveTowards(player.localPosition, movement, Time.deltaTime * easeValue);
    }

 

    void ForwardMovement()
    {
        pathFollower.enabled = true;
        DOTween.To(() => pathFollower.speed, x => pathFollower.speed = x, speed, 1f);
    }

   
    
    

    void CalculateForward()
    {
        if (!grounded)
        {
            forward = transform.forward;
            return;
        }
        
        forward = Vector3.Cross(hitInfo.normal,-transform.right);
    }


    void CalculateGroundAngle()
    {
        if (!grounded)
        {
            groundAngle = 90;
            return;
        }

        groundAngle = Vector3.Angle(hitInfo.normal, transform.forward);
    }
    
    void CheckGround()
    {
        if (Physics.Raycast(transform.position, -Vector3.up, out hitInfo, height + heightPadding, ground))
        {
            if (Vector3.Distance(transform.position, hitInfo.point) < height)
            {
                transform.position = Vector3.Lerp(transform.position,transform.position + Vector3.up * height,5 * Time.deltaTime);
            }

            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }
    
    void ApplyGravity()
    {
        if (!grounded)
        {
            //transform.position += Physics.gravity * Time.deltaTime;
            transform.Translate(Vector3.down * 2.5f * Time.deltaTime);

            var dist = Vector3.Distance(transform.position, hitInfo.point);
            if (dist > 200)
            {
                gameObject.AddComponent<Rigidbody>();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("finalmomentum"))
        {
            StartCoroutine(ScoreUpdater());
        }
    }
    
    private IEnumerator ScoreUpdater()
    {
        
       
        var duration = 0;
        var displayScore = 0;
        var amount = 0;
        while(true)
        {
            if(displayScore > amount)
            {
                displayScore --;
               
            }
            

            yield return new WaitForSeconds(duration); 
        }

    }
}
