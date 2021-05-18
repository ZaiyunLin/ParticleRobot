using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;
public class ObjectFinder : MonoBehaviour
{
    // Start is called before the first frame update
    public Dictionary<string, Transform> resource = new Dictionary<string, Transform>();
    Transform closestRes;
    
    void Start()
    {
      
        closestRes = transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other){
        //Debug.Log(other.gameObject.name);
        if(other.gameObject.tag =="resource"){
             if(resource.ContainsKey(other.gameObject.name)==false){
                
                resource.Add(other.gameObject.name,other.gameObject.transform);
             }
                 
        }
    }
    public void OnTriggerExit(Collider other){
        if(other.gameObject.tag =="resource"){
            if(resource.ContainsKey(other.gameObject.name)){
                resource.Remove(other.gameObject.name);
            }
        }
    }
    public Vector3 FindClosest(Vector3 Origin){
        float minLength = 99999;
        if(resource.Count==0){
            
            return GetComponent<ClusterControl>().mypos;
        }
        List<string> nullString = new List<string>();
        foreach(string ID in resource.Keys){
            if(resource.TryGetValue(ID,out Transform temp)){
                if(temp!=null){
                    if(( resource[ID].position - Origin).sqrMagnitude<minLength){
                    minLength =(resource[ID].position - Origin).sqrMagnitude;
                    closestRes = resource[ID];
                    }
                }else{
                    nullString.Add(ID);
                }
            }
            
        }
        foreach(string items in nullString){
            resource.Remove(items);
        }
       
        return closestRes.position;
    }
}
