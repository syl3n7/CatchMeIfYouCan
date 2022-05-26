using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 10f;

    [SerializeField]
    private float _gravity = 9.81f;

    [SerializeField]
    private float _jumpSpeed = 2f;

    [SerializeField]
    private float _doubleJumpMultiplier = 1f;

    private CharacterController _controller;

    private float _directionY;

    private bool _doubleJump = false;

    private float boostTimer = 0f;
    private bool boosting = false;

    private float boostJumpTimer = 0f;
    private bool boostingJump = false;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(verticalInput, 0, horizontalInput); // corrigi a direcao do vert / horiz /by syl3n7

        if (_controller.isGrounded)
        {
            _doubleJump = true;

            if (Input.GetButtonDown("Jump"))
            {
                _directionY = _jumpSpeed;
            }
        }
        else
        {
            if (Input.GetButtonDown("Jump") && _doubleJump)
            {
                _directionY = _jumpSpeed * _doubleJumpMultiplier;
                _doubleJump = false;
            }
        }

        _directionY -= _gravity * Time.deltaTime;

        direction.y = _directionY;

        _controller.Move(direction * _moveSpeed * Time.deltaTime);

        if(boosting)
        {
            boostTimer += Time.deltaTime;
            if(boostTimer >= 3f)
            {
                _moveSpeed = 10f;
                boostTimer = 0f;
                boosting = false;
            }
        }

        if(boostingJump)
        {
            boostJumpTimer += Time.deltaTime;
            if(boostJumpTimer >= 3f)
            {
                _jumpSpeed = 2f;
                boostJumpTimer = 0f;
                boostingJump = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "SpeedBoost1" || other.gameObject.name == "SpeedBoost2")
        {
            boosting = true;
            _moveSpeed = 15f;
            Destroy(other.gameObject);
        }

        if(other.gameObject.name == "JumpBoost1")
        {
            boostingJump = true;
            _jumpSpeed = 4f;
            Destroy(other.gameObject);
        }
    }
}
