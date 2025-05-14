using UnityEngine;
using UnityEngine.InputSystem;


public class GameInput : Singleton<GameInput>
{
    [SerializeField] public InputActionReference movement, interact, attack, mousePos, reload, look;

    private void Awake()
    {
        movement.asset.Enable();
    }
    private void Update()
    {
        
    }
}