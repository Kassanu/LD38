using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public GameObject sprite;
    protected Animator animator;
    public float speed = 1;
    public float radius = 0.5f;
    public float angle = 0;
    protected float currentSpeed = 0;
    public bool facingRight;

    // Use this for initialization
    private void Start () {
        this.spawn();
    }
	
	// Update is called once per frame
	private void Update () {

    }

    public void setAngle(float angle) {
        this.angle = angle;
    }

    protected void spawn() {
        animator = this.sprite.GetComponent<Animator>();
        this.currentSpeed = this.speed;
        this.transform.position = new Vector3((Mathf.Cos(this.angle * Mathf.Deg2Rad) * this.radius), (Mathf.Sin(this.angle * Mathf.Deg2Rad) * this.radius));
        this.transform.eulerAngles = new Vector3(0, 0, (this.angle * Mathf.Deg2Rad) + Mathf.Atan2(this.transform.position.y, this.transform.position.x) * Mathf.Rad2Deg - 90);
    }

    protected void trackPlayer() {
        GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];
        float playerAngle = player.GetComponent<Player>().angle;

        float raw_diffPtoE = playerAngle - this.angle;
        float mod_diffPtoE = raw_diffPtoE % 360f;

        float raw_diffEtoP = this.angle - playerAngle;
        float mod_diffEtoP = raw_diffEtoP % 360f;

        if (mod_diffPtoE > mod_diffEtoP) {
            this.facingRight = false;
        } else {
            this.facingRight = true;
        }
        
    }

    protected void move() {
        this.sprite.GetComponent<SpriteRenderer>().flipX = !facingRight;
        if (this.currentSpeed > 0) {
            animator.SetInteger("speed", 1);
            if (facingRight) {
                this.angle = (this.angle - (this.speed * Time.deltaTime)) % 360;
            } else {
                this.angle = (this.angle + (this.speed * Time.deltaTime)) % 360;
            }
            if (this.angle < 0) this.angle += 360;
            this.transform.position = new Vector3((Mathf.Cos(this.angle * Mathf.Deg2Rad) * this.radius), (Mathf.Sin(this.angle * Mathf.Deg2Rad) * this.radius));
            this.transform.eulerAngles = new Vector3(0, 0, (this.angle * Mathf.Deg2Rad) + Mathf.Atan2(this.transform.position.y, this.transform.position.x) * Mathf.Rad2Deg - 90);
        }
        else {
            animator.SetInteger("speed", 0);
        }
    }
}
