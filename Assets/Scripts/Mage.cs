using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : Enemy {

    public float fireRate;
    private float nextFire;
    public float stopTime;
    public GameObject shot;
    public Transform shotSpawn;

    private void Start() {
        this.spawn();
        this.trackPlayer();
        this.nextFire = Time.time + this.fireRate;
    }

    private void Update() {
        this.move();
        
        if (facingRight) {
            this.shotSpawn.transform.localPosition = new Vector3(Mathf.Abs(this.shotSpawn.transform.localPosition.x), this.shotSpawn.transform.localPosition.y);
        }
        else {
            this.shotSpawn.transform.localPosition = new Vector3(-Mathf.Abs(this.shotSpawn.transform.localPosition.x), this.shotSpawn.transform.localPosition.y);
        }

        if (Time.time > this.nextFire) {
            this.currentSpeed = 0;
            this.animator.SetBool("attack", true);
            if (Time.time > this.nextFire + this.stopTime) {
                this.nextFire = Time.time + this.fireRate;
                GameObject fireball = (GameObject)Instantiate(this.shot, this.shotSpawn.transform.position, this.shotSpawn.transform.rotation);
                if (facingRight)
                    fireball.GetComponent<Projectile>().spawn(this.angle - 15);
                else
                    fireball.GetComponent<Projectile>().spawn(this.angle + 15);
                fireball.GetComponent<Projectile>().setFacingRight(this.facingRight);
            }
        }
        else {
            this.animator.SetBool("attack", false);
            this.currentSpeed = this.speed;
        }
    }

}
