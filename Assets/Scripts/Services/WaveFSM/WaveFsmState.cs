

public abstract class WaveFsmState : FsmState
{
    public WaveSpawnSystem waveSystem;

    public WaveFsmState(Fsm fsm, WaveSpawnSystem waveSystem) : base(fsm)
    {
        this.waveSystem = waveSystem;
    }
}