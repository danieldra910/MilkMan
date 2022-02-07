using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public GameObject milkman;

    public GameObject rottweiler;
    public GameObject puddle;
    public GameObject pitbull;
    public GameObject germanSheppard;

    public GameObject leftWarpNode;
    public GameObject rightWarpNode;

    public GameObject dogNodeStart;
    public GameObject dogNodeCenter;
    public GameObject dogNodeLeft;
    public GameObject dogNodeRight;

    public bool diedInLevel=false;

    public static int clearedLevels=0;
    int currentMilk=0;

    public enum DogMode
    {
        chase,scatter

    }

    public DogMode currentDogMode;

    public AudioSource milk;
    public int totalMilk;
    public int remainingMilk;
    public int milkCollectedOnThisLife;

    public static int playerOneScore=0;

    public static int playerTwoScore=0;

    public Text playerOneScoreText;
    public Text playerTwoScoreText;
    // Start is called before the first frame update
    void Awake()
    {
        puddle.GetComponent<EnemyController>().leaveHome=true;
        currentDogMode = DogMode.chase;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GotMilk()
    {
        totalMilk++;
        remainingMilk++;
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

        currentMilk++;

        remainingMilk--;
        milkCollectedOnThisLife++;

        int pitbullMilk=0;
        int germanMilk=0;

        if(diedInLevel==true)
        {
            pitbullMilk=12;
            germanMilk=32;
        }
        else
        {
            pitbullMilk=30;
            germanMilk=60;
        }
        if(milkCollectedOnThisLife>=pitbullMilk && !pitbull.GetComponent<EnemyController>().leftHome)
        {
            pitbull.GetComponent<EnemyController>().leaveHome=true;
        }
        if(milkCollectedOnThisLife>=germanMilk && !germanSheppard.GetComponent<EnemyController>().leftHome)
        {
            germanSheppard.GetComponent<EnemyController>().leaveHome=true;
        }
        if(currentMilk==289)
        {
            clearedLevels++;
            if(clearedLevels==1)
            {
                SceneManager.LoadScene("Testing2");
            }
            else if(clearedLevels==2)
            {
                SceneManager.LoadScene("Win");
            }

            
        }
    }
}
