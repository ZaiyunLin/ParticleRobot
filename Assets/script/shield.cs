using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shield : MonoBehaviour
{
    // Start is called before the first frame update
    
    GameObject self;
    public GameObject energyshield_spr;
    
    [SerializeField]
    public int ID;
    int health;
    float FixeScale= 1;
    public GameObject parent;
    Color color;
    bool hit = false;
    float H, S, V;
           public void UpdateID(){
        ID = transform.parent.parent.GetComponent<ClusterControl>().getID();
    }
    
    void Start()
    {
        color = energyshield_spr.GetComponent<SpriteRenderer>().color;
        Color.RGBToHSV(color,out H, out S, out V); 

        health = 100;
        self = transform.parent.parent.gameObject;
        ID = self.GetComponent<ClusterControl>().getID();
        parent = transform.parent.gameObject;
        FixeScale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        ColorControl();
        transform.localScale = new Vector3 (FixeScale/parent.transform.localScale.x,FixeScale/parent.transform.localScale.y,FixeScale/parent.transform.localScale.z);
    }
    public void TakeDamage(int damage){
        hit = true;
        health -= damage;
        if (health<=0){
            gameObject.GetComponent<SphereCollider>().enabled = false;
        }

        }
    public void ColorControl(){
        float speed = 0.25f;
        float min  =0.2f;
        
        if (hit){
            S = Mathf.Lerp(S,min,speed);
            if((S-min)<0.02f){
                hit = false;              
            }
        }else{
            S = Mathf.Lerp(S,1,speed);
        }
        
        Color temp = Color.HSVToRGB(H,S,V);
        float a = S;
        float maxA = ((float) health)/100f;
        if(a>maxA){
            
            a = maxA;
            
        }
        temp.a = a*0.31f;
        energyshield_spr.GetComponent<SpriteRenderer>().color = temp;        
    }

}

