using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    bool hit = false;
    int damage = 10;
    int counter = 0;
    public int ID;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        counter ++;
        if(counter>2000){
            Destroy(this.gameObject);
        }
        
    }
    public void OnTriggerEnter(Collider other){
        if(!hit){
            if(other.tag =="shield"){
                if(other.gameObject.GetComponent<shield>().ID!=ID){
                    hit = true;
                    other.gameObject.GetComponent<shield>().TakeDamage(damage);
                    Destroy(this.gameObject);  
                }
            }
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
