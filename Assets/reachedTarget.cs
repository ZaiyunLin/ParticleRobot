using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reachedTarget : MonoBehaviour
{
    // Start is called before the first frame update
    public int ID = 0;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other){

        if(other.gameObject.TryGetComponent<robotMaster>(out robotMaster robmas)){
            if(robmas.ID == ID){
                robmas.UpdateTargetPos();
            }
        }

    }
}
