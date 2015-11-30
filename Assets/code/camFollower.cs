using UnityEngine;
using System.Collections;

public class camFollower : MonoBehaviour {

	public float myDistance=10.0f;
	public float myHeight=5.0f;
	public GameObject myTarget;
	public GameObject myMount;
	public float myElasticStrength=0.5f;

	private Vector3 oldPos;
	private Quaternion oldRot;
	private Quaternion camRotationCurrent;
	private Vector3 camPosCurrent;


	void Start(){
		oldPos = transform.position;
		oldRot = transform.rotation;
	}
	
	// Update is called once per frame

		void LateUpdate() {
	
		camPosCurrent = Vector3.Lerp (oldPos, myMount.transform.position, Time.deltaTime * myElasticStrength);
		camRotationCurrent = Quaternion.Lerp (oldRot, myMount.transform.rotation, Time.deltaTime * myElasticStrength);

		transform.position = camPosCurrent;
		transform.rotation = camRotationCurrent;
		//transform.LookAt(myTarget.transform);

		oldPos = transform.position;
		oldRot = transform.rotation;
	}
}
