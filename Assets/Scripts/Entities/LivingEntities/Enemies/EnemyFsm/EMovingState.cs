using UnityEditor;
using UnityEngine;

public class EMovingState : EnemyFsmState
{
    Transform target;
    public EMovingState(Fsm fsm, BaseEnemy enemy) : base(fsm, enemy)
    {
    }

    public override void Enter()
    {
        target = enemy.destinationSetter.target;
        enemy.StartMoving();
        enemy.StartRotation();
    }

    public override void Exit()
    {
    }

    public override void FixedUpdate()
    {
    }

    public override void Update()
    {

        if (Vector2.Distance(enemy.transform.position, target.position) < enemy.rangeToTarget)
        {
            fsm.SetState<EAttackingState>();
        }
    }
}