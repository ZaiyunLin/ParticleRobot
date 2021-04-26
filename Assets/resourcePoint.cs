using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resourcePoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other){
        if((other.tag=="enemy"||other.tag=="Player")&&other.isTrigger==false){
            other.transform.parent.gameObject.GetComponent<ClusterControl>().resourceNum +=1;
            Destroy(this.gameObject);
        }
    }
}
