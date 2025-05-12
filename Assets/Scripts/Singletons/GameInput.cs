using UnityEngine;
using UnityEngine.InputSystem;


public class GameInput : Singleton<GameInput>
{
    [SerializeField] public InputActionReference movement, interact, attack, mousePos, reload;
    public InputActionMap Movement {  get; private set; }

   


    private void Awake()
    {
        movement.asset.Enable();
    }
    private void Update()
    {
        
    }
}