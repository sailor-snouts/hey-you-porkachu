using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private bool IsHealth = true;
    [SerializeField]
    private int maxHealth = 100;
    [SerializeField]
    private SpriteRenderer healthFrame;
    [SerializeField]
    private SpriteRenderer loveFrame;
    private int health;
    private SpriteMask mask;

    private void Start()
    {
        this.mask = GetComponentInChildren<SpriteMask>();
        this.health = this.maxHealth;
        this.UpdateMask();
        if(this.IsHealth)
        {
            this.healthFrame.enabled = true;
            this.loveFrame.enabled = false;
        } else
        {
            this.loveFrame.enabled = true;
            this.healthFrame.enabled = false;
        }
    }

    public bool IsAlive()
    {
        return this.health > 0;
    }

    public bool IsLoved()
    {
        return this.health >= this.maxHealth;
    }

    public void HurtLove(int dmg)
    {
        if(!this.IsHealth)
        {
            dmg *= -1;
        }
        this.health = Mathf.Clamp(this.health - dmg, 0, this.maxHealth);
        this.UpdateMask();
    }

    private void UpdateMask()
    {
        float percent = 1f - (float) this.health / (float) this.maxHealth;
        this.mask.transform.localScale = new Vector3(percent, this.mask.transform.localScale.y, this.mask.transform.localScale.z);
    }
}
