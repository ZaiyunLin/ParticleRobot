using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class robotMaster : MonoBehaviour
{
    public float pullRadius = 2;
    float pullForce = -0.5f;
    
    public Transform cluster;
    public Transform target;
    float scaleOrg = 1;
    float scaleMax = 1.6f;
    float scaleCur = 1;
    int scaleDir = 1;
    float scaleSpeed = 0.2f;
    public float delay = 0;
    public float internalDelay = 40;
    int counter =0 ;
    public int internalCounter ;
    public bool on = true;
    // Start is called before the first frame update
    public bool startContraction;
    public bool externalStart;
    public bool resetContract;
    public GameObject Cluster;

    public GameObject self;
    public int health = 100;
    public int ID = 0;
    GameObject resourcePoint;
    Dictionary<int, Transform> connectedBdy;
    void Start()
    {
        resetContract = false;
        resourcePoint = Resources.Load("resourcePoint") as GameObject;
        cluster = GetComponent<Transform>();
        externalStart = false;
        startContraction = true;
        self = transform.parent.gameObject;
        ID = transform.parent.GetComponent<ClusterControl>().getID();
        connectedBdy = new Dictionary<int, Transform>();
        //internalCounter = internalDelay;
    }
    public void UpdateID(){
         ID = transform.parent.GetComponent<ClusterControl>().getID();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (Collider collider in Physics.OverlapSphere(transform.position, pullRadius)) {
            if(!collider.isTrigger){
                //check ID of the particle, is it my particle?
                if(collider.TryGetComponent(out robotMaster rb)){
                    int otherID = rb.ID;
                    if(otherID!=ID){
                        continue;
                    }
                }else{
                    continue;
                }
                
                Vector3 forceDirection = transform.position - collider.transform.position;
                GetComponent<Rigidbody>().AddForce(forceDirection.normalized * pullForce,ForceMode.VelocityChange);
            }
        }

        if(externalStart&& on){     
                if(internalCounter < internalDelay){
                    startContraction = true;
                    internalCounter ++;
                }
                if(startContraction ){
                    if(internalCounter >= internalDelay){
                        Contraction();
                    }
                }
                if (!startContraction){
                    internalCounter = 0;
                }
                
        }
        if(resetContract){
            on = false;
            resetContraction();
        }
        
    }
    void Contraction(){
        if(scaleDir ==1){
            scaleCur = Mathf.Lerp(scaleCur,scaleMax,scaleSpeed);
            if((scaleMax - scaleCur)<0.05f){
                scaleDir = -1;
            }
        }else{
            scaleCur = Mathf.Lerp(scaleCur,scaleOrg,scaleSpeed);
            if((scaleCur-scaleOrg)<0.05f){
                scaleDir = 1;
                startContraction = false;
            }
        }
        Vector3 scaleChange = new Vector3(scaleCur, scaleCur, scaleCur);
        gameObject.transform.localScale = scaleChange; 
    }
    public void resetContraction(){
            scaleCur = Mathf.Lerp(scaleCur,scaleOrg,scaleSpeed);
            if((scaleCur-scaleOrg)<0.05f){
                scaleDir = 1;
                resetContract = false;
                on = true;
            }
        
        Vector3 scaleChange = new Vector3(scaleCur, scaleCur, scaleCur);
        gameObject.transform.localScale = scaleChange; 
    }
    public void StartContraction(){
        externalStart = true;
        startContraction = true;
    }
    public void takeDamage(int damage){
        health -= damage;
        if(health<=0){
            self.GetComponent<ClusterControl>().removeParticles(this.gameObject);
            Instantiate(resourcePoint, transform.position,transform.rotation);
        }
    }
    public void OnCollisionEnter(Collision other){

            if(other.collider.TryGetComponent(out robotMaster rb)){
                int otherID = other.gameObject.GetInstanceID();
                int clusterID = rb.ID;
                    if(clusterID!=ID){
                        return;
                    }

                if(!connectedBdy.ContainsKey(otherID)){
                    connectedBdy.Add(otherID, other.transform);
                    SpringJoint joint= gameObject.AddComponent<SpringJoint>();
                    joint.autoConfigureConnectedAnchor = false;
                    joint.anchor = new Vector3(0,0,0);
                    joint.spring = 1000;
                    joint.minDistance = 1;
                    joint.maxDistance = 1;
                    joint.massScale = 1;
                    joint.connectedMassScale = 5;
                    joint.connectedAnchor = new Vector3(0,0,0);
                    joint.connectedBody = other.collider.attachedRigidbody;
                    joint.enableCollision = true;
                }
            }else{
                return;
            }
           
          
    }
}
