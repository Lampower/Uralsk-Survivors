using UnityEngine;

public class CameraRotateEffect: MonoBehaviour
{
    [SerializeField] Player Player;

    public float timerMax = 0.1f;
    public float timer = 0.1f;
    public float mod = 0.1f;
    public float zVal = 0.0f;

    public float zMin = 5f;
    public float zMax = 10f;

    public int dir = 1;

    private void Start()
    {
        timer = timerMax;
    }

    private void Update()
    {
        if (Player.moveDir != Vector2.zero)
        {
            Vector3 rot = new Vector3(0, 0, zVal);
            transform.eulerAngles = rot;
            
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                zVal += mod * dir;
                timer = timerMax;
            }

            if (zVal >= zMax)
            {
                dir = -1;
            }
            else if (zVal <= zMin)
            {
                dir = 1;
            }
        }
    }
}