using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject milkman;

    public GameObject leftWarpNode;
    public GameObject rightWarpNode;

    public GameObject dogNodeStart;
    public GameObject dogNodeCenter;
    public GameObject dogNodeLeft;
    public GameObject dogNodeRight;

    public AudioSource milk;
    public int currentMilk;

    public static int playerOneScore=0;

    public static int playerTwoScore=0;

    public Text playerOneScoreText;
    public Text playerTwoScoreText;
    // Start is called before the first frame update
    void Awake()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore (int amount)
    {
        playerOneScore+= amount;
        playerOneScoreText.text = "Player 1: " +playerOneScore.ToString();
    }

    public void CollectedMilk(NodeController nodeController)
    {
        milk.Play();
        AddScore(10);
    }
}
