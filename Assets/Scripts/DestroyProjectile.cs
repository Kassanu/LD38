using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyProjectile : MonoBehaviour {

    public AudioSource shieldDink;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "projectile") {
            Destroy(other.transform.parent.gameObject);
            this.shieldDink.Play();
        }
    }
}
