

using System;
using UnityEditor;
using UnityEngine;

namespace Events
{
    public class GameEvents
    {
        public static Action<HitEntityEvent> OnHitEntity;
        public static Action<WeaponPickupEvent> OnWeaponPickup;
        public static Action<SpawnEntityEvent> OnSpawnEntity;
        public static Action<SpawnPlayerEvent> OnSpawnPlayer;
        public static Action<EntityDiesEvent> OnEntityDies;
    }

    public class Event
    {
        public bool isCancelled = false;
    }
    #region Spawn
    public class SpawnEntityEvent : Event
    {
        GameObject entity;
    }
    public class SpawnPlayerEvent : Event
    {
        GameObject player;
    }
    #endregion

    #region HitEvents
    public class HitEntityEvent : Event
    {
        public LivingEntity sender;
        public LivingEntity hittedObject;
        public AbstractWeapon weaponHitted;
    }
    public class EntityDiesEvent : Event
    {
        public LivingEntity killer;
        public LivingEntity diedEntity;
        //public AbstractWeapon weaponHitted;
    }
    #endregion

    #region Weapon

    public class WeaponPickupEvent : Event
    {
    }
    #endregion
}