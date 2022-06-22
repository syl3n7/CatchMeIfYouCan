using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerMotor : MonoBehaviour
{
    [SerializeField]
    public Transform p;
    public GameObject[] balls;
    public GameObject[] balls2;
    public GameObject[] balls3;

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
    public TextMeshProUGUI deathCounter;
    private int counterDeath = 0;

    [Header("Audio")]
    [SerializeField]
    private AudioSource stepsAudio;

    [SerializeField]
    private AudioSource jumpAudio;

    [Header("Animation")]
    private Animator anim;

    private void Start()
    {
        inputManager = GetComponent<InputManager>();
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        deathMenu.SetActive(false);
    }

    private void Update()
    {
        deathCounter.text = "Deaths: " + counterDeath.ToString();
        
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
                speed *= 2;
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
            if (boostTimer >= 4f)
            {
                speed = run;
                boostTimer = 0f;
                boosting = false;
            }
        }

        if (boostingJump)
        {
            boostJumpTimer += Time.deltaTime;
            if (boostJumpTimer >= 2f)
            {
                jumpHeight = 3;
                _doubleJumpMultiplier = 4f;
                boostJumpTimer = 0f;
                boostingJump = false;
            }
        }

        if (changeSpeedWalk)
        {
            changeSpeedTimerWalk += Time.deltaTime;
            if (changeSpeedTimerWalk >= 2f)
            {
                speed = walk;
                changeSpeedWalk = false;
            }
        }

        if (!changeSpeedRun)
        {
            
        }
        else
        {
            changeSpeedTimerRun += Time.deltaTime;
            if (!(changeSpeedTimerRun >= 2f))
            {

            }
            else
            {
                speed = run;
                changeSpeedRun = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if (!deathMenu.activeSelf)
        {

        }
        else
        {
            gameObject.transform.position = p.position + Vector3.up + Vector3.up;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void DeathMenu()
    {
        deathMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        ++counterDeath;
        anim.SetTrigger("Death");
        boosting = false;
        boostingJump = false;
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
        if (!isGrounded)
        {
            if (!_doubleJump)
            {

            }
            else
            {
                playerVelocity.y = speed * _doubleJumpMultiplier;
                changeSpeedWalk = true;
                changeSpeedRun = true;
                jumpAudio.Play();
                _doubleJump = false;
            }
        }
        else
        {
            _doubleJump = true;
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);

        }
    }

     
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!hit.gameObject.CompareTag("Finnish"))
        {

        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Finnish");
        }

        if (!hit.gameObject.CompareTag("Terrain"))
        {

        }
        else
        {
            anim.SetTrigger("Death");
            deathMenu.SetActive(true);
        }

        if (!hit.gameObject.CompareTag("CheckPoint"))
        {

        }
        else
        {
            p = hit.transform;
        }

        if (!hit.gameObject.CompareTag("Balls"))
        {

        }
        else
        {
            foreach (var ball in balls)
            {
                ball.SetActive(true);
            }
        }

        if (!hit.gameObject.CompareTag("Balls2"))
        {

        }
        else
        {
            foreach (var ball in balls2)
            {
                ball.SetActive(true);
            }
        }

        if (!hit.gameObject.CompareTag("Balls3"))
        {

        }
        else
        {
            foreach (var ball in balls3)
            {
                ball.SetActive(true);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("SpeedBoost"))
        {

        }
        else
        {
            boosting = true;
            Destroy(other.gameObject);
        }

        if (!other.gameObject.CompareTag("JumpBoost"))
        {

        }
        else
        {
            boostingJump = true;
            jumpHeight = 3f;
            _doubleJumpMultiplier = 3f;
            Destroy(other.gameObject);
        }
    }
}
