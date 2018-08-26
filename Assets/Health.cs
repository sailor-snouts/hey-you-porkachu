using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 10000;
    [SerializeField]
    private int health;
    private SpriteMask mask;

    private void Start()
    {
        this.mask = GetComponentInChildren<SpriteMask>();
        this.health = this.maxHealth;
    }

    public bool IsAlive()
    {
        return this.health > 0;
    }

    public void Hurt(int dmg)
    {
        this.health -= dmg;
        this.UpdateMask();
    }

    private void UpdateMask()
    {
        float percent = 1f -(float) this.health / (float) this.maxHealth;
        this.mask.transform.localScale = new Vector3(percent, this.mask.transform.localScale.y, this.mask.transform.localScale.z);
    }
}
