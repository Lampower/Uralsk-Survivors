using UnityEngine;

public class EAttackingState : EnemyFsmState
{
    Transform target;
    public EAttackingState(Fsm fsm, BaseEnemy enemy) : base(fsm, enemy)
    {
    }

    public override void Enter()
    {
        target = enemy.destinationSetter.target;
        enemy.StopMoving();
        enemy.StopRotation();
    }

    public override void Exit()
    {
    }

    public override void FixedUpdate()
    {
        enemy.AdjustPositionToTarget();
    }

    public override void Update()
    {

        if (Vector2.Distance(enemy.transform.position, target.position) > enemy.rangeToTarget + enemy.offsetToTarget)
        {
            fsm.SetState<EMovingState>();
        }
    }
}