using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelHp : MonoBehaviour
{
    private int maxHealth;
    public int WheelsHP;

    public void SetHealth(int amount)
    {
        WheelsHP = Mathf.Clamp(WheelsHP, 0, 50);

        WheelsHP = maxHealth = amount;
    }

    public IEnumerator DelayDamage(int amount, int delay)
    {
        yield return new WaitForSeconds(delay);
        //never go below 0
        WheelsHP = Mathf.Max(WheelsHP - amount, 0);
    }

}
