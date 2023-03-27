using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleSmash : MonoBehaviour {

	// Use this for initialization
    //all of the required items in order to gie the impression of hte glass breaking.
    [ColorUsageAttribute(true, true, 0f, 8f, 0.125f, 3f)]
    public Color color;
    //Explosion variables
    public float radius = 5.0F;
    public float power = 10.0F;
    AudioSource audio;
    //Game objects for the bottle.
    public GameObject Cork, Liquid, Glass, Glass_Shattered;
    //default despawn time;
    public float DespawnTime = 5.0f;
    //splash effect.
    public ParticleSystem Effect;
    //3D mesh on hte ground (given a specific height).
    public GameObject Splat;
    //such as the ground layer otherwise the broken glass could be considered the 'ground'
    public LayerMask SplatMask;
    //distance of hte raycast
    public float maxSplatDistance = 10.0f;
    //if the change in velocity is greater than this THEN it breaks
    public float shatterAtSpeed = 2.0f;
    //if the is disabled then it wont shatter by itself.
    public bool allowShattering = true;
    //if it collides with an object then and only then is there a period of 0.2f seconds to shatter.
    public bool onlyAllowShatterOnCollision = true;
    //for the ability to find the change in velocity.
    private Vector3 previousPos;
    private Vector3 previousVelocity;
    private Vector3 randomRot;
    //dont break if we have already broken, only applies to self breaking logic, not by calling Smash()
    bool broken = false;
    //timeout
    float collidedRecently = -1;
	void Start () {
        previousPos = transform.position;
         audio = GetComponent<AudioSource>();
    }
	//Smash function so it can be tied to buttons.
    public void RandomizeColor()
    {
        color = new Color(Random.Range(0, 1), Random.Range(0, 1), Random.Range(0, 1), 1);
    }

    void OnCollisionEnter(Collision collision)
    {
        //set a timer for about 0.2s to be able to be broken
        if(collision.transform.tag != "Liquid")
            collidedRecently = 0.2f;
        
    }

    public Vector3 GetRandomRotation()
    {
        return randomRot;
    }
    public void RandomRotation()
    {
        randomRot = (Random.insideUnitSphere + Vector3.forward).normalized;
    }

    public void Smash()
    {
        
        broken = true;
        audio.Play();
        //the Corks collider needs to be turned on;

        if (Cork != null)
        {
            Cork.transform.parent = null;
            Cork.GetComponent<Collider>().enabled = true;
            Cork.GetComponent<Rigidbody>().isKinematic = false;
            Destroy(Cork.gameObject, DespawnTime);
        }
        //the Liquid gets removed after n seconds
        if (Liquid != null)
        {
            float t = 0.0f;
            //if (Effect != null)
            //    t = (Effect.main.startLifetime.constantMin + Effect.main.startLifetime.constantMax)/2;
            Destroy(Liquid.gameObject, t);
        }
        //particle effect
        if(Effect != null)
        {
            Effect.Play();
            Destroy(Effect.gameObject, Effect.main.startLifetime.constantMax);
        }

        //now the label;
        
        //turn Glass off and the shattered on.
        if (Glass != null)
        {
            Destroy(Glass.gameObject);
        }
        if (Glass_Shattered != null)
        {
            Glass_Shattered.SetActive(true);
            Glass_Shattered.transform.parent = null;
            Destroy(Glass_Shattered, DespawnTime);
        }

        //instantiate the splat.
        RaycastHit info = new RaycastHit();
        if(Splat != null)
        if (Physics.Raycast(transform.position, Vector3.down, out info, maxSplatDistance, SplatMask))
        {
            //GameObject newSplat = Instantiate(Splat);
            //newSplat.transform.position = info.point;
            
        }
        Destroy(transform.gameObject, DespawnTime);
        //Explosion power
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
        }

    }
	// Update is called once per frame, for the change in velocity and all that jazz...
	void FixedUpdate () {


        collidedRecently -= Time.deltaTime;
        Vector3 currentVelocity = (transform.position - previousPos) / Time.fixedDeltaTime;
        if ((onlyAllowShatterOnCollision && collidedRecently >= 0.0f) | !onlyAllowShatterOnCollision)
        if(allowShattering)
        if(Vector3.Distance(currentVelocity,previousVelocity) > shatterAtSpeed)
        {
            if (!broken)
                Smash();
        }
        
        previousVelocity = currentVelocity;
        previousPos = transform.position;
	}
}
