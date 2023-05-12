using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody playerRB;
    private PlayerControl playerIA;
    
    [SerializeField]
    private float jumpForce, runningVelocity, walkingVelocity, currentVelocity;

    [SerializeField]
    private Transform groundCheck;

    [SerializeField]
    private LayerMask layerGround;

    [SerializeField]
    private bool grounded, running;

    private void Awake() 
    {
        playerRB = GetComponent<Rigidbody>();

        playerIA = new PlayerControl();
        playerIA.Movement.Enable();
        playerIA.Movement.Jump.performed += Jump;

        //playerIA.UI.Enable();
        playerIA.UI.Pause.performed += Pause;
    }

    private void Update()
    {
        grounded = Physics.CheckSphere(groundCheck.position, 0.01f, layerGround);


        Vector2 inputVector = playerIA.Movement.Movement.ReadValue<Vector2>();

        running = playerIA.Movement.Running.ReadValue<float>() > 0;
        //Debug.Log(playerIA.Movement.Running.ReadValue<float>());

        if (running) 
            currentVelocity = runningVelocity;
        else
            currentVelocity = walkingVelocity;

        if (grounded) 
            playerRB.velocity = transform.TransformDirection(new Vector3(inputVector. x, 0, inputVector.y) * currentVelocity);
        
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (grounded)
        {
            Debug.Log("Jump");
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);  
        }
    }

    private void Pause(InputAction.CallbackContext context)
    {
        Debug.Log("Pause");
    }

    private void OnDrawGizmos() 
    {
        //GroundCheck
        //Gizmos.DrawSphere(groundCheck.position, 0.01f);

    }


}
