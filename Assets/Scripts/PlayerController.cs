using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed;
    public float rotationSpeed;
    public float verticalRotationRange;

    public float verticalRotation;
    public float horizontalRotation;

    public CharacterController cc;

    // Use this for initialization
    void Start () {
        Cursor.lockState = CursorLockMode.Locked;

        moveSpeed = 10.0f;
        rotationSpeed = 2.0f;
        verticalRotationRange = 90.0f;

        cc = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {

        //rotate
        //horizontal
        horizontalRotation = Input.GetAxis("Mouse X") * rotationSpeed;
        transform.Rotate(0, horizontalRotation, 0);

        //vertical
        verticalRotation -= Input.GetAxis("Mouse Y") * rotationSpeed;
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalRotationRange, verticalRotationRange);
        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

        //move
        float verticalSpeed = Input.GetAxis("Vertical") * moveSpeed;
        float horizontalSpeed = Input.GetAxis("Horizontal") * moveSpeed;

        Vector3 momentum = new Vector3(horizontalSpeed,0,verticalSpeed);

        momentum = transform.rotation * momentum;

        cc.SimpleMove(momentum);
	}
}
