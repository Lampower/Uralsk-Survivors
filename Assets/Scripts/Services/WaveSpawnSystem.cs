using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaveSpawnSystem : Singleton<WaveSpawnSystem>
{
    SpawnEntityService spawnEntityService;

    public List<Transform> spawnPoints = new();

    //public List<BaseEnemy> enemyList = new();

    public List<BaseEnemy> spawnedInstances = new();

    public int currentWave = 0;

    public int startAmountOfEnemies = 10;

    public int enemiesToSpawn;

    public float enemyMultiplier = 1;

    public float timeBetweenWaves = 15f;

    public UnityAction OnWaveEnd;
    public UnityAction OnWaveStart;

    Fsm fsm;

    private void Awake()
    {
        fsm = new Fsm();

        fsm.AddState(new NoWaveState(fsm, this));
        fsm.AddState(new PendingWaveState(fsm, this));
        fsm.AddState(new WaveRunningState(fsm, this));

        fsm.SetState<NoWaveState>();
    }

    private void Update()
    {
        fsm.CurrentState.Update();
    }

    public void StartWaveSystem()
    {
        currentWave = 0;
        enemiesToSpawn = startAmountOfEnemies;

        foreach (var enemy in spawnedInstances)
        {
            enemy.OnEntityDeath.RemoveListener(OnEnemyDeath);
            Destroy(enemy.gameObject);
        }

        spawnedInstances.Clear();

        OnWaveEnd?.Invoke();
        fsm.SetState<PendingWaveState>();
    }

    public void StartNewWave()
    {
        currentWave++;

        enemiesToSpawn = startAmountOfEnemies + Mathf.RoundToInt(currentWave * enemyMultiplier + 1);
        SpawnEnemiesRandomly();
    }

    public void SpawnEnemiesRandomly()
    {
        List<Transform> shuffledSpawnPoint = new List<Transform>(spawnPoints);
        Shuffle(shuffledSpawnPoint);
        var length = shuffledSpawnPoint.Count;

       

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            var spawnIndex = i % length;
            var spawnPoint = shuffledSpawnPoint[spawnIndex];
            var enemy = SpawnEntityService.Instance.SpawnRandomEnemy(spawnPoint.position);
            spawnedInstances.Add(enemy);
            enemy.OnEntityDeath.AddListener(OnEnemyDeath);
        }
    }

    private void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int j = Random.Range(i, list.Count);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }

    public void StopRunning()
    {
        fsm.SetState<NoWaveState>();
    }

    private void OnEnemyDeath(BaseEnemy enemy)
    {
        spawnedInstances.Remove(enemy);
    }

}