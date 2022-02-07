using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NodeController : MonoBehaviour
{
    public GameManager gameManager;

    public bool canMoveLeft=false;
    public bool canMoveRight=false;
    public bool canMoveUp=false;
    public bool canMoveDown=false;

    public GameObject nodeLeft;
    public GameObject nodeRight;
    public GameObject nodeUp;
    public GameObject nodeDown;

    public bool isWarpRight = false;
    public bool isWarpLeft = false;

    public bool milkNode = false;
    public bool hasMilk = false;

    public bool isDogStartingNode = false;

    public SpriteRenderer milkSprite;

    public bool isSideNode=false;

    // Start is called before the first frame update
    void Awake()
    {
        gameManager=GameObject.Find("GameManager").GetComponent<GameManager>();

        if(transform.childCount > 0)
        {
            gameManager.GotMilk();
            hasMilk = true;
            milkNode = true;
            milkSprite = GetComponentInChildren<SpriteRenderer>();
        }

        RaycastHit2D[] hitsDown;
        hitsDown=Physics2D.RaycastAll(transform.position,-Vector2.up);

        for(int i=0; i<hitsDown.Length; i++)
        {
            float distance = Mathf.Abs(hitsDown[i].point.y-transform.position.y);
            if(distance<0.3f && hitsDown[i].collider.tag =="Node")
            {
                canMoveDown=true;
                nodeDown=hitsDown[i].collider.gameObject;
            }
            
        }

        RaycastHit2D[] hitsUp;
        hitsUp=Physics2D.RaycastAll(transform.position,Vector2.up);

        for(int i=0; i<hitsUp.Length; i++)
        {
            float distance = Mathf.Abs(hitsUp[i].point.y-transform.position.y);
            if(distance<0.3f && hitsUp[i].collider.tag =="Node")
            {
                canMoveUp=true;
                nodeUp=hitsUp[i].collider.gameObject;
            }
            
        }

        RaycastHit2D[] hitsRight;
        hitsRight=Physics2D.RaycastAll(transform.position,Vector2.right);

        for(int i=0; i<hitsRight.Length; i++)
        {
            float distance = Mathf.Abs(hitsRight[i].point.x-transform.position.x);
            if(distance<0.3f && hitsRight[i].collider.tag =="Node")
            {
                canMoveRight=true;
                nodeRight=hitsRight[i].collider.gameObject;
            }
            
        }

        RaycastHit2D[] hitsLeft;
        hitsLeft=Physics2D.RaycastAll(transform.position,-Vector2.right);

        for(int i=0; i<hitsLeft.Length; i++)
        {
            float distance = Mathf.Abs(hitsLeft[i].point.x-transform.position.x);
            if(distance<0.3f && hitsLeft[i].collider.tag =="Node")
            {
                canMoveLeft=true;
                nodeLeft=hitsLeft[i].collider.gameObject;
            }
            
        }

        if (isDogStartingNode == true)
        {
            canMoveDown = true;
            nodeDown=gameManager.dogNodeCenter;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetNodeFromDirection (string direction)
    {
        if(direction=="left" && canMoveLeft==true)
        {
            return nodeLeft;
        }

        else if(direction == "right" && canMoveRight==true)
        {
            return nodeRight;
        }

        else if(direction =="up" && canMoveUp==true)
        {
            return nodeUp;
        }

        else if(direction == "down" && canMoveDown==true)
        {
            return nodeDown;
        }

        else
            return null;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && hasMilk)
        {
            hasMilk = false;
            milkSprite.enabled = false;
            gameManager.CollectedMilk(this);
        }
    }

    //Last Line
}
