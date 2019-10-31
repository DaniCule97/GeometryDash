using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float speed = 10;
    private float jumpForce = 5;
    float playerHeight;
    float gravityForce;

    public enum GameMode { NORMAL, GRAVITYALTERED, DIRECTIONINVERTED, FLYING, GRAVITYCHANGES, GRAVITYCHANGESFLYING};
    GameMode actualGameMode;

    Vector3 startPosition;

    public Vector3 StartPosition { get => startPosition; set => startPosition = value; }

    void Start()
    {
        StartPosition = transform.position;
        playerHeight = GetComponent<Collider2D>().bounds.size.y;
        actualGameMode = GameMode.NORMAL;
        gravityForce = GetComponent<Rigidbody2D>().gravityScale;
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
                    RaycastHit2D hit =
                               Physics2D.Raycast(
                                   transform.position,
                                   new Vector2(0, -1));

                    float floorDistance = hit.distance;
                    bool touchingGround = floorDistance < playerHeight * 0.6f;
                    if (touchingGround)
                    {
                        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                    }
                }
                break;
            case GameMode.GRAVITYALTERED:
                transform.Translate(speed * Time.deltaTime, 0, 0);
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    RaycastHit2D hit =
                               Physics2D.Raycast(
                                   transform.position,
                                   new Vector2(0, 1));

                    float floorDistance = hit.distance;
                    bool touchingGround = floorDistance < playerHeight * 0.6f;
                    if (touchingGround)
                    {
                        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -jumpForce), ForceMode2D.Impulse);
                    }
                }
                break;
            case GameMode.FLYING:
                transform.Translate(speed * Time.deltaTime, 0, 0);
                if (Input.GetKeyDown(KeyCode.Space))
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -jumpForce), ForceMode2D.Impulse);
                break;
            case GameMode.DIRECTIONINVERTED:
                transform.Translate(-speed * Time.deltaTime, 0, 0);
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    RaycastHit2D hit =
                               Physics2D.Raycast(
                                   transform.position,
                                   new Vector2(0, -1));

                    float floorDistance = hit.distance;
                    bool touchingGround = floorDistance < playerHeight * 0.6f;
                    if (touchingGround)
                    {
                        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                    }
                }
                break;
            case GameMode.GRAVITYCHANGES:

                break;
            case GameMode.GRAVITYCHANGESFLYING:
                break;
        }
        
    }

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
            case GameMode.DIRECTIONINVERTED:
                GetComponent<Rigidbody2D>().gravityScale = gravityForce;
                break;
            case GameMode.GRAVITYCHANGES:
                GetComponent<Rigidbody2D>().gravityScale = gravityForce;
                break;
            case GameMode.GRAVITYCHANGESFLYING:
                GetComponent<Rigidbody2D>().gravityScale = gravityForce;
                break;
        }
    }

    public void ResetPosition()
    {
        transform.position = StartPosition;
    }
}
