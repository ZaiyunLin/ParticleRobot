using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    bool hit = false;
    public int damage = 1;
    public int ID;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other){
        if(!hit){
            if(other.tag =="enemy"||other.tag=="Player"){
                if(other.isTrigger==true){
                    return;
                }
                if(other.transform.gameObject.GetComponent<robotMaster>().ID!= ID){
                    hit = true;
                    other.GetComponent<robotMaster>().takeDamage(damage);
                    Destroy(this.gameObject);  
                }
            }
        }
    }
}
