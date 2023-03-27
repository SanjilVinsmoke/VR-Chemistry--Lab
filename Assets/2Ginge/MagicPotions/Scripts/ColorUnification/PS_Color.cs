using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PS_Color : MonoBehaviour {

    // Use this for initialization
    public BottleSmash bottleSmash;
    private ParticleSystem ps;
    void Start ()
    {
        ps = GetComponent<ParticleSystem>();

    }
	
	// Update is called once per frame
	void Update ()
	{
        //get the main block.
	    ParticleSystem.MainModule mm = ps.main;
        //apply the color;
        mm.startColor = new ParticleSystem.MinMaxGradient(bottleSmash.color);
	    


	}
}
