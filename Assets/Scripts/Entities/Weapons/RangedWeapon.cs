using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class RangedWeapon : AbstractWeapon
{

    public UnityAction<RangedWeapon> OnFinishedReloading;

    public float range;
    public Bullet bullet;
    public Transform FirePoint;

    public float bulletSpeed = 1;

    public int currentAmmo;
    public int maxAmmo;

    public float reloadTime = 1;
    public override void Awake()
    {
        base.Awake();
        if (bullet == null)
            bullet = GameManager.Instance.BulletPrefab;

        currentAmmo = maxAmmo;
    }

    public abstract void Fire();

    /// <summary>
    /// Use this method in the end of every override to track reload action
    /// </summary>
    public virtual void Reload()
    {
        StartCoroutine(ReloadTimer());
    }

    public virtual IEnumerator ReloadTimer()
    {
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        OnFinishedReloading.Invoke(this);
    }

}