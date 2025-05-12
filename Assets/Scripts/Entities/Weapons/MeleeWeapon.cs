using Events;
using UnityEngine;

using System.Collections.Generic;

public abstract class MeleeWeapon: AbstractWeapon
{
    public Collider2D AttackCollider;
    public override void Update()
    {
        base.Update();
    }
    public override void Attack()
    {
        if (cooldown > 0)
            return;
        cooldown = bps;
        List<RaycastHit2D> hittedObjs = new List<RaycastHit2D>();
        AttackCollider.Cast(AttackCollider.transform.forward, hittedObjs);

        Vector2 origin = (Vector2)AttackCollider.transform.position + AttackCollider.offset;
        Vector2 size = AttackCollider.bounds.size;

        Debug.DrawRay(origin, AttackCollider.transform.forward * 0.5f, Color.red, 0.2f); // линия направления удара


        foreach (var obj in hittedObjs)
        {
            if (obj.transform.TryGetComponent<LivingEntity>(out var entity))
            {
                if (IsFriendlyFire(entity.gameObject)) continue;

                HitEntityEvent evt = new HitEntityEvent();
                evt.weaponHitted = this;
                evt.hittedObject = entity;
                evt.sender = Holder;
                GameEvents.OnHitEntity?.Invoke(evt);

                if (evt.isCancelled) { continue; }

                entity.TakeDamage(damage, this.Holder);
            }
        }
    }
}