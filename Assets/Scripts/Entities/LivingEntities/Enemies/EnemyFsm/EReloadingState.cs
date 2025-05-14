using UnityEngine;

public class EReloadingState : EnemyFsmState
{
    Transform target;

    RangedWeapon rangedWeapon;
    public EReloadingState(Fsm fsm, BaseEnemy enemy) : base(fsm, enemy)
    {
    }

    public override void Enter()
    {
        target = enemy.destinationSetter.target;
        if (enemy.weapon.TryGetComponent<RangedWeapon>(out var weapon))
        {
            rangedWeapon = weapon;
            rangedWeapon.OnFinishedReloading += FinishReloading;
        }
        enemy.StopMoving();
        //enemy.StopRotation();
    }

    public override void Exit()
    {
        rangedWeapon.OnFinishedReloading += FinishReloading;
    }

    public override void FixedUpdate()
    {
        //enemy.AdjustPositionToTarget();
    }

    public override void Update()
    {

    }
    void FinishReloading(RangedWeapon weapon)
    {
        fsm.SetState<PMovingState>();
    }
}