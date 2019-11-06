using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] TMP_Text textStatus;
    int actualLevel;
    [SerializeField] Transform finish;

    [SerializeField] Coin coin1;
    [SerializeField] Coin coin2;
    [SerializeField] Coin coin3;

    float endX;

    public float EndX { get => endX; set => endX = value; }

    void Start()
    {
        actualLevel = FindObjectOfType<GameStatus>().ActualLevel;
        endX = finish.position.x;
    }
    void Update()
    {
    }

    private void ChangeText(float playerX)
    {
        //   Debug.Log(( playerX / endX * 100).ToString("0") + "%");
        if (playerX != 0)
            textStatus.text = (playerX / endX * 100).ToString("0") + "%";
        else
            textStatus.text = "0%";
    }

    private void ResetCoins()
    {
        coin1.Visible();
        coin2.Visible();
        coin3.Visible();
    }

    private void NextLevel()
    {
        //Aqui se guardaria el progreso con las monedas conseguidas a parte tambien llevaria un control del maximo porcentaje 
        
        actualLevel++;
        if (actualLevel > FindObjectOfType<GameStatus>().HighestLevel)
            actualLevel = 1;
        FindObjectOfType<GameStatus>().ActualLevel = actualLevel;
        SceneManager.LoadScene("Level" + actualLevel);
    }

    private void ResetLevel()
    {
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().Play();
        ResetCoins();
    }
}
