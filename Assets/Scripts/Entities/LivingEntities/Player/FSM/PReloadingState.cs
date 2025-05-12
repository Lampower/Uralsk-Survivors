public class PReloadingState : PlayerFsmState
{
    RangedWeapon weapon;
    float prevSpeed;
    bool hasInited = false;

    float reduceSpeedBy = 20;
    public PReloadingState(Fsm fsm, Player player) : base(fsm, player)
    {
    }

    public override void Enter()
    {
        weapon = player.CurrentWeapon?.GetComponent<RangedWeapon>();
        if (!weapon)
        {
            fsm.SetState<PMovingState>();
            return;
        }
        weapon.OnFinishedReloading += FinishReloading;
        weapon.Reload();
        prevSpeed = player.MovementSpeed;
        player.MovementSpeed = prevSpeed * (100 - reduceSpeedBy) / 100;
        hasInited = true;
    }

    public override void Exit()
    {
        if (hasInited)
        {
            player.MovementSpeed = prevSpeed;
            weapon.OnFinishedReloading -= FinishReloading;
            hasInited = false;
        }
    }

    public override void FixedUpdate()
    {
        player.Move();
    }

    public override void Update()
    {
        player.Rotate();
    }

    void FinishReloading(RangedWeapon weapon)
    {
        fsm.SetState<PMovingState>();
    }
}
