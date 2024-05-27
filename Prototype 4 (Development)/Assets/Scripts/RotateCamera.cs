using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class RotateCamera : MonoBehaviour
{
    public float rotationSpeed;
    public float rotationSpeedMouse = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float mouseInputX = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up,
            horizontalInput * rotationSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up,
            mouseInputX * rotationSpeedMouse);
    }
}
