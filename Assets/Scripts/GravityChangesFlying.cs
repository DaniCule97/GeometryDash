﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityChangesFlying : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            FindObjectOfType<Player>().SendMessage("ChangeGameMode", Player.GameMode.GRAVITYCHANGESFLYING);
            AudioSource.PlayClipAtPoint(GetComponent<AudioSource>().clip, Camera.main.transform.position);
        }
    }
}
