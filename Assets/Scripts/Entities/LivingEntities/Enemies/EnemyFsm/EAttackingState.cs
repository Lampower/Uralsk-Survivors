using UnityEngine;

public class EAttackingState : EnemyFsmState
{
    Transform target;

    RangedWeapon rangedWeapon;
    public EAttackingState(Fsm fsm, BaseEnemy enemy) : base(fsm, enemy)
    {
    }

    public override void Enter()
    {
        target = enemy.destinationSetter.target;
        if (enemy.weapon && enemy.weapon.TryGetComponent<RangedWeapon>(out var weapon))
        {
            rangedWeapon = weapon;
        }
        enemy.StopMoving();
        enemy.StopRotation();
    }

    public override void Exit()
    {
        enemy.StartRotation();
    }

    public override void FixedUpdate()
    {
        enemy.AdjustPositionToTarget();
    }

    public override void Update()
    {
        enemy.Rotate();
        if (enemy.weapon != null) 
        {
            if (rangedWeapon && rangedWeapon.currentAmmo <= 0)
            {
                rangedWeapon.Reload();
            }
            else
            {
                enemy.weapon.Attack();
            }
        }
        if (Vector2.Distance(enemy.transform.position, target.position) > enemy.rangeToTarget + enemy.offsetToTarget)
        {
            fsm.SetState<EMovingState>();
        }
    }
}