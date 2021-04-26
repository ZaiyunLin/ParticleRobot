using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapCreator : MonoBehaviour
{
    public GameObject resouce;
    int mapsize = 50;
    public int resNum = 500;
    int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < resNum; i++){
            count++;
            //Debug.Log("ss");
            generate();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void generate(){
        float posx = Random.Range(-mapsize,mapsize);
        float posy = Random.Range(-mapsize,mapsize);
        Vector3 randomPos= new Vector3 (posx,posy,0); 
        GameObject point= Instantiate(resouce, randomPos,transform.rotation);
        point.name = "resourcepoint" +count;
    }
}
