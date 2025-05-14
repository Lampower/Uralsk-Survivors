using UnityEngine;

public class WaveRunningState : WaveFsmState
{
    public WaveRunningState(Fsm fsm, WaveSpawnSystem waveSystem) : base(fsm, waveSystem)
    {
    }

    public override void Enter()
    {
        waveSystem.StartNewWave();
    }

    public override void Exit()
    {

    }

    public override void FixedUpdate()
    {
    }

    public override void Update()
    {
        if (waveSystem.spawnedInstances.Count <= 0)
        {
            waveSystem.OnWaveEnd?.Invoke();
            fsm.SetState<PendingWaveState>();
        }
    }
}
