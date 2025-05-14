using Events;
using UnityEngine;
using Pathfinding;
using UnityEngine.UIElements;
using UnityEditor.Rendering.LookDev;
using UnityEngine.Events;


public class BaseEnemy : LivingEntity
{
    public AIPath path;
    public AIDestinationSetter destinationSetter;

    public AbstractWeapon weapon;

    public float rangeToTarget = 5;
    public float offsetToTarget = 2;

    public float moveSpeed = 10;
    public float rotationSpeed = 1080;

    public Fsm fsm;

    public UnityEvent<BaseEnemy> OnEntityDeath;

    public void Start()
    {
        path = GetComponent<AIPath>();
        destinationSetter = GetComponent<AIDestinationSetter>();
        if (destinationSetter)
        {
            destinationSetter.target = GameManager.Instance.mainPlayer.transform;
        }
        if (path)
        {
            path.maxSpeed = moveSpeed;
        }

        InitFsm();
    }
    public override void TakeDamage(int damage, LivingEntity takenBy = null)
    {
        Health -= damage;
        if (Health < 0)
        {
            Kill(takenBy);
        }

    }

    public virtual void InitFsm()
    {
        fsm = new Fsm();

        fsm.AddState(new EMovingState(fsm, this));
        fsm.AddState(new EAttackingState(fsm, this));

        fsm.SetState<EMovingState>();
    }

    public virtual void Kill(LivingEntity killer = null)
    {
        EntityDiesEvent evt = new EntityDiesEvent();
        evt.diedEntity = this;
        evt.killer = killer;
        GameEvents.OnEntityDies?.Invoke(evt);

        OnEntityDeath?.Invoke(this);

        if (evt.isCancelled) return;

        Destroy(this.gameObject);

    }

    public virtual void Update()
    {
        fsm.CurrentState.Update();
    }

    public virtual void FixedUpdate()
    {
        fsm.CurrentState.FixedUpdate();
    }

    public virtual void StartMoving()
    {
        path.canMove = true;
    }

    public virtual void StopMoving()
    {
        path.canMove = false;
    }

    public virtual void StartRotation()
    {
        path.enableRotation = true;
    }

    public virtual void StopRotation()
    {
        path.enableRotation = false; 
    }

    public virtual void Rotate()
    {
        var rotationAngle = destinationSetter.target.position - transform.position;
        float angle = Mathf.Atan2(rotationAngle.y, rotationAngle.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }

    public virtual void Attack()
    {

    }

    public virtual void AdjustPositionToTarget()
    {
        var dir = destinationSetter.target.transform.position - transform.position;
        RaycastHit2D[] hits = Physics2D.RaycastAll(
            transform.position, 
            dir, 
            Vector2.Distance(transform.position, destinationSetter.target.position)
        );

        foreach (var hit in hits) 
        {
            if (hit.collider && hit.collider.gameObject.layer == 3)
            {
                StartMoving();
                return;
            }
        }
        StopMoving();

        
        
    }

    public virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, destinationSetter.target.position);
    }
}