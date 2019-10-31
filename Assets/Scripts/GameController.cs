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
    [SerializeField] Coin[] coins;
    
    float endX;

    public float EndX { get => endX; set => endX = value; }

    void Start()
    {
        endX = finish.position.x; //Coins lo inicializo a 3 porque todos los niveles tienen 3
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
    private void GetCoin(int coinID)
    {
        foreach(Coin c in coins)
        {
            if (c.Id == coinID)
                c.enabled = false;
        }
    }
    
    private void NextLevel()
    {
        actualLevel++;
        if (actualLevel > FindObjectOfType<GameStatus>().HighestLevel)
            actualLevel = 1;
        FindObjectOfType<GameStatus>().ActualLevel = actualLevel;
        SceneManager.LoadScene("Level" + actualLevel);

    }
}
