using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;

public class PlayerMovement : NetworkBehaviour
{
    private Rigidbody rb;
    private Vector2 m_moveAmt;
    private Vector2 m_LookAmt;
    public float moveSpeed;
    public PlayerInput input;

    public Vector3 MouseLocation;
    public Vector3 hitPosition;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if (!IsOwner)
        {
            input.enabled = false;
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        this.GetComponent<PlayerStats>().enabled = true;
    }
    public void Update()
    {
        Looking();
    }
    private void FixedUpdate()
    {
        Walking();
    }

    public void MoveInput(InputAction.CallbackContext context)
    {
        m_moveAmt = context.ReadValue<Vector2>();
    }
    public void MovePointer(InputAction.CallbackContext context)
    {
        m_LookAmt = context.ReadValue<Vector2>(); // Problem is the value is counting the change of the mouse not using the mouse position of the mouse
        //Debug.Log(context.ReadValue<Vector2>());
    }
    public void Walking()
    {
        rb.position = new Vector3(rb.position.x + (m_moveAmt.y * moveSpeed), rb.position.y, rb.position.z + (-m_moveAmt.x * moveSpeed));
    }
    public void Looking()
    {
        Ray raytest = Camera.main.ScreenPointToRay(new Vector3(m_LookAmt.x, m_LookAmt.y));
        //Ray raytest = Camera.main.ScreenPointToRay(Mouse.current.position.value);

        RaycastHit rayinfo;

        if(Physics.Raycast(raytest, out rayinfo, 100f))
        {
            hitPosition = rayinfo.point;
            Debug.DrawLine(hitPosition, hitPosition + new Vector3(0, 3, 0));
        }

        Vector3 rotation = hitPosition - transform.position;

        float rotY = Mathf.Atan2(-rotation.z, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, rotY, 0);
    }

}
