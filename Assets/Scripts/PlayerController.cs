using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float characterMoveSpeed;
    public float cameraRotationSpeed;
    public float verticalRotationRange;

    private float verticalRotation;
    private float horizontalRotation;

    private float xSpeed;
    private float ySpeed;
    private float zSpeed;

    private float yVelocity = 0;

    private CharacterController cc;

    // Use this for initialization
    void Start() {
        Cursor.lockState = CursorLockMode.Locked;

        characterMoveSpeed = 10.0f;
        cameraRotationSpeed = 2.0f;
        verticalRotationRange = 90.0f;

        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update() {
        UpdateCameraDirection();
        UpdatePlayerPosition();
    }

    private void UpdateCameraDirection()
    {
        //horizontal
        horizontalRotation = Input.GetAxis("Mouse X") * cameraRotationSpeed;
        transform.Rotate(0, horizontalRotation, 0);

        //vertical
        verticalRotation -= Input.GetAxis("Mouse Y") * cameraRotationSpeed;
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalRotationRange, verticalRotationRange);
        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }

    private void UpdatePlayerPosition()
    {
        yVelocity += Physics.gravity.y * Time.deltaTime;
        zSpeed = Input.GetAxis("Vertical") * characterMoveSpeed;
        xSpeed = Input.GetAxis("Horizontal") * characterMoveSpeed;
        if (Input.GetButton("Jump") && cc.isGrounded)
        {
            yVelocity = 4;
        }

        Vector3 momentum = new Vector3(xSpeed, yVelocity, zSpeed);
        momentum = transform.rotation * momentum;

        cc.Move(momentum * Time.deltaTime);
    }
}
