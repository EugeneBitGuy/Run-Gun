using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private static bool isGameOver;
    void Start()
    {
        isGameOver = false;
    }
    void Update()
    {
        if (isGameOver)
        {
            Time.timeScale = 0;
            Debug.Log("GameOver");
        }
    }

    public static void GameOver()
    {
        isGameOver = true;
    }

}
