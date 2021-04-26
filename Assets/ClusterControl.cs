using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClusterControl : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 mypos;
    public Bounds bounds;
    public GameObject weapon;
    public List<GameObject> particles;
    GameObject particleDefault;
    float delay = 20;
    
    int counter = 0;
    public bool start = false;
    public bool start2 = false;
    public bool recalculate = false;
    public bool dead = false;

    bool reached = false;
    public Transform targetpos;
    public Vector3 actualTargetpos;
    public static float timescale = 2.0f;
    public int children = 0;
    public int state = 0;
    int recalculateInterval = 300;
    int intervalCounter =0;
    public int resourceNum =0;
    Vector3 calculatedPos;
    float finalDis;
    Vector3 dir;
    int ID = -1;
    bool paused = false;
    public int mytype = 0;
    void Start()
    {   
        mypos = transform.position;
        reached = false;
        bounds = gameObject.GetComponent<BoxCollider>().bounds;
        bounds.center = transform.position;
        if(gameObject.tag =="enemy"){
            mytype = 1;
        }
        if(mytype == 1){
            targetpos = Instantiate(targetpos,new Vector3(5,6,0),transform.rotation);
        }
        
        particles = new List<GameObject>();
        children = transform.childCount;
        for (int i = 0; i < children; ++i){
             particles.Add(transform.GetChild(i).gameObject);
        }
        calculateDistance();
        Time.timeScale = timescale;
        particleDefault = Resources.Load("particleDefault") as GameObject;
       
    }
    public void UpdateID(int id){

        ID = id;
        //Debug.Log(ID);
    }
    public int getID(){
        return ID;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
       
        Time.timeScale = timescale;
        intervalCounter++;
        if(intervalCounter >= recalculateInterval){
            recalculate = true;
        }
        if(recalculate){
            CalculateTargePos();
            calculateDistance();
            recalculate = false;
        }
        
       
        if(start2){
            paused = false;
            foreach (GameObject particle in particles){
                particle.GetComponent<robotMaster>().StartContraction();  
                start2 = false;
            }
            
        }
        //or paused   
        if(mytype==1){
                if(reached){
               // Debug.Log("paus");
               targetpos.position = GetComponent<ObjectFinder>().FindClosest(particles[0].transform.position);
                recalculate = true;
            }
        }
        if(paused){
            pause();
        }
         boundsDetect();
        
        mypos = bounds.center;
        if(particles.Count == 0){
            dead = true;
        }
        if(dead){
            mypos = transform.position;
        }

        switch(state){
            //case 0:;
        }

    }
    void Update(){
        Time.timeScale = timescale;
        if(mytype!=1){
            //if I am player
            if(Input.GetMouseButtonDown(0)){
                targetpos.transform.position = getMousePos();
                //Debug.Log("press");
                recalculate=true;
                start2 = true;

            }

            if(resourceNum>0){
                if(Input.GetMouseButtonDown(1)){
                    resourceNum -=3;
                    GameObject clone = Instantiate(particleDefault,getMousePos(),Quaternion.identity,transform);
                    clone.GetComponent<robotMaster>().UpdateID();
                    GameObject wp= Instantiate(weapon,clone.transform.position,Quaternion.identity,clone.transform);
                    wp.GetComponent<weapon>().UpdateID();
                    particles.Add(clone);

                    }
            }
            if(Input.GetKeyDown(KeyCode.Space)){
                paused =true;
                
            }
        }else{
            //if I am an AI
            start2 = true;
        }

    }
    public void CalculateTargePos(){
                float sqrDis = Mathf.Sqrt(bounds.SqrDistance(targetpos.position));
                //Debug.Log("sqrdis "+sqrDis);
                dir = (targetpos.position - bounds.center).normalized;
                float fulldis = Vector3.Distance(bounds.center,targetpos.position);
                //Debug.Log("fullDis "+fulldis);
                //Debug.Log(dir);
                finalDis= Mathf.Abs(fulldis-sqrDis)+3;
                //Debug.Log("finalDis "+finalDis);
                calculatedPos = bounds.center +dir*finalDis;
                actualTargetpos = calculatedPos;
    }
   
    public void calculateDistance(){
            //Debug.Log("calculateDistance");
            intervalCounter = 0;
            children = transform.childCount;
                  
            for (int i = 0; i < children; ++i){
                particles[i].GetComponent<robotMaster>().resetContract=true;
                float dist = Vector3.Distance(particles[i].transform.position, actualTargetpos);   
                float tempDelay = dist*dist*0.5f;
                //Debug.Log(tempDelay);
                particles[i].GetComponent<robotMaster>().internalDelay = tempDelay;
                particles[i].GetComponent<robotMaster>().internalCounter = 0;
            
             };
    }
    public float sigmoid(float x){
        float y = 0;
        y = Mathf.Exp(x)/(Mathf.Exp(x)+1);
        return y;
    }
    public Vector3 getMousePos(){
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z =0;
        
        return mousePos;
    }
    public void removeParticles(GameObject particle){
        particles.Remove(particle);
        Destroy(particle);
    }
    public void boundsDetect(){
         
        if(particles.Count>0 ){
            bounds.center = particles[0].transform.position;
            
        }
         bounds.Expand(new Vector3 (-20,-20,0));
         children = transform.childCount;           
            for (int i = 0; i < children; ++i){
                if(!bounds.Contains( particles[i].transform.position)){
                   // Debug.Log("Encapsulate");
                    bounds.Encapsulate(particles[i].transform.position);
                    
                }
                
            }
        if(bounds.Contains(targetpos.position)){
            reached = true; 
        }else{
            reached = false;
        }
    }
    void OnDrawGizmosSelected(){
         Gizmos.color = Color.magenta;
       
        // Gizmos.matrix = transform.localToWorldMatrix;
        
         Gizmos.DrawWireCube( bounds.center, bounds.size );
     
        
    }
    public void pause(){
        foreach (GameObject particle in particles){
                particle.GetComponent<robotMaster>().resetContract=true;
                particle.GetComponent<robotMaster>().on=false;  
                
            }
    }
    public void FindTarget(){
        
    }
}
