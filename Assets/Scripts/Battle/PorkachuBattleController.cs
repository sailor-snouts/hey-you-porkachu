using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PorkachuBattleController : EnemyController {

    private Dictionary<int, Sprite> porkachus = new Dictionary<int, Sprite>();
    private Inventory inventory;
    public Sprite dapperPork;
    public Sprite dressPork;
    public Sprite musclePork;
    public Sprite plainPork;

    // Use this for initialization
    void Start () {
        initializePorkachuMap();
        inventory = FindObjectOfType<Inventory>();
        int porkachuType = inventory.porkachuType;
        GetComponent<SpriteRenderer>().sprite = porkachus[porkachuType];
        this.y = this.transform.position.y;
        GameObject healthInstance = Instantiate(healthPrefab, healthPrefab.transform.position, Quaternion.identity);
        healthBar = healthInstance.GetComponent<HealthBar>();
        healthBar.ToggleHealthMode();
    }

    // Update is called once per frame
    void Update() {
        if (moving) {
            Move();
            return;
        }
        CalculateMove();
        Move();
    }

    private void initializePorkachuMap() {
        porkachus.Add(2, dapperPork);
        porkachus.Add(3, dressPork);
        porkachus.Add(4, musclePork);
        porkachus.Add(1, plainPork);
    }
}
