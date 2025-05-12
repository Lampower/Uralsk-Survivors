using Events;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : LivingEntity, IHolder, IMoveable, IRotateable
{
    private Fsm fsm;

    public AbstractWeapon CurrentWeapon;
    public Transform WeaponPivot;

    public Rigidbody2D rb;

    public float MovementSpeed;
    public float RotationSpeed;

    public float pickupRadius = 3;
    public float throwForce = 5;

    public Vector2 moveDir;
    public Vector2 lookDir;

    public bool isPickingUp = false;
    public bool isAttacking = false;

    public bool isReloading = false;

    public int score = 0;

    private void Awake()
    {
        EquipDefaultWeapon();


        fsm = new Fsm();

        fsm.AddState(new PMovingState(fsm, this));
        fsm.AddState(new PAfkState(fsm, this));
        fsm.AddState(new PReloadingState(fsm, this));

        fsm.SetState<PMovingState>();

        GameEvents.OnEntityDies += AddScoreForPlayer;
    }

    private void Update()
    {
        moveDir = GameInput.Instance.movement.action.ReadValue<Vector2>();
        var mousePos = GameInput.Instance.mousePos.action.ReadValue<Vector2>();
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        var newLookDir = (mousePos - (Vector2)transform.position);
        lookDir = newLookDir == Vector2.zero ? lookDir : newLookDir;

        isPickingUp = GameInput.Instance.interact.action.WasPerformedThisFrame();
        isAttacking = GameInput.Instance.attack.action.IsInProgress();
        isReloading = GameInput.Instance.reload.action.WasPerformedThisFrame();

        //Debug.Log($"{isPickingUp}, {isAttacking}, {isReloading}");

        fsm.CurrentState.Update();
    }

    private void FixedUpdate()
    {
        fsm.CurrentState.FixedUpdate();
    }

    public void Drop()
    {
        if (CurrentWeapon == null)
            return;

        var weapon = CurrentWeapon;
        weapon.transform.SetParent(null);
        weapon.Holder = null;

        var rb = weapon.rb;
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = 0;
        }

        var collider = weapon.col;
        if (collider != null)
            collider.enabled = true;

        CurrentWeapon = null;
    }

    public void TryPickupNearbyWeapon()
    {
        var hits = Physics2D.OverlapCircleAll(transform.position, pickupRadius);

        foreach (var hit in hits)
        {
            var weapon = hit.GetComponent<AbstractWeapon>();
            if (weapon != null && weapon.Holder == null)
            {
                Pickup(weapon);
                return;
            }
        }
        EquipDefaultWeapon();
    }

    private void EquipDefaultWeapon()
    {
        var fistsPrefab = GameManager.Instance.FistsPrefab;
        var fists = Instantiate(fistsPrefab, WeaponPivot.position, Quaternion.identity);
        Pickup(fists.GetComponent<AbstractWeapon>());
    }

    public void Pickup(AbstractWeapon weapon)
    {
        if (CurrentWeapon != null)
        {
            if (CurrentWeapon.GetType() == typeof(Fists))
                Destroy(CurrentWeapon.gameObject);
            else
                Drop();
        }

        if (weapon == null) 
        {
            return;    
        }

        CurrentWeapon = weapon;
        weapon.Holder = GetComponent<LivingEntity>();

        weapon.transform.SetParent(WeaponPivot);
        weapon.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

        var rb = weapon.rb;
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.linearVelocity = Vector2.zero;
        }

        var collider = weapon.col;
        if (collider != null)
            collider.enabled = false;
    }

    public void Move()
    {
        rb.MovePosition(rb.position + moveDir * MovementSpeed * Time.fixedDeltaTime);
    }


    public void Rotate()
    {
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        //Quaternion rotation = Quaternion.LookRotation(transform.forward, lookDir);

        //transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, RotationSpeed * Time.deltaTime);
    }

    public override void TakeDamage(int damage, LivingEntity takenBy = null)
    {
        Health -= damage;
    }

    public void AddScoreForPlayer(EntityDiesEvent evt)
    {
        if (evt.killer.GetHashCode() != this.GetHashCode())
            return;

        score += evt.diedEntity.ScoreForKilling;
    }


}