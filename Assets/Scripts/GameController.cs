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

    Coin[] coins;
    [SerializeField] Coin coin1;
    [SerializeField] Coin coin2;
    [SerializeField] Coin coin3;

    float endX;

    public float EndX { get => endX; set => endX = value; }

    void Start()
    {
        coins = new Coin[3];
        coins[0] = coin1;
        coins[1] = coin2;
        coins[2] = coin3;
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

    private void ResetCoins()
    {
        foreach (Coin c in coins)
        {
            c.Visible();
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
