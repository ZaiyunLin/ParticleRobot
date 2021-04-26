using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    // Start is called before the first frame update    
    Camera cam ;
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 pos = GetComponent<ClusterControl>().mypos;
        pos.z = -10;
        cam.GetComponent<Transform>().position = pos;
    }
}
