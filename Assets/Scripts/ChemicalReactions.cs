using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemicalReactions : MonoBehaviour
{

    public AudioSource audio;
    public GameObject smokeParticles;
    public GameObject smokeParticlesForWaterAndPotassium;
    public GameObject afterExplosionSmoke;
    public BottleSmash smashScript;
    bool hasExplosionBeenInstantiated = false;
    bool hasBottleExploded = false;
    public ParticleSystem ps;
    public GameObject cameraRig;
    // Use this for initialization

    public Shelf sh;
    
    void SetSmokeParticlesInactive()
    {
        smokeParticles.SetActive(false);
    }
    private void OnEnable()
    {
        LiquidAbsorption.reactWaterPotassium += ExplodeWaterAndPotassium;
        LiquidAbsorption.reactHydroChloricAndSodiumHydroOxide += HydroChloricAndSodiumHydroOxide;
    }
    private void OnDisable()
    {
        LiquidAbsorption.reactWaterPotassium -= ExplodeWaterAndPotassium;
        LiquidAbsorption.reactHydroChloricAndSodiumHydroOxide -= HydroChloricAndSodiumHydroOxide;
    }

   
    //is called when hydrochloric acid and NAOH are mixed it Invokes a cancelation of smoke particles from the reaction after 5.5 seconds
    void HydroChloricAndSodiumHydroOxide()
    {
        smokeParticles.SetActive(true);
        Debug.Log("i have reacted");
        Invoke("SetSmokeParticlesInactive", 5.5f);
    }
    //Is called when water and potassium is mixed. If statements are used to call the explosion ONLY ONCE. This way reaction actually starts whenever particles FIRST collide,
    //rather than everytime they collide.
    public void ExplodeWaterAndPotassium()
    {
        if (!hasExplosionBeenInstantiated)
        {
            smokeParticlesForWaterAndPotassium.SetActive(true);
            Invoke("PlayAudio", 2f);
            hasExplosionBeenInstantiated = true;
        }
        //This if statement makes sure that bottle is smashed only once.
        if (!hasBottleExploded)
        {
            ExplodeInvoke();
            hasBottleExploded = true;
        }

    }
    
    //helper method to instantiate bottle smash
    void Smash()
    {
        
        smashScript.Smash();
        sh.GetComponent<Shelf>().ShelfFalling();
       
        smokeParticlesForWaterAndPotassium.SetActive(false);

    }
    //helper method to Invoke a smash of the bottle after 4 seconds when substances have been mixed
    void ExplodeInvoke()
    {

        //cameraRig.transform.position = Vector3.forward;
        Invoke("Smash", 4f);
        Invoke("AfterExplosionSmoke", 4f);

    }
    //This method starts smoke particles and lets it ramp up but makes it invisible for 1.
    void AfterExplosionSmoke()
    {
       
        afterExplosionSmoke.SetActive(true);
        afterExplosionSmoke.GetComponent<FadeOut>().StartFadeOut();
        Invoke("SlowDownSmoke", 0.15f);
        Invoke("RemoveSmoke",5f);

        
    }

   
    void RemoveSmoke()
    {
        Destroy(afterExplosionSmoke);
    }
    void SlowDownSmoke()
    {
        var main = ps.main;
        main.simulationSpeed = 0.06f;
    }
    //This method is invoked from ExplodeWaterAndPotassium.
    void PlayAudio()
    {
        audio.Play();
    }

    
}