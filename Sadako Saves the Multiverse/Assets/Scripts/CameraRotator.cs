using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    public Vector3 defaultRotation;
    bool isFixing;
    public float fixSpeed;
    public float turnSpeed;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) {
            isFixing = true;
            
        }

        if (isFixing) {
            Quaternion newRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.Euler(defaultRotation), Time.deltaTime * fixSpeed);
            transform.localRotation = newRotation;
            if (Quaternion.Angle(transform.localRotation, Quaternion.Euler(defaultRotation)) < 1f) {
                isFixing = false;
                transform.localRotation = Quaternion.Euler(defaultRotation);
            }
        } else {
            if (Input.GetKey(KeyCode.Q)) {
                transform.Rotate(new Vector3(0, turnSpeed * -1 * Time.deltaTime, 0), Space.World);
            }
            if (Input.GetKey(KeyCode.E)) {
                transform.Rotate(new Vector3(0, turnSpeed * Time.deltaTime, 0), Space.World);
            }
        }


    }
}
