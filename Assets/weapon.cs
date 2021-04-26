using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject self;
    float range;
    SphereCollider trigger;
    [SerializeField]
    GameObject target;
    public bool attack;

    [SerializeField]
    int enenmyNum = 1;
    int internalCD = 100;
    int counter = 0;
    public float bulletSpeed = 1;
    public GameObject bullet;

    [SerializeField]
    int ID;
    void Start()
    {   
        
         ID = transform.parent.parent.GetComponent<ClusterControl>().getID();
        range = 8;
        self = gameObject.transform.parent.transform.parent.gameObject;
        trigger = gameObject.GetComponent<SphereCollider>();
        trigger.radius = range;
        bullet = Resources.Load("bullet") as GameObject;



    }
    public void UpdateID(){
        ID = transform.parent.parent.GetComponent<ClusterControl>().getID();
    }
    // Update is called once per frame
    void Update()
    {
        
        if(target!=null){
            Attack(target);
        }else{
            enenmyNum = 1;
        }
    }
     private void OnTriggerStay(Collider other)
    {   
      
        if(other.tag =="enemy" || other.tag =="Player"){
            if(other.isTrigger==true){
                return;
            }
           //Debug.Log(other.gameObject.name);
            if(other.transform.gameObject.GetComponent<robotMaster>().ID!= ID){
                if(enenmyNum == 0){
                    return;
                }
                if(enenmyNum == 1){
                    enenmyNum -= 1;
                }
                target = other.gameObject;
            }
        }
        

    }
    private void OnTriggerExit(Collider other){
        if(other.gameObject == target){
            enenmyNum +=1;
            target = null;
        }
    }

    void Attack(GameObject target){
        //Debug.Log("attack");
        if(counter<internalCD){
           // Debug.Log("counter");
            counter ++;
           // Debug.Log(counter);
        }else{
           // Debug.Log("shoot");
            GameObject bulletClone =  Instantiate(bullet, transform.position, transform.rotation);  
            Vector3 forceDirection = transform.position - target.transform.position;
            bulletClone.GetComponent<Rigidbody>().AddForce(-forceDirection.normalized*bulletSpeed,ForceMode.VelocityChange);
            bulletClone.GetComponent<bullet>().ID = ID;
            counter = 0;
        }

    }
}
