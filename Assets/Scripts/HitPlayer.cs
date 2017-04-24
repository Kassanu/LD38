using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPlayer : MonoBehaviour {

    public GameController gc;

	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Enemy") {
            //Destroy(this.transform.parent.gameObject);
            this.gc.gameOver();
        }
        else if (other.tag == "projectile") {
           //Destroy(this.transform.parent.gameObject);
            this.gc.gameOver();
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("Collision: " + other);
    }

}
