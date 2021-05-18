using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resourcePoint : MonoBehaviour
{
    // Start is called before the first frame update
    Color color ;
    void Start()
    {
        color = Random.ColorHSV(0,1,1f,1,1f,0.95f);
        GetComponent<SpriteRenderer>().color = color;
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
