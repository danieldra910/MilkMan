using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{

    public GameManager gameManager;

    public GameObject currentNode;
    public float speed= 3.0f;

    public string direction="";
    public string previousDirection="";

    public bool warp=true;

    public bool isDog = false;

    // Start is called before the first frame update
    void Awake()
    {
        previousDirection = "left";
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        NodeController currentNodeController=currentNode.GetComponent <NodeController>();

        transform.position=Vector2.MoveTowards(transform.position,currentNode.transform.position, speed*Time.deltaTime);

        bool reverse =false;
        if((direction == "left" && previousDirection =="right") || (direction =="right" && previousDirection =="left") || (direction == "up" && previousDirection == "down") || (direction == "down" && previousDirection == "up") )
        {
            reverse=true;
        }

        if((transform.position.x==currentNode.transform.position.x && transform.position.y == currentNode.transform.position.y ) || reverse==true)
        {
            if( isDog==true)
            {
                GetComponent<EnemyController>().ReachedCenterOfNode(currentNodeController);
            }
            if(currentNodeController.isWarpLeft && warp == true)
            {
                currentNode = gameManager.rightWarpNode;
                direction = "left";
                previousDirection ="left";
                transform.position = currentNode.transform.position;
                warp=false;
            }

            else if(currentNodeController.isWarpRight && warp == true)
            {
                currentNode = gameManager.leftWarpNode;
                direction = "right";
                previousDirection ="right";
                transform.position = currentNode.transform.position;
                warp=false;
            }

            else
            {
                if(currentNodeController.isDogStartingNode && direction == "down" && (!isDog||GetComponent<EnemyController>().dogNodeState != EnemyController.DogNodeStateEnum.respawining))
                {
                    direction=previousDirection;
                }

                GameObject newNode = currentNodeController.GetNodeFromDirection(direction);

                if(newNode !=null)
                {
                    currentNode = newNode;
                    previousDirection = direction;
                }

                else
                {
                    direction = previousDirection;
                    newNode = currentNodeController.GetNodeFromDirection(direction);
                    if(newNode !=null)
                    {
                        currentNode=newNode;
                    }
                }
            }
        }
        else
        {
            warp=true;
        }

    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public void SetDirection(string newDirection)
    {
        direction = newDirection;
    }

    //LastLine
}
