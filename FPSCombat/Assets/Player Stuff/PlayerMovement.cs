using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float runCost = 15f;
    private PlayerStats stats;

    private CharacterController controller;

    private Vector3 velocity;
    private float gravity = -20f;
    
    void Start()
    {
        controller = GetComponent<CharacterController>();
        stats = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {   
        //MOVEMENT HERE
        float x = Input.GetAxis("Horizontal"); // Side to Side movement
        float z = Input.GetAxis("Vertical"); // Forward Movement

        Vector3 move = transform.right * x + transform.forward * z; // moves in the direction of the camera in first person
        if (Input.GetKey(KeyCode.LeftShift) && stats.currentStamina > 0)
        {
            controller.Move(move * runSpeed * Time.deltaTime);
            stats.DrainStamina(runCost * Time.deltaTime);
        }
        else
        {
            controller.Move(move * walkSpeed * Time.deltaTime);
        } 


        
        
        // GRAVITY HERE
        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }
}
