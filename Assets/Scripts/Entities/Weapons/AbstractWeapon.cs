using Unity.VisualScripting;
using UnityEngine;

public abstract class AbstractWeapon : AbstractEntity, IAttackable, ICarriable
{
    public LivingEntity Holder = null;
    public int damage;

    public float bps = 1;
    public float cooldown = 0;

    public Rigidbody2D rb;
    public Collider2D col;

    public virtual void Awake()
    {
        cooldown = 1 / bps;

        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
        if (col == null)
            col = GetComponent<Collider2D>();
        col.isTrigger = true;
    }

    public virtual void Update()
    {
        if (cooldown > 0)
            cooldown -= Time.deltaTime;
    }
    
    public abstract void Attack();

    public virtual bool IsFriendlyFire(GameObject target)
    {
        if (!Holder)
            return true;
        return Holder.gameObject.CompareTag(target.tag);
    }
}