using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelf : MonoBehaviour {
	private Animator ani;
	private bool smash;


	

	// Use this for initialization
	void Start () {
		ani = GetComponent<Animator>();
		smash = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ShelfFalling(){
		ani.SetBool("smash",true);
	}

	public void ShelfNotFalling(){
		ani.SetBool("smash",false);
	}
}
