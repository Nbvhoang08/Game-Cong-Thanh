
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] allobjects ;
    public List<GameObject> enemies;
     void Start()
    {
       allobjects = GameObject.FindGameObjectsWithTag("nodes");
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject obj in allobjects)
        {
            if (obj.layer == 8 && !allobjects.Contains(obj)) {
                    enemies.Add(obj);   
                
            }
            else if (obj.layer != 8 && allobjects.Contains(obj)) { 
                    enemies.Remove(obj);
            }
           

        }    
    }


}
