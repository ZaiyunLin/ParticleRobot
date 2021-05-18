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
    int mapsize;
    void Start()
    {
        clusters.Add(player);
        player.GetComponent<ClusterControl>().UpdateID(-1);
        mapsize = mapCreator.mapsize;
        
        SpawnAI();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnAI(){
        for(int i = 0; i<AInum;i++){
            float posx = Random.Range(-mapsize,mapsize);
            float posy = Random.Range(-mapsize,mapsize);
            GameObject ai = Instantiate(AI,new Vector3(posx,posy,0),transform.rotation );
            clusters.Add(ai);
            ai.GetComponent<ClusterControl>().UpdateID(i);
        }
    }

}
