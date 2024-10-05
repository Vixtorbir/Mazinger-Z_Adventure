using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 6f;
    public float runSpeed = 8f;
    public float jumpImpulse = 10f;

    public bool isInContactWithTubo2 = false;  // Track contact with 'tubo2'

    Vector2 moveInput;
    TouchingDirections touchingDirections;

    AudioManager audioManager;

    private bool canChangeScene = false; // Variable para controlar el cambio de escena
    private float sceneChangeDelay = 1f; // Tiempo de espera antes de cambiar de escena
    private float sceneChangeTimer = 0f; // Temporizador para el cambio de escena

    public float CurrentMoveSpeed
    {
        get
        {
            if (IsMoving && !touchingDirections.IsOnWall)
            {
                return IsRunning ? runSpeed : walkSpeed;
            }
            else
            {
                return 2;
            }
        }
    }

    [SerializeField]
    private bool _isMoving = false;

    public bool IsMoving
    {
        get { return _isMoving; }
        private set
        {
            _isMoving = value;
            animaton.SetBool("IsMoving", value);
        }
    }

    [SerializeField]
    private bool _isRunning = false;

    public bool IsRunning
    {
        get { return _isRunning; }
        set
        {
            _isRunning = value;
            animaton.SetBool("IsRunning", value);
        }
    }

    Rigidbody2D rb;
    Animator animaton;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animaton = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);
        animaton.SetFloat("y Velocity", rb.velocity.y);

        // Verifica si el jugador está en contacto con 'tubo2' y tiene suficientes puntos
        if (isInContactWithTubo2 && ScoreManager.instance.GetScore() >= 50)
        {
            // Reproduce el sonido solo si no se ha cambiado la escena
            if (!canChangeScene)
            {
                audioManager.PlaySfx(audioManager.tubo);
                canChangeScene = true; // Permite el cambio de escena
                sceneChangeTimer = 0f; // Reinicia el temporizador
            }

            // Si ya ha pasado el tiempo, cambia la escena
            sceneChangeTimer += Time.deltaTime;
            if (sceneChangeTimer >= sceneChangeDelay)
            {
                ChangeToVictoryScreen();
            }
        }
        else
        {
            canChangeScene = false; // Resetea el cambio de escena si no está en contacto
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        IsMoving = moveInput != Vector2.zero;
        SetFacingDirection(moveInput);
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsRunning = true;
        }
        else if (context.canceled)
        {
            IsRunning = false;
        }
    }

    public void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveInput.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && touchingDirections.IsGrounded && !touchingDirections.IsOnWall)
        {
            audioManager.PlaySfx(audioManager.jump);
            animaton.SetTrigger("jump");
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player touches 'tubo2'
        if (collision.CompareTag("tubo2"))
        {
            isInContactWithTubo2 = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Check if the player exits contact with 'tubo2'
        if (collision.CompareTag("tubo2"))
        {
            isInContactWithTubo2 = false;
        }
    }

    private void ChangeToVictoryScreen()
    {
        // Change the scene to the victory screen
        SceneManager.LoadScene("VictoryScene");
    }
}
