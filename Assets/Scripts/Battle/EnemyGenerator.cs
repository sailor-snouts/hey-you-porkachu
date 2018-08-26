using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour {

    public List<GameObject> enemies;

	void Start () {
        GameObject enemy = enemies[Random.Range(0, enemies.Count)];
        Instantiate(enemy, enemy.transform.position, Quaternion.identity);
	}
}
