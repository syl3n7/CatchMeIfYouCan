using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMotor : MonoBehaviour
{
    [SerializeField]
    public Transform p;

    [Header("PlayerMovement")]
    private InputManager inputManager;
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    public float speed;
    public float gravity;
    public float jumpHeight;
    public float idle;
    public float walk;
    public float run;

    [SerializeField]
    private float _doubleJumpMultiplier;
    private bool _doubleJump = false;

    private float boostTimer = 0f;
    private bool boosting = false;

    private float boostJumpTimer = 0f;
    private bool boostingJump = false;

    private float changeSpeedTimerWalk = 0f;
    private float changeSpeedTimerRun = 0f;
    private bool changeSpeedWalk = false;
    private bool changeSpeedRun = false;

    [Header("Death")]
    public GameObject deathMenu;
    [SerializeField]
    public bool death = false;

    [Header("Audio")]
    [SerializeField]
    private AudioSource stepsAudio;

    [SerializeField]
    private AudioSource jumpAudio;

    [Header("Animation")]
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        inputManager = GetComponent<InputManager>();
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        deathMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrounded && !inputManager.onFoot.Move.IsPressed())
        {
            speed = idle;
            anim.SetFloat("Speed", 0f);
        }

        if (isGrounded && inputManager.onFoot.Move.IsPressed())
        {
            speed = walk;
            if (boosting)
            {
                speed *= walk;
            }

            if (changeSpeedWalk)
            {
                speed = walk - 2f;
            }
            anim.SetFloat("Speed", 0.5f);
        }

        if (isGrounded && inputManager.onFoot.Run.IsPressed())
        {
            speed = run;
            if (boosting)
            {
                speed *= run;
            }

            if (changeSpeedRun)
            {
                speed = run - 2f;
            }

            anim.SetFloat("Speed", 1f);
        }

        isGrounded = controller.isGrounded;

        if (isGrounded && inputManager.onFoot.Move.IsPressed() && stepsAudio.isPlaying == false)
        {
            stepsAudio.Play();
        }
        else
        {
            if (!isGrounded)
            {
                stepsAudio.Stop();
            }

            if (!inputManager.onFoot.Move.IsPressed())
            {
                stepsAudio.Stop();
            }
        }

        if (!isGrounded && jumpAudio.isPlaying == false)
        {
            jumpAudio.Play();
        }
        else
        {
            if (isGrounded)
            {
                jumpAudio.Stop();
            }
        }

        if (boosting)
        {
            boostTimer += Time.deltaTime;
            if (boostTimer >= 3f)
            {
                speed = run;
                boostTimer = 0f;
                boosting = false;
            }
        }

        if (boostingJump)
        {
            boostJumpTimer += Time.deltaTime;
            if (boostJumpTimer >= 3f)
            {
                jumpHeight = 3;
                _doubleJumpMultiplier = 5f;
                boostJumpTimer = 0f;
                boostingJump = false;
            }
        }

        if (changeSpeedWalk)
        {
            changeSpeedTimerWalk += Time.deltaTime;
            if (changeSpeedTimerWalk >= 1.5f)
            {
                speed = walk;
                changeSpeedWalk = false;
            }
        }

        if (changeSpeedRun)
        {
            changeSpeedTimerRun += Time.deltaTime;
            if (changeSpeedTimerRun >= 1.5f)
            {
                speed = run;
                changeSpeedRun = false;
            }
        }

        if (death)
        {
            deathMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void DeathMenu()
    {
        deathMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        death = false;
        speed = idle;
        anim.SetFloat("Speed", 0f);
    }
    //receive the inputs for our InputManager.cs and apply them to our character controller.
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;

        if (isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            _doubleJump = true;
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
        else
        {
            if (_doubleJump)
            {
                playerVelocity.y = speed * _doubleJumpMultiplier;
                changeSpeedWalk = true;
                changeSpeedRun = true;
                jumpAudio.Play();
                _doubleJump = false;
            }
        }
    }

     
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Terrain")
        {
            anim.SetTrigger("Death");
            gameObject.transform.position = p.position + Vector3.up;
            death = true;
        }

        if (hit.gameObject.tag == "CheckPoint")
        {
            p = hit.transform;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "SpeedBoost1" || other.gameObject.name == "SpeedBoost2")
        {
            boosting = true;
            Destroy(other.gameObject);
        }

        if (other.gameObject.name == "JumpBoost1")
        {
            boostingJump = true;
            jumpHeight = 6f;
            _doubleJumpMultiplier = 7f;
            Destroy(other.gameObject);
        }
    }
}
