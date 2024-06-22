using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soilder : MonoBehaviour
{
    // Start is called before the first frame update
    public int num;
    public Node selectedNode; // Node hiện tại được chọn
    public Node targetNode;
    public Vector3 StarPos;
    public int speed;
    public bool canMove =false ;
    public GameObject tower;
    void Start()
    {
       
    }
    private void OnEnable()
    {
        if (this.gameObject.layer == 7)
        {
            // chon muc tieu theo BOT neu la enemies
            selectedNode = tower.gameObject.GetComponent<BOT>().CurrentNode;
            targetNode = tower.gameObject.GetComponent<BOT>().TargetNode;
        }
        else if (this.gameObject.layer == 6)
        {
            // chon muc tieu theo manager neu la player
            selectedNode = SoilderManager.instance.selectedNode;
            targetNode = SoilderManager.instance.targetNode;
        }
        if (selectedNode != null && targetNode != null)
        {
            canMove = true;
            StarPos =selectedNode.transform.position;
        }
        else
        {
            canMove = false;
            this.gameObject.SetActive(false);
            
        }  

    }
    // Update is called once per frame
    private void Update()
    {
        StateManger();
    }
    void FixedUpdate()
    {
        Move();            
    }

    void Move()
    {
        if (canMove) {
           
            transform.position = Vector3.MoveTowards(transform.position, targetNode.transform.position, speed * Time.fixedDeltaTime);
            float dis = Vector3.Distance(transform.position, targetNode.transform.position);
            if (dis < 0.01) {
                enterCastle();
                canMove=false;
                transform.position = StarPos;
                this.gameObject.SetActive(false);
            }

        }
        
    }
    void StateManger()
    {
        if (this.gameObject.layer == 7)
        {
            this.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else if (this.gameObject.layer == 6)
        {
            this.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    void enterCastle()
    {
        if(this.gameObject.layer == targetNode.gameObject.layer) {
            targetNode._soldiers += 1;
           
        }
        else
        {
            targetNode._soldiers -= 1;
            if(targetNode._soldiers == 0)
            {
                targetNode.gameObject.layer = this.gameObject.layer;
            }
       
        }
    }

    
}
