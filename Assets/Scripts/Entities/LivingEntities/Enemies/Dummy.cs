using Events;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : BaseEnemy
{
    public float Dps = 0;

    public float timer = 0;

    public Dictionary<float, int> RegisteredHits = new();
    public void Awake()
    {
        this.Health = 9999;
        GameEvents.OnHitEntity += LogHit;
    }

    public void OnDestroy()
    {
        GameEvents.OnHitEntity -= LogHit;
    }
    public override void TakeDamage(int damage, LivingEntity takenBy = null)
    {
        RegisteredHits.Add(Time.time, damage);
    }

    public override void InitFsm()
    {
        // Do not init
    }

    public override void Update()
    {
        // Do not update
    }

    public override void FixedUpdate()
    {
        // Do not update
    }
    float AvgInRegistered()
    {
        var sum = 0;
        var count = 0;
        var curTime = Time.time;
        var lastTime = curTime - 1;
        List<float> keysToRemove = new List<float>();
        foreach (var pair in RegisteredHits) {
            if (pair.Key < lastTime) 
                keysToRemove.Add(pair.Key);
            else
            {
                sum += pair.Value;
            }
        }

        foreach (var key in keysToRemove) 
            RegisteredHits.Remove(key);

        return sum;
    }

    void LogHit(HitEntityEvent e)
    {
        if (e.hittedObject.Equals(this))
        {
            print($"DPS: {AvgInRegistered()}, Damage: {e.weaponHitted.damage}");
        }
    }

    public override void OnDrawGizmos()
    {

    }
}
