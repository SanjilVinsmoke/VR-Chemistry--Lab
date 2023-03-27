using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainScene : MonoBehaviour {

	// Use this for initialization
	public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "LoadScene")
        {
            Debug.Log("Loading main scene");
            SceneManager.LoadScene("SGM Cool");
        }

        }
        
    }

