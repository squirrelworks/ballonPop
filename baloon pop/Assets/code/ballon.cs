using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]

public class ballon : MonoBehaviour {


	public AudioClip popSound;
	AudioSource audio;
	private bool isDead=false;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame

		void LateUpdate() {
		/*if (isDead) {

			if (!audio.isPlaying) {
				Destroy (gameObject);
			}
		}
		*/
	}

void ApplyDamage(float myDamage){

		gameObject.GetComponent<Renderer> ().enabled = false;
		gameObject.GetComponent<Collider> ().isTrigger = false;

		audio.PlayOneShot(popSound,0.7F);
		//isDead = true;


       
		Destroy(gameObject, popSound.length);

	}
}
