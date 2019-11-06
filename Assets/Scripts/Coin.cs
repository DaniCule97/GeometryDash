using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    //Las monedas se utilizarian para desbloquear niveles bonus

    [SerializeField] private int id;
    private bool catched;
    public int Id { get => id; set => id = value; }

    private void Start()
    {
        catched = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            catched = true;
            AudioSource.PlayClipAtPoint(GetComponent<AudioSource>().clip, Camera.main.transform.position);
            this.gameObject.SetActive(false);
        }
    }

    public void Visible()
    {
        catched = false;
        this.gameObject.SetActive(true);
    }
}
