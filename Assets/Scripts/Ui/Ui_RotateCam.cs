using UnityEngine;

public class Ui_RotateCam : MonoBehaviour
{
    public Transform cam;
    public Transform center;
    private float speed = 2;

    private void Start()
    {
        
    }
    private void Update()
    {
        cam.transform.RotateAround(center.position, Vector3.up, speed);
    }
}