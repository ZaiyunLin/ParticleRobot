using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    // Start is called before the first frame update    
    Camera cam ;
    int camsize = 10;
    ClusterControl control;
    void Start()
    {
        cam = Camera.main;
        control = GetComponent<ClusterControl>();
    }

    // Update is called once per frame
    void Update()
    {
        
        
        Camera.main.orthographicSize = control.camsize*0.1f + 10f ;
        Vector3 pos = GetComponent<ClusterControl>().mypos;
        pos.z = -10;
        Vector3 campos = cam.GetComponent<Transform>().position;
        campos = Vector3.Lerp(campos,pos,0.7f*Time.deltaTime);
        cam.GetComponent<Transform>().position = campos;
    }
}
