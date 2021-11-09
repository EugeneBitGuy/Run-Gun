using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGame_UI : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Text healthBar;
    [SerializeField] private Text scoreBar;
    public static InGame_UI Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        healthBar.text = player.GetComponent<PlayerController>().Hp.ToString();
    }

    private void Update()
    {
        scoreBar.text = ((int)player.transform.position.z / 2).ToString();
    }

    public void UpdateHealthBar()
    {
        healthBar.text = player.GetComponent<PlayerController>().Hp.ToString();
    }

}
