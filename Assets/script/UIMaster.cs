﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIMaster : MonoBehaviour
{
    // Start is called before the first frame update
    public Text deployableParticle;
    public ClusterControl player;

    public int particleNum;
        void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int a = player.resourceNum;
        deployableParticle.text = "Particle Resource " + a;
    }
    public void update(int p){
        p = particleNum;
    }
}
