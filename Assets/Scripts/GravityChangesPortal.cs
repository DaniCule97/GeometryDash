using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityChangesPortal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            FindObjectOfType<Player>().SendMessage("ChangeGameMode", Player.GameMode.GRAVITYCHANGES);
    }
}
