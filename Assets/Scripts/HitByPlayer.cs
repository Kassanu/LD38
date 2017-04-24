using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitByPlayer : MonoBehaviour {

    public AudioSource hit;

	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Enemy") {
            Destroy(other.transform.parent.gameObject);
            this.hit.Play();
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("Collision: " + other);
    }
}
