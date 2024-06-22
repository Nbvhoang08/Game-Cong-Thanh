using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.Pool;

public class SoilderManager : MonoBehaviour
{
    // Start is called before the first frame update

    List<GameObject> pooledObjects = new List<GameObject>();
    

    public Node selectedNode; // Node hiện tại được chọn
    public Node targetNode; // Node mục tiêu
    private GameObject selectedSoldier; // Quân lính được chọn
    public static SoilderManager instance;
    public bool CanAction;
    public int countToActivate = 5;
    public int activatedCount = 0;
    private void Awake()
    {
        instance = this; 
    }

    void Update()
    {
        if(activatedCount >= countToActivate)
        {
            selectedNode = null;
            targetNode = null;
            activatedCount = 0;
    
        }
        


        // Kiểm tra xem có click chuột không
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                Node clickedNode = hit.collider.GetComponent<Node>();
                CheckMilitaryNumber();
                // Kiểm tra xem node có liên kết với node trước đó không
                if (selectedNode != null && selectedNode.nodes.Contains(clickedNode))
                {
                    if (selectedNode == targetNode)
                    {
                        selectedNode = null;
                        targetNode = null;
                    }
                    else
                    {
                        targetNode = clickedNode;
                        StartCoroutine(ActivateObjectsWithDelay(pooledObjects));
                        selectedNode._soldiers -= countToActivate; 
                    }
                }
                else
                {
                    selectedNode = clickedNode;
                }
                

            }
           
        }
    }
    void CheckMilitaryNumber()
    {
        if (selectedNode != null)
        {
            if (selectedNode._soldiers < 5 && selectedNode._soldiers > 0)
            {
                countToActivate = selectedNode._soldiers;
                CanAction = true;
            }
            else if (selectedNode._soldiers <= 0)
            {
                CanAction = false;
                selectedNode._soldiers =0;
            }
            else
            {
                CanAction = true;
                countToActivate = 5;
            }
        }
    }
    IEnumerator ActivateObjectsWithDelay(List<GameObject> pooledObjects)
    {
   
        float delayTime = 0.2f; // Đặt khoảng delay là 0.2 giây, có thể điều chỉnh theo nhu cầu.
        pooledObjects = selectedNode.Soldier;
       
            for (int i = 0; i < pooledObjects.Count && activatedCount < countToActivate; i++)
            {
                
                if (!pooledObjects[i].activeInHierarchy && CanAction)
                {
                    pooledObjects[i].SetActive(true);
                    activatedCount++;
           
                    if (activatedCount == countToActivate) break;
                    yield return new WaitForSeconds(delayTime);
                }
                
            }
        }
      
    }

   



