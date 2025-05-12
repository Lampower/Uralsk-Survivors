using Unity.VisualScripting;
using UnityEngine.WSA;
using UnityEngine;
using System.Collections;

public class AssaultRifle : RangedWeapon
{
    public override void Attack()
    {
        if (cooldown <= 0)
        {
            cooldown = 1 / bps;

            Fire();
        }

        cooldown -= Time.deltaTime;
    }

    public override void Fire()
    {
        if (currentAmmo <= 0)
        {
            return;
        }
        var bulletPref = Instantiate(bullet, FirePoint.position, FirePoint.rotation);
        bulletPref.Init(this, Holder);
        currentAmmo -= 1;
    }

}