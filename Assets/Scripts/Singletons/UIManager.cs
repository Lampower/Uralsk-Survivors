using TMPro;

public class UIManager: Singleton<UIManager>
{
    public string scoreTextStr = "Score: {0}";
    public string ammotTextStr = "{0}/{1}";

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI ammotText;

    Player player;

    private void Start()
    {
        player = GameManager.Instance.mainPlayer;
    }

    private void Update()
    {
        scoreText.text = string.Format(scoreTextStr, player.score);

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
    }
}