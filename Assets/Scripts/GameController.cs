using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    int actualLevel;
    [SerializeField] TMP_Text textStatus;
    [SerializeField] Transform finish;

    float playerX;
    float endX;
    int coins;

    void Start()
    {
        playerX = 0;//GetComponent<Player>().StartPosition.x;
        endX = finish.position.x;
        coins = 3; //Coins lo inicializo a 3 porque todos los niveles tienen 3
    }
    void Update()
    {
        playerX = GetComponent<Player>().transform.position.x;
        if (playerX != 0)
            textStatus.text = (endX/playerX * 100).ToString("%,%%") + "%";
        else
            textStatus.text = "0%";
    }
    private void GetCoin()
    {
        coins--;
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
