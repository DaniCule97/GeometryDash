using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatus : MonoBehaviour
{
    private int actualLevel;
    private int points;
    private int highestLevel;
    
    public int Points { get => points; set => points = value; }
    public int ActualLevel { get => actualLevel; set => actualLevel = value; }
    public int HighestLevel { get => highestLevel; }

    private void Awake()
    {
        int gameStatusCount =
            FindObjectsOfType<GameStatus>().Length;
        if (gameStatusCount > 1) { Destroy(gameObject); }
        else { DontDestroyOnLoad(gameObject); }
    }
    private void Start()
    {
        highestLevel = 5;
        points = 0;
        actualLevel = 1;
    }
}
