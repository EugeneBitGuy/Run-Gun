using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject inGame_UI;
    void Start()
    {
        Time.timeScale = 1;
        inGame_UI.SetActive(true);
    }
    void Update()
    {
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        losePanel.SetActive(true);
        inGame_UI.SetActive(false);
    }

    public void Restart()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

}
