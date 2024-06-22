
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Pool;


public class BOT : MonoBehaviour,IState
{
    // Start is called before the first frame update
    
    public Node CurrentNode;
    public Node TargetNode;
    int activatedCount;
    int countToActivate;
    public bool attack;
    [SerializeField]  private bool CanAction;
     IState currentState;
    [SerializeField] List<Node> PLNode;
   
    private float timer = 0;
    private float delayTime = 0.2f;

    void Start()
    {
        CurrentNode = this.GetComponent<Node>();
        ChangeState(new IncreaState());

    }
    void Update()
    {
        var();
        
        if (currentState != null) {
            
             currentState.OnExcute(this);
        }
        Debug.Log(currentState.ToString());
        
        foreach(Node node in CurrentNode.nodes) {
            if(node.gameObject.layer !=7 && !PLNode.Contains(node)){
                PLNode.Add(node);
            }else if(node.gameObject.layer == 7 )
            {
                PLNode.Remove(node);
            }
        
        }
    }
    public  void OnInit()
    {
        ChangeState(new IncreaState());
       
    }
    
    
    public void Attack()
    {
       
        NodeMinSoldierAttack(CurrentNode);
        CheckMilitaryNumber();
        /*StartCoroutine(ActivateWithDelay(CurrentNode.Soldier));*/
        timer += Time.deltaTime;

        // Nếu đã đủ thời gian giữa các lần kích hoạt và có thể thực hiện hành động
        if (timer >= delayTime && CanAction)
        {
            // Kích hoạt một đối tượng
            ActivateObject();
            // Đặt lại timer
            timer = 0f;
        }
    }
    public void Def()
    {
        Debug.Log("def");
        NodeMinSoldier(CurrentNode);
        CheckMilitaryNumber();
        StartCoroutine(ActivateWithDelay(CurrentNode.Soldier));
    }
    public void var()
    {
        if (PLNode == null) {
            attack = false;
        }
        else
        {
            attack= true;
        }
    }

    

    // Update is called once per frame
    public void ChangeState(IState newstate)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newstate;
        if(currentState != null)
        {
            currentState.OnEnter(this);
          
        }
    }








  
    Node NodeMinSoldier(Node node)
    {
        // chon ra Node co linhs it nhat
        int minNum = int.MaxValue;
        foreach (Node min in node.nodes) {
            if (node._soldiers < minNum)
            {
                minNum = node._soldiers;
                 TargetNode = min;
            }
        }
        return TargetNode;
    }
    Node NodeMinSoldierAttack(Node node)
    {
        // chon ra Node co linhs it nhat
        int minNum = int.MaxValue;
        foreach (Node min in PLNode)
        {
            if (node._soldiers < minNum)
            {
                minNum = node._soldiers;
                TargetNode = min;
            }
        }
        return TargetNode;
    }



    void CheckMilitaryNumber()
    {
        if (CurrentNode != null)
        {
            if (CurrentNode._soldiers < 5 && CurrentNode._soldiers > 0)
            {
                countToActivate = CurrentNode._soldiers;
                CanAction = true;
            }
            else if (CurrentNode._soldiers <= 0)
            {
                CanAction = false;
                attack = false;
                CurrentNode._soldiers = 0;
                ChangeState(new IncreaState());
            }
            else
            {
                CanAction = true;
                countToActivate = 5;
            }
        }
    }

    IEnumerator ActivateWithDelay(List<GameObject> pooledObjects)
    {

        float delayTime = 1f; // Đặt khoảng delay là 0.5 giây, có thể điều chỉnh theo nhu cầu.
        pooledObjects = CurrentNode.Soldier;

        for (int i = 0; i < pooledObjects.Count && activatedCount < countToActivate; i++)
        {

            if (!pooledObjects[i].activeInHierarchy && CanAction)
            {
                pooledObjects[i].SetActive(true);
                activatedCount++;


                if (activatedCount == countToActivate) break;
                Debug.Log(delayTime) ;
                yield return new WaitForSeconds(delayTime);
            }

        }
    }


    void ActivateObject()
    {
        for(int i =0; i <= CurrentNode._soldiers;i++)
        {
            if (!CurrentNode.Soldier[i].activeInHierarchy  )
            {
                CurrentNode.Soldier[i].SetActive(true);
                CurrentNode._soldiers--;
                return; 
            }
        }
    }
}
