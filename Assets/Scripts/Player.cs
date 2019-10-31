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
        Debug.Log(gravityForce);
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
                if (Input.GetKeyDown(KeyCode.Space))
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                break;
            case GameMode.GRAVITYCHANGES:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    RaycastHit2D hit;
                    if (gravityInverted)
                        hit = Physics2D.Raycast(
                            transform.position,
                            new Vector2(0, 1));
                    else
                        hit = Physics2D.Raycast( 
                            transform.position,
                            new Vector2(0, -1));

                    float floorDistance = hit.distance;
                    bool touchingGround = floorDistance < playerHeight * 0.6f;
                    if (touchingGround)
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

    public void ResetPosition()
    {
        transform.position = StartPosition;
    }
}
