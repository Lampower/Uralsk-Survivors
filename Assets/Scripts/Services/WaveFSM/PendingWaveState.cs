using UnityEngine;

public class PendingWaveState : WaveFsmState
{
    public float localTimer = 0;
    public float timeToWait;
    public PendingWaveState(Fsm fsm, WaveSpawnSystem waveSystem) : base(fsm, waveSystem)
    {
    }

    public override void Enter()
    {
        localTimer = 0;
        timeToWait = waveSystem.timeBetweenWaves;
    }

    public override void Exit()
    {
    }

    public override void FixedUpdate()
    {
    }

    public override void Update()
    {
        localTimer += Time.deltaTime;
        if (localTimer >= timeToWait)
        {
            waveSystem.OnWaveStart?.Invoke();
            fsm.SetState<WaveRunningState>();
        }
    }
}
