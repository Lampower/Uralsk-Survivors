using UnityEngine;

public class PMovingState : PlayerFsmState
{
    public PMovingState(Fsm fsm, Player player) : base(fsm, player)
    {
    }

    public override void Enter()
    {
    }

    public override void Exit()
    {
    }

    public override void FixedUpdate()
    {
        player.Move();
    }

    public override void Update()
    {
        player.Rotate();

        if (player.isAttacking)
            player.CurrentWeapon.Attack();

        else if (player.isPickingUp)
            player.TryPickupNearbyWeapon();

        else if (player.isReloading) { fsm.SetState<PReloadingState>(); }
    }
}
