using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour {

    private Dictionary<int, GameObject> enemies = new Dictionary<int, GameObject>();
    public GameObject cookingSheet;
    public GameObject pan;
    public GameObject pot;
    public GameObject skillet;
    public GameObject porkachu;

	void Start () {
        initializeEnemyMap();
        GameManager gm = FindObjectOfType<GameManager>();
        GameObject enemy = enemies[gm.currentChef];
        Instantiate(enemy, enemy.transform.position, Quaternion.identity);
	}

    private void initializeEnemyMap() {
        enemies.Add(1, cookingSheet);
        enemies.Add(2, pan);
        enemies.Add(3, pot);
        enemies.Add(4, skillet);
        enemies.Add(5, porkachu);
    }
}
