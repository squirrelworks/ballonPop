using UnityEngine;
using System.Collections;
//[RequireComponent(typeof(AudioSource))]

public class planeController : MonoBehaviour {

	public GameObject groundFollower;


	public float rollSpeed = 5.0f;
	public float pitchSpeed = 5.0f;
	public float mouseSensetivity=0.02f;

	public float engineRev=100.0f;
	public float maxEngineRev=4000.0f;
	public float maxAirspeed=100.0f;
	public float liftGenerated=0.1f;
	public float liftStartPercentage=0.25f;

	public float myAcceleration=0.2f;
	public float myDrag=0.1f;

	public float gravityForce=0.5f;


	// needed vars
	private float myRoll=0.0f;
	private float myAirspeed=0.0f;
	private float myAltitude=0.0f;
	private float myLift=0.0f;



		
		// Use this for initialization
	void Start () {
		myAirspeed = engineRev * 0.2f;
		//audio = GetComponent<AudioSource>();

		//Screen.lockCursor = true;

	}

	void OnGUI() {

		GUI.TextField(new Rect(10, 10, 200, 20), "alt: "+myAltitude, 25);

		GUI.TextField(new Rect(220, 10, 200, 20), "RPM: "+engineRev, 25);

		GUI.TextField(new Rect(430, 10, 200, 20), "airspeed: "+myAirspeed, 25);
	}


	
	// Update is called once per frame
	void Update () {

		// handle user inputs
		if (Input.GetKey ("z")) {

			if(engineRev<maxEngineRev){
				engineRev=engineRev+2.0f;
			}

		}

		if (Input.GetKey ("x")) {
			if(engineRev>0.0f){
			engineRev=engineRev-2.0f;
			}
		}


		
		/*
		float roll = -Input.GetAxis ("Horizontal") * rollSpeed;
		float pitch = Input.GetAxis ("Vertical") * pitchSpeed;
*/

		float mouseRoll = ((Screen.width * 0.5f) - Input.mousePosition.x) * mouseSensetivity;
		float mousePitch = ((Screen.height * 0.5f) - Input.mousePosition.y) * mouseSensetivity;

		Debug.Log ("mouseRoll: "+mouseRoll);


		float roll = mouseRoll; //rollSpeed * Input.GetAxis("Mouse X");
		float pitch = mousePitch*-1.0f; // pitchSpeed * Input.GetAxis("Mouse Y");

		
		// roll value
		myRoll = transform.eulerAngles.z;
		
		if(myRoll>180.0f){
			//Debug.Log ("corrected roll: "+(360-myRoll));
		}


		// calculate airspeed
		if(myAirspeed<(engineRev*0.2f)){
			myAirspeed=myAirspeed+myAcceleration;
			if(myAirspeed>maxAirspeed){
				myAirspeed=maxAirspeed;
			}
		}
		
		if(myAirspeed>(engineRev*0.2f)){

			myAirspeed=myAirspeed-myDrag;

		}

	

		//calculate lift
		myLift = 0.0f;

		if(myAirspeed>maxAirspeed*liftStartPercentage){
			myLift=myAirspeed*liftGenerated;
			Debug.Log ("lift: "+myLift);
		}

// move plane
		transform.Rotate(pitch,0.0f,roll);
		transform.Translate(Vector3.forward * myAirspeed * Time.deltaTime);
		transform.Translate(Vector3.up * myLift * Time.deltaTime);


		// get roll value 
	

		// div

		
		//Cast a raycast for altitude
		RaycastHit hit;

		float distance = 1000f;
		Vector3 targetLocation;
		
if(Physics.Raycast(transform.position, Vector3.down, out hit, distance)) {
		   targetLocation = hit.point;
			myAltitude=hit.distance;
			groundFollower.transform.position = targetLocation; 

			applyGravity(hit.point);
		}
	
	}


	//collision

	void OnTriggerEnter(Collider other) {
		//audio.PlayOneShot(popSound,0.7F);
		other.gameObject.SendMessage("ApplyDamage", 5.0F);
		//Destroy(other.gameObject);
	}
	
	
	
	// gravity
	
	private void applyGravity(Vector3 myGroundHit) {
	
	Vector3 targetPosition = myGroundHit;
		
		Vector3 currentPosition = this.transform.position;
	

			Vector3 directionOfTravel = targetPosition - currentPosition;
			//now normalize the direction, since we only want the direction information
			directionOfTravel.Normalize();
			//scale the movement on each axis by the directionOfTravel vector components
			
			this.transform.Translate(
				(directionOfTravel.x * gravityForce * Time.deltaTime),
				(directionOfTravel.y * gravityForce * Time.deltaTime),
				(directionOfTravel.z * gravityForce * Time.deltaTime),
				Space.World);

	}




}

