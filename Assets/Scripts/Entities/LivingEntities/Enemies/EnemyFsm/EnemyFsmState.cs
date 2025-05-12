public abstract class EnemyFsmState : FsmState
{
    protected BaseEnemy enemy;
    public EnemyFsmState(Fsm fsm, BaseEnemy enemy) : base(fsm)
    {
        this.enemy = enemy;
    }
}