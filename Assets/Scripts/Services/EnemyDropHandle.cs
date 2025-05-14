


using Events;
using UnityEngine;

public class EnemyDropHandle: Singleton<EnemyDropHandle>
{
    public GameObject[] dropItems;

    public float dropChance = 0.5f;

    public void OnEntityDeath(EntityDiesEvent evt)
    {
        if (evt.diedEntity.TryGetComponent<BaseEnemy>(out var enemy))
        {
            var chance = Random.Range(0f, 1f);
            if (chance <= dropChance)
            {
                //var randomIndex = Random.Range(0, dropItems.Length);
                //var item = Instantiate(dropItems[randomIndex], enemy.transform.position, Quaternion.identity);
                //item.transform.SetPositionAndRotation(enemy.transform.position, Quaternion.identity);
            }
        }
    }
}