using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float speed = 10;
    private float jumpForce = 5.1f;

    public LayerMask groundLayer;
    float playerHeight;
    float gravityForce;
    bool gravityInverted;

    public enum GameMode { NORMAL, GRAVITYALTERED, FLYING, GRAVITYCHANGES, GRAVITYCHANGESFLYING};
    [SerializeField] GameMode actualGameMode;

    Vector3 startPosition;

    public Vector3 StartPosition { get => startPosition; set => startPosition = value; }

    void Start()
    {
        StartPosition = transform.position;
        playerHeight = GetComponent<Collider2D>().bounds.size.y;
        gravityForce = GetComponent<Rigidbody2D>().gravityScale;
        ChangeGameMode(actualGameMode);
    }
    
    void Update()
    {
        //Usado para actualizar el % de partida completado
        FindObjectOfType<GameController>().SendMessage("ChangeText", transform.position.x);
    }
    private void FixedUpdate()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);
        switch (actualGameMode)
        {
            case GameMode.NORMAL:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (isGrounded(true))
                    {
                        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                    }
                }
                break;
            case GameMode.GRAVITYALTERED:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (isGrounded(false))
                    {
                        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -jumpForce), ForceMode2D.Impulse);
                    }
                }
                break;
            case GameMode.FLYING:
                if (Input.GetKeyDown(KeyCode.Space))
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                break;
            case GameMode.GRAVITYCHANGES:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (isGrounded(!gravityInverted))
                    {
                        gravityForce = -gravityForce;
                        GetComponent<Rigidbody2D>().gravityScale = gravityForce;
                        gravityInverted = !gravityInverted;
                    }
                }
                break;
            case GameMode.GRAVITYCHANGESFLYING:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    gravityForce = -gravityForce;
                    GetComponent<Rigidbody2D>().gravityScale = gravityForce;
                }
                break;
        }
        
    }
    //Lo hago asi porque mas adelante aqui tambien se cambiara como se ve el personaje
    public void ChangeGameMode(GameMode newMode)
    {
        actualGameMode = newMode;
        switch (newMode)
        {
            case GameMode.NORMAL:
                GetComponent<Rigidbody2D>().gravityScale = gravityForce;
                break;
            case GameMode.GRAVITYALTERED:
                GetComponent<Rigidbody2D>().gravityScale = -gravityForce;
                break;
            case GameMode.FLYING:
                GetComponent<Rigidbody2D>().gravityScale = gravityForce;
                break;
            case GameMode.GRAVITYCHANGES:
                gravityInverted = false;
                GetComponent<Rigidbody2D>().gravityScale = gravityForce;
                break;
            case GameMode.GRAVITYCHANGESFLYING:
                gravityInverted = false;
                GetComponent<Rigidbody2D>().gravityScale = gravityForce;
                break;
        }
    }

    bool isGrounded(bool downDirection)
    {
        Vector2 position = transform.position;
        Vector2 direction = downDirection? Vector2.down: Vector2.up;
        float distance = 0.7f;

        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        if (hit.collider != null)
        {
            return true;
        }

        return false;
        /*
        RaycastHit2D hit = Physics2D.Raycast(
                            transform.position,
                            new Vector2(0, downDirection? -1: 1));

                    float floorDistance = hit.distance;
        return floorDistance < playerHeight * 0.6f;
        */
    }
    public void SuperJump() //Only used on normal mode and Inverted Gravity
    {
        switch (actualGameMode)
        {
            case GameMode.NORMAL:
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce*1.5f), ForceMode2D.Impulse);
                break;
            case GameMode.GRAVITYALTERED:
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -jumpForce*1.5f), ForceMode2D.Impulse);
                break;
        }
    }
    public void ResetPosition()
    {
        AudioSource.PlayClipAtPoint(GetComponent<AudioSource>().clip, Camera.main.transform.position);
        FindObjectOfType<GameController>().SendMessage("ResetLevel");
        transform.position = StartPosition;
    }
}
