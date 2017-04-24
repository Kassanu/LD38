using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public GameObject sprite;
    private Animator animator;
    public float speed = 1;
    public float radius = 0.5f;
    public float angle = 0;
    public bool facingRight;

    // Use this for initialization
    void Start () {
        animator = this.sprite.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        this.move();
	}

    public void setFacingRight(bool facingRight) {
        this.facingRight = facingRight;
        this.sprite.GetComponent<SpriteRenderer>().flipX = facingRight;
    }

    protected void move() {
        if (facingRight) {
            this.angle = (this.angle - (this.speed * Time.deltaTime)) % 360;
        } else {
            this.angle = (this.angle + (this.speed * Time.deltaTime)) % 360;
        }
        if (this.angle < 0) this.angle += 360;
        this.transform.position = new Vector3((Mathf.Cos(this.angle * Mathf.Deg2Rad) * this.radius), (Mathf.Sin(this.angle * Mathf.Deg2Rad) * this.radius));
        this.transform.eulerAngles = new Vector3(0, 0, (this.angle * Mathf.Deg2Rad) + Mathf.Atan2(this.transform.position.y, this.transform.position.x) * Mathf.Rad2Deg - 90);
    }

    public void spawn(float angle) {
        this.angle = angle;
    }

}
