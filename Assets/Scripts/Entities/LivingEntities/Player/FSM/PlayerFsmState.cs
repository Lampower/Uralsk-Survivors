public abstract class PlayerFsmState : FsmState
{
    protected Player player;
    public PlayerFsmState(Fsm fsm, Player player) : base(fsm)
    {
        this.player = player;
    }

}