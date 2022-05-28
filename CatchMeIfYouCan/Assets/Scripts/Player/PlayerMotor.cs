using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private InputManager inputManager;
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    public float speed = 5f;
    public float gravity = -100f;
    public float jumpHeight = 1.5f;

    [SerializeField]
    private float _doubleJumpMultiplier = 2f;
    private bool _doubleJump = false;

    private float boostTimer = 0f;
    private bool boosting = false;

    private float boostJumpTimer = 0f;
    private bool boostingJump = false;

    // Start is called before the first frame update
    void Start()
    {
        inputManager = GetComponent<InputManager>();
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;

        if (boosting)
        {
            boostTimer += Time.deltaTime;
            if (boostTimer >= 3f)
            {
                speed = 10f;
                boostTimer = 0f;
                boosting = false;
            }
        }

        if (boostingJump)
        {
            boostJumpTimer += Time.deltaTime;
            if (boostJumpTimer >= 3f)
            {
                jumpHeight = 1.5f;
                _doubleJumpMultiplier = 2f;
                boostJumpTimer = 0f;
                boostingJump = false;
            }
        }

    }

    //receive the inputs for our InputManager.cs and apply them to our character controller.
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;


        if(isGrounded && playerVelocity.y < 0)
            playerVelocity.y =  -2f;
        controller.Move(playerVelocity * Time.deltaTime);
        
    }

    public void Jump()
    {
        if(isGrounded)
        {
            _doubleJump = true;
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
        else
        {
            if (_doubleJump)
            {
                playerVelocity.y = speed * _doubleJumpMultiplier;
                _doubleJump = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "SpeedBoost1" || other.gameObject.name == "SpeedBoost2")
        {
            boosting = true;
            speed = 15f;
            Destroy(other.gameObject);
        }

        if (other.gameObject.name == "JumpBoost1")
        {
            boostingJump = true;
            jumpHeight = 4f;
            _doubleJumpMultiplier = 3f;
            Destroy(other.gameObject);
        }
    }
}
