using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour {
    public ParticleSystem ps;
    private float hSliderValueR=255;
    private float hSliderValueG=255;
    private float hSliderValueB=255;

    IEnumerator Fade()
    {
        
        for (float f = 1f; f >= 0; f -= 0.01f)
        {
            var main = ps.main;
            main.startColor = new Color(hSliderValueR, 255, 255, f);
            Debug.Log(main.startColor.color);
            
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void StartFadeOut()
    {
        
        StartCoroutine("Fade");
    }
}
