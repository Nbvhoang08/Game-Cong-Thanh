using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;
public class Node : MonoBehaviour
{
    public int id;
    public List<Node> nodes;
    public GameObject connectionPrefab;
    //----------------------------------------------------------------------------------------//
    public bool beInvaded;
    public int level;

    public int _maxSoldiers;
    public int _soldiers = 0;

    public Text soiderNUM;
    public List<GameObject> Soldier = new List<GameObject>();
    public string childObjectName = "Art";
    public int amountToPool = 50;
    public GameObject objectToPool;
    
    public Node(int nodeId)
    {
        id = nodeId;
       
        
    }
    public void Start()
    {
       
        DisplayNodeMap();
        InvokeRepeating("IncreaseSoldiers", 1f, 1f); // Gọi hàm IncreaseSoldiers mỗi giây
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(objectToPool,transform.position,Quaternion.identity);
            obj.GetComponent<Soilder>().tower = this.gameObject;
            Soldier.Add(obj);
            
            obj.SetActive(false);
            
        }
       
        
       
    }
    private void Update()
    {
        soiderNUM.text = _soldiers.ToString();
        StateManger();
        ChangeStateSoilder();
    }
    private void IncreaseSoldiers()
    {
        if (_soldiers < _maxSoldiers)
            _soldiers++;

    }
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < Soldier.Count; i++)
        {
            if (!Soldier[i].activeInHierarchy)
            {
                return Soldier[i];
            }
        }
        return null;
    }
    void DisplayNodeMap()
    {

        for (int i = 0; i< nodes.Count;i++ )
        {
            // Vẽ các đường nối từ node hiện tại đến các node kết nối
          
            if (this.id < nodes[i].id)
            {
                DrawConnection(new Vector3(transform.position.x,transform.position.y,6), i);
                    
            }
            
        }
    }
    void DrawConnection(Vector3 start, int endNodeIndex)
    {
        // Vị trí của node kết nối
      
        Vector3 endPosition = new Vector3(nodes[endNodeIndex].transform.position.x, nodes[endNodeIndex].transform.position.y,6);

        // Tạo đối tượng đường nối từ Prefab
        GameObject connectionObject = Instantiate(connectionPrefab);

        // Lấy hoặc thêm Component LineRenderer cho đối tượng đường nối
        LineRenderer lineRenderer = connectionObject.GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            lineRenderer = connectionObject.AddComponent<LineRenderer>();
        }

        // Thiết lập các thuộc tính của LineRenderer
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, endPosition);
    }

    void StateManger()
    {
        Transform childTransform = transform.Find("Art");
        
        if (this.gameObject.layer == 7)
        {
            beInvaded = true;
            if (childTransform != null)
            {
                childTransform.GetComponent<SpriteRenderer>().color = Color.red;
                
                
            }
        }else if(this.gameObject.layer == 6)
        {
            childTransform.GetComponent<SpriteRenderer>().color = Color.white;
            beInvaded = false;
        }
    }
    void Invade()
    {
        
    }

    void ChangeStateSoilder()
    {
        for (int i = 0; i < Soldier.Count; i++)
        {
            Soldier[i].layer = this.gameObject.layer;
            
        }    
    }
}


