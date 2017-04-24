using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc : Enemy {

    private void Start() {
        this.spawn();
        this.trackPlayer();
    }

    private void Update() {
        this.move();
    }

}
