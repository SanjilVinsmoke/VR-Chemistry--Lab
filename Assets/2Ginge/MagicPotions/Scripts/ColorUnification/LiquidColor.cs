using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidColor : MonoBehaviour {

	// Use this for initialization
    public BottleSmash bottleSmash;
    private LiquidVolumeAnimator lva;

    public bool UpdateSurfaceColor = true;
    public bool UpdateColor = true;
    public bool UpdateSurfaceEmission = true;
    public bool UpdateEmission = true;
    void Start ()
	{
	    //mat = GetComponent<MeshRenderer>();
	    lva = GetComponent<LiquidVolumeAnimator>();
	}
	
	// Update is called once per frame
	void Update ()
	{
        if(UpdateColor)
	        lva.mats[0].SetColor("_Color", bottleSmash.color);
        if(UpdateEmission)
	        lva.mats[0].SetColor("_EmissionColor", bottleSmash.color);
        if(UpdateSurfaceColor)
	        lva.mats[0].SetColor("_SColor", bottleSmash.color);
        if(UpdateSurfaceEmission)
	        lva.mats[0].SetColor("_SEmissionColor", bottleSmash.color);

    }
}
