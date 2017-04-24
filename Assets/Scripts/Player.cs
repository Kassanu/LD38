using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public bool facingRight = true;
    public Collider2D[] colliders;
    public float speed = (2 * Mathf.PI) / 5;
    private Animator animator;
    public GameObject sprite;
    private SpriteRenderer sr;
    private bool attacking = false;
    public float angle = 0;
    private float radius = 0.5f;
    private bool defending = false;
    private bool isDead = false;

    // Use this for initialization
    void Start () {
        this.sr = this.sprite.GetComponent<SpriteRenderer>();
        this.animator = this.sprite.GetComponent<Animator>();
        this.colliders[0].enabled = true;
        this.colliders[1].enabled = false;
        this.colliders[2].enabled = false;
        this.transform.position = new Vector3(Mathf.Cos(this.angle * Mathf.Deg2Rad) * this.radius, Mathf.Sin(this.angle * Mathf.Deg2Rad) * this.radius);
    }

	// Update is called once per frame
	void Update () {
        if (!this.isDead) {
            if (!defending && Input.GetKeyDown("space")) {
                attacking = true;
                this.colliders[1].enabled = true;
            }
            if (!defending && Input.GetKeyUp("space")) {
                attacking = false;
                this.colliders[1].enabled = false;
            }

            if (Input.GetKeyDown(KeyCode.LeftShift)) {
                this.defending = true;
                this.colliders[2].enabled = true;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift)) {
                this.defending = false;
                this.colliders[2].enabled = false;
            }

            float moveHorizontal = Input.GetAxis("Horizontal");
            if (moveHorizontal > 0) {

                this.angle = (this.angle - (this.speed * Time.deltaTime)) % 360;
                if (this.angle < 0) this.angle += 360;

                this.transform.position = new Vector3((Mathf.Cos(this.angle * Mathf.Deg2Rad) * this.radius), (Mathf.Sin(this.angle * Mathf.Deg2Rad) * this.radius));
                animator.SetInteger("speed", 1);
                sr.flipX = false;
                this.colliders[1].transform.localPosition = new Vector3(Mathf.Abs(this.colliders[1].transform.localPosition.x), this.colliders[1].transform.localPosition.y);
                this.colliders[2].transform.localPosition = new Vector3(Mathf.Abs(this.colliders[2].transform.localPosition.x), this.colliders[2].transform.localPosition.y);
            } else if (moveHorizontal < 0) {
                this.angle = (this.angle + (this.speed * Time.deltaTime)) % 360;
                if (this.angle < 0) this.angle += 360;
                this.transform.position = new Vector3(Mathf.Cos(this.angle * Mathf.Deg2Rad) * this.radius, Mathf.Sin(this.angle * Mathf.Deg2Rad) * this.radius);
                animator.SetInteger("speed", 1);
                sr.flipX = true;
                this.colliders[1].transform.localPosition = new Vector3(-Mathf.Abs(this.colliders[1].transform.localPosition.x), this.colliders[1].transform.localPosition.y);
                this.colliders[2].transform.localPosition = new Vector3(-Mathf.Abs(this.colliders[2].transform.localPosition.x), this.colliders[2].transform.localPosition.y);
            } else {
                animator.SetInteger("speed", 0);
            }

            this.transform.eulerAngles = new Vector3(0, 0, (this.angle * Mathf.Deg2Rad) + Mathf.Atan2(this.transform.position.y, this.transform.position.x) * Mathf.Rad2Deg - 90);
            animator.SetBool("attack", attacking);
            animator.SetBool("defend", defending);
        }
    }

    public void killPlayer() {
        this.isDead = true;
        this.transform.position = new Vector3(0, 0, 0);
        this.transform.eulerAngles = new Vector3(0, 0, 0);
        animator.SetInteger("speed", 0);
        animator.SetBool("attack", false);
        animator.SetBool("defend", false);
        animator.SetBool("dead", true);
    }

    public float getAngle() {
        return this.angle;
    }

}
