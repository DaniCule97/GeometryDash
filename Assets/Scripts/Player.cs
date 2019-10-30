using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float speed = 10;
    private float jumpForce = 5;
    float playerHeight;
    bool grounded;

    public enum GameMode { NORMAL, GRAVITYALTERED, DIRECTIONINVERTED, FLYING, GRAVITYCHANGES, GRAVITYCHANGESFLYING};
    GameMode actualGameMode;

    Vector3 startPosition;

    public Vector3 StartPosition { get => startPosition; set => startPosition = value; }

    void Start()
    {
        StartPosition = transform.position;
        playerHeight = GetComponent<Collider2D>().bounds.size.y;
        actualGameMode = GameMode.NORMAL;
    }
    
    void Update()
    {
        //Usado para actualizar el % de partida completado
        FindObjectOfType<GameController>().SendMessage("ChangeText", transform.position.x);
    }
    private void FixedUpdate()
    {
        switch(actualGameMode)
        {
            case GameMode.NORMAL:
                transform.Translate(speed * Time.deltaTime, 0, 0);
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    /*
                    RaycastHit2D hit =
                               Physics2D.Raycast(
                                   transform.position,
                                   new Vector2(0, -1));

                    float floorDistance = hit.distance;
                    bool touchingGround = floorDistance < playerHeight * 0.6f;
                    if (touchingGround)*/
                    if (grounded)
                    {
                        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                    }
                }
                break;
            case GameMode.GRAVITYALTERED:
                break;
            case GameMode.FLYING:
                break;
            case GameMode.DIRECTIONINVERTED:
                break;
            case GameMode.GRAVITYCHANGES:
                break;
            case GameMode.GRAVITYCHANGESFLYING:
                break;
        }
        
    }

    public void ChangeGameMode(GameMode newMode)
    {
        switch (actualGameMode)
        {
            case GameMode.NORMAL:
                break;
            case GameMode.GRAVITYALTERED:
                break;
            case GameMode.FLYING:
                break;
            case GameMode.DIRECTIONINVERTED:
                break;
            case GameMode.GRAVITYCHANGES:
                break;
            case GameMode.GRAVITYCHANGESFLYING:
                break;
        }
    }

    public void ResetPosition()
    {
        transform.position = StartPosition;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = false;
        }
    }
}
