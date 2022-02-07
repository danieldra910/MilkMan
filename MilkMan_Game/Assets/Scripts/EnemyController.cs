using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public bool isScared =false;

    public GameObject [] scatterNodes;

    public int scatterNodeIndex;

    public bool leftHome=false;

    // Start is called before the first frame update
    void Awake()
    {
        scatterNodeIndex=0;

        gameManager=GameObject.Find("GameManager").GetComponent<GameManager>();


        movementScript = GetComponent<MovementScript>();

        if(dogType == DogType.rott)
        {
            dogNodeState = DogNodeStateEnum.startNode;
            respawnState = DogNodeStateEnum.centerNode;
            startingNode = dogNodeStart;
            leaveHome = true;
            leftHome=true;
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

        if(movementScript.currentNode.GetComponent<NodeController>().isSideNode)
        {
            movementScript.SetSpeed(1);
        }
        else
        {
            movementScript.SetSpeed(2);
        }
    }

    public void ReachedCenterOfNode (NodeController nodeController)
    {
        if(dogNodeState == DogNodeStateEnum.movingInNodes)
        {
            leftHome=true;
            // Scatter mode
            if(gameManager.currentDogMode == GameManager.DogMode.scatter)
            {
                //reached scatter node
                DetermineScatterModeDirection();
            }
            //Scared mode
            else if(isScared==true)
            {
                string direction = RandomDirection();
                movementScript.SetDirection(direction);

            }
            //Chase Mode
            else
            {

                if(dogType == DogType.rott)
                {
                    DetermineRottWeilerDirection();
                }

                else if(dogType==DogType.puddle)
                {
                    DeterminePuddleDirection();
                }
                else if(dogType==DogType.pitbull)
                {
                    DeterminePitbullDirection();
                }
                else if(dogType==DogType.german)
                {
                    DetermineGermanSheppardDirection();
                }

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
        string milkmanDirection = gameManager.milkman.GetComponent<MovementScript>().previousDirection;
        float distanceBetweenNodes = 0.28f;
        Vector2 target = gameManager.milkman.transform.position;

        if(milkmanDirection == "left")
        {
            target.x -= distanceBetweenNodes*2;
        }
        else if(milkmanDirection == "right")
        {
            target.x += distanceBetweenNodes*2;
        }
        else if(milkmanDirection == "up")
        {
            target.y += distanceBetweenNodes*2;
        }
        else if(milkmanDirection == "down")
        {
            target.y -= distanceBetweenNodes*2;
        }
        string direction = GetClosestDirection(target);
        movementScript.SetDirection(direction);
    }

    void DeterminePitbullDirection()
    {
        string milkmanDirection = gameManager.milkman.GetComponent<MovementScript>().previousDirection;
        float distanceBetweenNodes = 0.28f;
        Vector2 target = gameManager.milkman.transform.position;

        if(milkmanDirection == "left")
        {
            target.x -= distanceBetweenNodes*2;
        }
        else if(milkmanDirection == "right")
        {
            target.x += distanceBetweenNodes*2;
        }
        else if(milkmanDirection == "up")
        {
            target.y += distanceBetweenNodes*2;
        }
        else if(milkmanDirection == "down")
        {
            target.y -= distanceBetweenNodes*2;
        }
        GameObject rott = gameManager.rottweiler;
        float xDistance = target.x - rott.transform.position.x;
        float yDistance = target.y - rott.transform.position.y;

        Vector2 pitbullTarget = new Vector2(target.x + xDistance , target.y + yDistance);
        string distance =GetClosestDirection(pitbullTarget);
        movementScript.SetDirection(distance);
    }

    void DetermineGermanSheppardDirection()
    {
        float distance = Vector2.Distance(gameManager.milkman.transform.position , transform.position);
        float distanceBetweenNodes = 0.28f;

        if(distance<0)
        {
            distance*=-1;
        }
        //within 8 nodes of player
        if(distance <= distanceBetweenNodes*8)
        {
            DetermineRottWeilerDirection();
        }
        else
        {
            DetermineScatterModeDirection();
        }
    }

    string RandomDirection()
    {
        List<string> possibleDirections = new List<string>();
        NodeController nodeController = movementScript.currentNode.GetComponent<NodeController>();

        if(nodeController.canMoveDown && movementScript.previousDirection !="up")
        {
            possibleDirections.Add("down");
        }
        if(nodeController.canMoveUp && movementScript.previousDirection !="down")
        {
            possibleDirections.Add("up");
        }
        if(nodeController.canMoveRight && movementScript.previousDirection !="left")
        {
            possibleDirections.Add("right");
        }
        if(nodeController.canMoveLeft && movementScript.previousDirection !="right")
        {
            possibleDirections.Add("left");
        }
        string direction = "";
        int RandomDirectionIndex = Random.Range(0,possibleDirections.Count-1);
        direction = possibleDirections[RandomDirectionIndex];

        return direction;
    }

    void DetermineScatterModeDirection()
    {
        if(transform.position.x == scatterNodes[scatterNodeIndex].transform.position.x && transform.position.y == scatterNodes[scatterNodeIndex].transform.position.y )
        {
            scatterNodeIndex++;

            if(scatterNodeIndex == scatterNodes.Length-1)
            {
                scatterNodeIndex=0;
            }
        }
        string direction = GetClosestDirection (scatterNodes[scatterNodeIndex].transform.position);
        movementScript.SetDirection(direction);
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