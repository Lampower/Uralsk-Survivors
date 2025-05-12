using Events;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public Fists FistsPrefab;
    public Bullet BulletPrefab;

    public Player mainPlayer;

    public List<AbstractWeapon> weaponsToDrop = new();

    public Scene currentScene;

    private void Awake()
    {
        mainPlayer = GameObject.FindAnyObjectByType<Player>();
        currentScene = this.gameObject.scene;

        GameEvents.OnEntityDies += OnPlayerDeath;
    }

    void OnPlayerDeath(EntityDiesEvent e)
    {
        if (e.diedEntity.GetHashCode() == mainPlayer.GetHashCode())
        {

        }
    }
}