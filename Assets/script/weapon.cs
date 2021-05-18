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
    int internalCD = 50;
    int counter = 0;
    public float bulletSpeed = 1;
    public GameObject bullet;
    public GameObject weapon_spr;
    float FixeScale= 1;
    public GameObject parent;
    [SerializeField]
    int ID;
    void Start()
    {   
        
        ID = transform.parent.parent.GetComponent<ClusterControl>().getID();
        range = 11;
        self = transform.parent.parent.gameObject;
        trigger = gameObject.GetComponent<SphereCollider>();
        trigger.radius = range;
        bullet = Resources.Load("bullet") as GameObject;
        parent = transform.parent.gameObject;
        FixeScale = transform.localScale.x;


    }
    public void UpdateID(){
        ID = transform.parent.parent.GetComponent<ClusterControl>().getID();
    }
    // Update is called once per frame
    void Update()
    {
         transform.localScale = new Vector3 (FixeScale/parent.transform.localScale.x,FixeScale/parent.transform.localScale.y,FixeScale/parent.transform.localScale.z);
        if(target!=null){
            Attack(target);
            TowardTarget(target);
        }else{
            enenmyNum = 1;
        }
    }
     private void OnTriggerEnter(Collider other)
    {   
      
        if(other.tag =="enemy" || other.tag =="Player"){
            if(other.isTrigger==true){
                return;
            }
           //Debug.Log(other.gameObject.name);
           if(other!=null &&other.transform.gameObject.TryGetComponent(out robotMaster rm)){
              if(rm.ID!= ID ){
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
    void TowardTarget(GameObject target){


        Vector3 vectorToTarget = target.transform.position - weapon_spr.transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        weapon_spr.transform.rotation = Quaternion.Slerp(weapon_spr.transform.rotation, q, Time.deltaTime * 1);
        //transform.rotation = Quaternion.AngleAxis(1, Vector3.forward);
        //weapon_spr.transform.LookAt(target.transform);

    }
}
