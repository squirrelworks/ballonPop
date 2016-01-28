using UnityEngine;
using System.Collections;

public class AutoRotator : MonoBehaviour {
	
	public float turnRate=1f;
	public int turnDirection=1;
	
	// Use this for initialization
	void Start () {
		
	
	}
	
	// Update is called once per frame
	void Update () {
	transform.RotateAround(Vector3.up, (turnRate * Time.deltaTime)*turnDirection);
	}
}
