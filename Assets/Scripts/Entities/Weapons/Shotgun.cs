using UnityEngine;

public class Shotgun : RangedWeapon
{

    public int bulletCount;

    public int shootAngle = 30;

    public override void Awake()
    {
        base.Awake();
    }

    public override void Update()
    {
        base.Update();
    }
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
