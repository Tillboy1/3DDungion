using JetBrains.Annotations;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
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
        MouseLocation = Camera.main.ScreenToWorldPoint(new Vector3(m_LookAmt.x, 1, m_LookAmt.y));
        //Ray raytest = Camera.main.ScreenPointToRay(new Vector3(m_LookAmt.x, 1, m_LookAmt.y));

        //Debug.Log(m_LookAmt + " mouse " + MouseLocation + " Location");

        Vector3 rotation = MouseLocation - transform.position;
        //Debug.Log(rotation + "=" + MouseLocation + "-" + transform.position);

        float rotY = Mathf.Atan2(-rotation.z, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, rotY, 0);
    }

}
