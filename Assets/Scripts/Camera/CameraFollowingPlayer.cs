using UnityEngine;

public class CameraFollowingPlayer: MonoBehaviour
{
    [SerializeField] Player Player;
    Vector3 mousePos;
    [SerializeField] bool followPlayer = true;
    [SerializeField] Camera cam;
    [SerializeField] float speed = 10;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (followPlayer) 
        {
            CamFollowPlayer();
        }
    }

    void CamFollowPlayer()
    {
        Vector3 newPos = Player.transform.position;
        newPos.z = cam.transform.position.z;
        this.transform.position = Vector3.MoveTowards(transform.position, newPos, speed * Time.deltaTime);
    }


}