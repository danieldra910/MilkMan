using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public enum DogNodeStateEnum
    {
        respawining,
        leftNode,
        rightNode,
        centerNode,
        startNode,
        movingInNodes
    }
    public DogNodeStateEnum dogNodeState;

    public enum DogType 
    {
        rott,
        puddle,
        pitbull,
        german
    }
    public DogType  dogType;

    public DogNodeStateEnum respawnState;

    public GameObject dogNodeStart;
    public GameObject dogNodeCenter;
    public GameObject dogNodeLeft;
    public GameObject dogNodeRight;

    public MovementScript movementScript;

    public GameObject startingNode;

    public bool leaveHome = false;

    public GameManager gameManager;

    public bool testRespawn = false;

    // Start is called before the first frame update
    void Awake()
    {
        gameManager=GameObject.Find("GameManager").GetComponent<GameManager>();


        movementScript = GetComponent<MovementScript>();

        if(dogType == DogType.rott)
        {
            dogNodeState = DogNodeStateEnum.startNode;
            respawnState = DogNodeStateEnum.centerNode;
            startingNode = dogNodeStart;
            leaveHome = true;
        }
        else if( dogType == DogType.puddle)
        {
            dogNodeState = DogNodeStateEnum.centerNode;
            startingNode = dogNodeCenter;
            respawnState = DogNodeStateEnum.centerNode;
        }
        else if (dogType == DogType.pitbull)
        {
            dogNodeState = DogNodeStateEnum.leftNode;
            startingNode = dogNodeLeft;
            respawnState = DogNodeStateEnum.leftNode;
        }
        else if( dogType == DogType.german)
        {
            dogNodeState = DogNodeStateEnum.rightNode;
            startingNode = dogNodeRight;
            respawnState = DogNodeStateEnum.rightNode;
        }
        movementScript.currentNode = startingNode; 
    }

    // Update is called once per frame
    void Update()
    {
        if(testRespawn == true)
        {
            leaveHome = false;
            dogNodeState = DogNodeStateEnum.respawining;
            testRespawn = false;
        }
    }

    public void ReachedCenterOfNode (NodeController nodeController)
    {
        if(dogNodeState == DogNodeStateEnum.movingInNodes)
        {
            if(dogType == DogType.rott)
            {
                DetermineRottWeilerDirection();
            }
        }
        else if(dogNodeState == DogNodeStateEnum.respawining)
        {
            string direction = "";

            if(transform.position.x == dogNodeStart.transform.position.x && transform.position.y == dogNodeStart.transform.position.y )
            {
                direction="down";
            }
            else if(transform.position.x == dogNodeCenter.transform.position.x && transform.position.y == dogNodeCenter.transform.position.y )
            {
                if(respawnState == DogNodeStateEnum.centerNode)
                {
                    dogNodeState=respawnState;
                }
                else if (respawnState == DogNodeStateEnum.leftNode)
                {
                    direction="left";
                }
                else if (respawnState == DogNodeStateEnum.rightNode)
                {
                    direction="right";
                }
            }
            else if ((transform.position.x == dogNodeLeft.transform.position.x && transform.position.y == dogNodeLeft.transform.position.y) || (transform.position.x == dogNodeRight.transform.position.x && transform.position.y == dogNodeRight.transform.position.y))
            {
                dogNodeState = respawnState;
            }

            else
            {
                direction = GetClosestDirection(dogNodeStart.transform.position);
            }
            movementScript.SetDirection(direction);

        }
        else
        {
            if(leaveHome==true)
            {
                if(dogNodeState == DogNodeStateEnum.leftNode)
                {
                    dogNodeState = DogNodeStateEnum.centerNode;
                    movementScript.SetDirection("right");
                }
                else if (dogNodeState == DogNodeStateEnum.rightNode)
                {
                    dogNodeState = DogNodeStateEnum.centerNode;
                    movementScript.SetDirection("left");
                }
                else if(dogNodeState == DogNodeStateEnum.centerNode)
                {
                    dogNodeState = DogNodeStateEnum.startNode;
                    movementScript.SetDirection("up");
                }
                else if (dogNodeState == DogNodeStateEnum.startNode)
                {
                    dogNodeState = DogNodeStateEnum.movingInNodes;
                    movementScript.SetDirection("left");
                }
            }
        }
    }

    void DetermineRottWeilerDirection()
    {
        string direction = GetClosestDirection(gameManager.milkman.transform.position);
        movementScript.SetDirection(direction);
    }

    void DeterminePuddleDirection()
    {

    }

    void DeterminePitbullDirection()
    {

    }

    void DetermineGermanSheppardDirection()
    {
        
    }

    string GetClosestDirection(Vector2 target)
    {
        float shortestDistance = 0;
        string lastMovingDirection = movementScript.previousDirection;
        string newDirection = "";
        NodeController nodeController = movementScript.currentNode.GetComponent<NodeController>();

        if(nodeController.canMoveUp && lastMovingDirection != "down")
        {
            GameObject nodeUp = nodeController.nodeUp;
            // distance between top node and player
            float distance = Vector2.Distance(nodeUp.transform.position, target);

            if(distance < shortestDistance || shortestDistance == 0)
            {
                shortestDistance = distance;
                newDirection = "up";
            }
        
        }
        if(nodeController.canMoveDown && lastMovingDirection != "up")
        {
            GameObject nodeDown = nodeController.nodeDown;
            float distance = Vector2.Distance(nodeDown.transform.position, target);

            if(distance < shortestDistance || shortestDistance == 0)
            {
                shortestDistance = distance;
                newDirection = "down";
            }
        
        }

        if(nodeController.canMoveLeft && lastMovingDirection != "right")
        {
            GameObject nodeLeft = nodeController.nodeLeft;
            float distance = Vector2.Distance(nodeLeft.transform.position, target);

            if(distance < shortestDistance || shortestDistance == 0)
            {
                shortestDistance = distance;
                newDirection = "left";
            }
        
        }

        if(nodeController.canMoveRight && lastMovingDirection != "left")
        {
            GameObject nodeRight = nodeController.nodeRight;
            float distance = Vector2.Distance(nodeRight.transform.position, target);

            if(distance < shortestDistance || shortestDistance == 0)
            {
                shortestDistance = distance;
                newDirection = "right";
            }
        
        }

        return newDirection;
    }


}