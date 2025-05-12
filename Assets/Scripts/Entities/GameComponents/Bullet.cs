using Events;
using System.Collections;
using UnityEngine;

public class Bullet : AbstractEntity
{
    public Rigidbody2D rb;

    public RangedWeapon weapon;
    public LivingEntity sender;
    public int damage;

    public float range;
    public float bulletSpeed;

    public float flyTime;

    public float timer;

    Vector2 startPos;

    public void Init(RangedWeapon weapon, LivingEntity sender)
    {
        this.weapon = weapon;
        this.sender = sender;
        this.damage = weapon.damage;
        this.range = weapon.range;
        this.bulletSpeed = weapon.bulletSpeed;

        flyTime = range / bulletSpeed;

        timer = flyTime;

        startPos = transform.position;
        rb.linearVelocity = transform.right * bulletSpeed;
    }

    private void Update()
    {
        var curPos = transform.position;

        if (timer <= 0)
        {
            DestroyBullet();
        }
        if (Vector2.Distance(curPos, startPos) >= range)
        {
            DestroyBullet();
        }

        timer -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            DestroyBullet();
        }
        else if (collision.gameObject.TryGetComponent<LivingEntity>(out var entity))
        {
            if (weapon.IsFriendlyFire(entity.gameObject)) return;

            HitEntityEvent evt = new HitEntityEvent();
            evt.weaponHitted = weapon;
            evt.sender = weapon.Holder;
            evt.hittedObject = entity;
            GameEvents.OnHitEntity?.Invoke(evt);
            if (evt.isCancelled) return;
            DestroyBullet();
            entity.TakeDamage(weapon.damage, weapon.Holder);
        }
    }

    public void DestroyBullet()
    {
        StopAllCoroutines();
        Destroy(gameObject);
    }

    IEnumerator Fly()
    {
        rb.linearVelocity = transform.up * bulletSpeed;
        yield return new WaitForSeconds(bulletSpeed);
        rb.linearVelocity = Vector2.zero;
        DestroyBullet();
    }
}