using UnityEngine;
using UnityEngine.InputSystem;

public class CameraHolder : MonoBehaviour
{
    public GameObject LocationOfset;
    public float OfsetDistnce;

    private void Update()
    {
        this.transform.position = new Vector3(LocationOfset.transform.position.x - OfsetDistnce, LocationOfset.transform.position.y, LocationOfset.transform.position.z);
    }
}
