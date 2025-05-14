


using System.Collections.Generic;
using UnityEngine;

public class SpawnEntityService : Singleton<SpawnEntityService>
{
    public List<BaseEnemy> EnemiesPool = new List<BaseEnemy>();

    public BaseEnemy SpawnEnemy(BaseEnemy enemyPrefab, Vector2 position)
    {
        var enemy = Instantiate(enemyPrefab, position, Quaternion.identity);
        enemy.gameObject.SetActive(true);

        return enemy;
    }

    public BaseEnemy SpawnRandomEnemy(Vector2 position)
    {
        var randomIndex = Random.Range(0, EnemiesPool.Count);
        var enemy = SpawnEnemy(EnemiesPool[randomIndex], position);
        return enemy;
    }
}