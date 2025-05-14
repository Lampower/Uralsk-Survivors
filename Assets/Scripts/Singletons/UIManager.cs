using TMPro;
using UnityEngine;

public class UIManager: Singleton<UIManager>
{
    public string scoreTextStr = "Score: {0}";
    public string ammotTextStr = "{0}/{1}";
    public string waveTextStr = "Left: {0}";
    public string hpTextStr = "HP: {0}";

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI ammotText;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI hpText;


    public SliderFiller waveSlider;

    public GameObject controls;

    Player player;

    private void Start()
    {
        player = GameManager.Instance.mainPlayer;

        waveSlider.targetValue = WaveSpawnSystem.Instance.timeBetweenWaves;

        WaveSpawnSystem.Instance.OnWaveEnd += OnWaveEnded;
        WaveSpawnSystem.Instance.OnWaveStart += OnWaveStarted;

        if (Application.platform == RuntimePlatform.Android)
        {
            controls.SetActive(true);
        }
        else
        {
            controls.SetActive(false);
        }
    }

    private void Update()
    {
        scoreText.text = string.Format(scoreTextStr, player.score);
        hpText.text = string.Format(hpTextStr, player.Health);


        if (player.CurrentWeapon && player.CurrentWeapon.TryGetComponent<RangedWeapon>(out var weaponInfo))
        {
            if (!ammotText.gameObject.activeSelf)
            {
                ammotText.gameObject.SetActive(true);
            }
            ammotText.text = string.Format(ammotTextStr, weaponInfo.currentAmmo, weaponInfo.maxAmmo);
        }
        else if (ammotText.gameObject.activeSelf)
        {
            ammotText.gameObject.SetActive(false);
        }



        var entitiesLeft = WaveSpawnSystem.Instance.spawnedInstances.Count;
        if ( entitiesLeft > 0)
        {
            waveText.gameObject.SetActive(true);
            waveText.text = string.Format(waveTextStr,entitiesLeft);
        }
        else
        {
            waveText.gameObject.SetActive(false);

        }

        
    }

    private void OnWaveEnded()
    {
        waveText.gameObject.SetActive(false);
        waveSlider.gameObject.SetActive(true);
    }

    private void OnWaveStarted()
    {
        waveText.gameObject.SetActive(true);
        waveSlider.gameObject.SetActive(false);
    }
}