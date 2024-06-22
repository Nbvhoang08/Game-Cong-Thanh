using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Objecpool : MonoBehaviour
{
    // Start is called before the first frame update
    public static Objecpool instance;
  
    public int amountToPool =50 ;
    public List<GameObject> pooledObjects;
    void Awake()
    {
        instance = this;
    }

    void Start()
    {

       
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
}
