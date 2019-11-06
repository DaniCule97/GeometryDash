using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatus : MonoBehaviour
{
    private int actualLevel;
    private int highestLevel;
    
    public int ActualLevel { get => actualLevel; set => actualLevel = value; }
    public int HighestLevel { get => highestLevel; }

    private void Awake()
    {
        int gameStatusCount =
            FindObjectsOfType<GameStatus>().Length;
        if (gameStatusCount > 1) { Destroy(gameObject); }
        else { DontDestroyOnLoad(gameObject); }

        actualLevel = 1;
    }
    private void Start()
    {
        highestLevel = 2;
    }
}
