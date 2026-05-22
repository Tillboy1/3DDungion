using UnityEngine;

public class CameraOfset : MonoBehaviour
{
    public GameObject OfsetPoint;

    public void Update()
    {
        this.transform.position = OfsetPoint.transform.position;
    }
}
