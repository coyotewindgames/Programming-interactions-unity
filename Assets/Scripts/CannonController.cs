using UnityEngine;

public class CannonController : MonoBehaviour
{
    public Transform barrelTransform;
    [SerializeField] private Transform[] wheelTransform;
    [SerializeField] private Transform cannonTransform;

    [SerializeField] private Transform barrelSpawnPoint;

    public float aimSpeed = 20f;
    public float turnSpeed = 25f;
    private Vector2 _aimRotation = Vector2.zero;
    public float aimRotationMin = -10f;
    public float aimRotationMax = 15f;
    public GameObject shotPrefab;
    public float projectileSpeed = 20f;

    public float projectileTorque = 10f;
    private Vector2 _turnRotation;
    public float turnRotation = 40f;
    public float turnRotationMin = -40f;

    public float wheelRotationSpeed = 50f;
    private Vector2 _wheelRotation;

    private SimpleControls _simpleControls;

    private float _lastFireTime = 0f;
    public float fireCooldown = 0.75f;

    private void Awake()
    {
        _simpleControls = new SimpleControls();
        _turnRotation = transform.localEulerAngles;
        turnRotationMin += _turnRotation.y;
        turnRotation += _turnRotation.y;
    }
    private void OnEnable()
    {
        _simpleControls.Enable();
        _simpleControls.gameplay.fire.performed += ctx =>
        {
            Fire();
        };
        _simpleControls.gameplay.menu.canceled += ctx =>
        {
            Menu.Instance.ToggleMenu();
        };
    }
    private void OnDisable()
    {
        _simpleControls.Disable();
    }

    private void Update()
    {
        Vector2 move = _simpleControls.gameplay.move.ReadValue<Vector2>();
        Aim(move.y);
        Turn(move.x);
        _lastFireTime += Time.deltaTime;
    }

    private void Aim(float aimDirection)
    {
        float scaledAimSpeed = aimSpeed * Time.deltaTime;
        float aimAmount = aimDirection * scaledAimSpeed;
        _aimRotation.x = Mathf.Clamp(_aimRotation.x + aimAmount, aimRotationMin, aimRotationMax);
        barrelTransform.localEulerAngles = _aimRotation;
    }

    private void Turn(float aimDirection)
    {
        float scaledTurnSpeed = turnSpeed * Time.deltaTime;
        float turnAmount = aimDirection * scaledTurnSpeed;
        _turnRotation.y = Mathf.Clamp(_turnRotation.y + turnAmount, turnRotationMin, turnRotation);
        transform.localEulerAngles = _turnRotation;

        _wheelRotation.x = _turnRotation.y;
        foreach (var wheel in wheelTransform)
        {
            wheel.localEulerAngles = _wheelRotation;
        }
    }

    private void Fire()
    {
        if (_lastFireTime >= fireCooldown){
        

        _lastFireTime = 0f;

        var cannonBall = Instantiate(shotPrefab, barrelSpawnPoint.position, barrelSpawnPoint.rotation);
        var rb = cannonBall.GetComponent<Rigidbody>();
        rb.AddTorque(Random.insideUnitSphere.normalized * projectileTorque, ForceMode.Impulse);
        rb.AddForce(barrelSpawnPoint.forward * projectileSpeed, ForceMode.Impulse);
        }
    }
}
