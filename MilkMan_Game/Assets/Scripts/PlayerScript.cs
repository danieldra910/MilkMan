using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerScript : MonoBehaviour
{
    
    MovementScript movementController;

    public GameObject startNode;

    Vector2 startPos;
    // Start is called before the first frame update
    void Awake()
    {
       startPos=new Vector2(0.332f,-0.47f);
        movementController = GetComponent<MovementScript>();

        startNode = movementController.currentNode;
    }

    public void SetUp()
    {
        movementController.currentNode=startNode;
        movementController.previousDirection="left";
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
           
            movementController.SetDirection("left");
        }
        if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
          
            movementController.SetDirection("right");
        }
        if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            movementController.SetDirection("up");
           
        }
        if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            movementController.SetDirection("down");
        }


    }
}
