using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int health = 100;
	
    public bool isAlive()
    {
        return this.health > 0;
    }

    public void Hurt(int dmg)
    {
        this.health -= dmg;
    }
}
