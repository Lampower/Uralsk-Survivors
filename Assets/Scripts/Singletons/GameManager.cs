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

    public GameObject PlayerUI;
    public GameObject DeathUI;


    private void Awake()
    {

        DeathUI.SetActive(false);
        PlayerUI.SetActive(true);

        mainPlayer = GameObject.FindAnyObjectByType<Player>();
        currentScene = this.gameObject.scene;


        GameEvents.OnEntityDies += OnPlayerDeath;
    }

    private void Start()
    {
        WaveSpawnSystem.Instance.StartWaveSystem();
    }

    void OnPlayerDeath(EntityDiesEvent e)
    {
        if (e.diedEntity.GetHashCode() == mainPlayer.GetHashCode())
        {
            WaveSpawnSystem.Instance.StopRunning();
            Time.timeScale = 0f;
            DeathUI.SetActive(true);
            PlayerUI.SetActive(false);

        }
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        DeathUI.SetActive(false);
        PlayerUI.SetActive(true);

        mainPlayer.Revive();
        WaveSpawnSystem.Instance.StartWaveSystem();

    }
}