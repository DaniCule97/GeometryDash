using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperJump : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(GetComponent<AudioSource>().clip, Camera.main.transform.position);
            FindObjectOfType<Player>().SendMessage("SuperJump");
        }
    }
}
