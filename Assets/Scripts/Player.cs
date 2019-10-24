using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float speed = 10;
    private float jumpForce = 5;
    float playerHeight;

    Vector3 startPosition;
    void Start()
    {
        startPosition = transform.position;
        playerHeight = GetComponent<Collider2D>().bounds.size.y;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
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
    }
    public void ResetPosition()
    {
        transform.position = startPosition;
    }
}
