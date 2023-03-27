using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleColor : MonoBehaviour {

	// Use this for initialization
    public BottleSmash bottleSmash;
    private ParticleSystemRenderer ps;
	void Start ()
	{
	    ps = GetComponent<ParticleSystemRenderer>();

	}
	
	// Update is called once per frame
	void Update () {

        //apply the material color
        ps.material.SetColor("_TintColor", bottleSmash.color);

    }
}
