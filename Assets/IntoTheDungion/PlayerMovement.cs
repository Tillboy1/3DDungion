using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    private Vector2 m_moveAmt;
    private Vector2 m_LookAmt;
    public float moveSpeed;

    public Vector3 MouseLocation;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void Update()
    {
        Walking();
        Looking();
    }

    public void MoveInput(InputAction.CallbackContext context)
    {
        m_moveAmt = context.ReadValue<Vector2>();
    }
    public void Walking()
    {
        rb.position = new Vector3(rb.position.x + (m_moveAmt.y * moveSpeed), rb.position.y, rb.position.z + (-m_moveAmt.x * moveSpeed));
    }
    public void Looking()
    {

        MouseLocation = Camera.main.ScreenToWorldPoint(m_LookAmt);
        Vector3 rotation = MouseLocation - transform.position;

        float rotY = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, rotY, 0);
    }

    public void movePointer(InputAction.CallbackContext context)
    {
        m_LookAmt = context.ReadValue<Vector2>();
    }
}
