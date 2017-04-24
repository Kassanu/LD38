using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    private bool isGameOver, restart;
    public GameObject[] enemyTypes;
    public float minSpawnWait, maxSpawnWait, startWait, lastSpawn, timeAlive;
    public int minEnemySpawn, maxEnemySpawn;
    public Player player;
    public Camera mainCamera;
    public GameObject blackScreen;
    public Text timeText;

    // Use this for initialization
    void Start () {
        this.isGameOver = false;
        this.restart = false;
        this.timeAlive = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (this.restart) {
            this.player.killPlayer();
            GameObject[] enemies;
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach(GameObject enemy in enemies) {
                Destroy(enemy);
            }
            
            if (Input.GetKeyDown(KeyCode.R)) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

            this.timeText.text = "You survived for " + (int)timeAlive + " seconds.";

            this.player.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane));
            this.blackScreen.SetActive(true);
        } else {
            this.timeAlive += Time.deltaTime;

            if (this.startWait <= 0) {
                if (Time.time > this.lastSpawn + Random.Range(this.minSpawnWait, this.maxSpawnWait)) {
                    this.lastSpawn = Time.time;
                    int eNum = Random.Range(this.minEnemySpawn, this.maxEnemySpawn+1);

                    for (int i = 0; i < eNum; i++) {
                        float rNum = Random.Range(0.0f, 10.0f);
                        GameObject enemy = (GameObject)Instantiate(this.enemyTypes[rNum >= 8 ? 1 : 0]);
                        float spawnRange = Random.Range((this.player.getAngle() + 180) - 90, (this.player.getAngle() + 180) + 90);
                        enemy.GetComponent<Enemy>().setAngle(spawnRange % 360);
                    }
                }
            } else {
                this.startWait -= 1;
            }
        }
    }

    public void gameOver() {
        this.isGameOver = true;
        this.restart = true;
    }

}
