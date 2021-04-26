using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> clusters;
    public GameObject player;
    public GameObject AI;
    public int AInum = 1;
    void Start()
    {
        clusters.Add(player);
        player.GetComponent<ClusterControl>().UpdateID(-1);
        
        SpawnAI();
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnAI(){
        for(int i = 0; i<AInum;i++){

            GameObject ai = Instantiate(AI,new Vector3(Random.Range(-2,2),-5+i*10,0),transform.rotation );
            clusters.Add(ai);
            ai.GetComponent<ClusterControl>().UpdateID(i);
        }
    }

}
