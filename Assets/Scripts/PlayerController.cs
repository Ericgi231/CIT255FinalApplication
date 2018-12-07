using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Models;

public class PlayerController : MonoBehaviour {

    //params
    public float characterMoveSpeed;
    public float cameraRotationSpeed;
    public float verticalRotationRange;
    public float jumpHeight;

    //vars
    private float verticalRotation = 0;
    private float horizontalRotation = 0;

    private float xVelocity = 0;
    private float zVelocity = 0;
    private float yVelocity = 0;

    private CharacterController cc;

    public Texture2D crosshairImage;

    public static Tool CurrentTool;

    // Use this for initialization
    void Start() {
        //lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        
        //set character to controll
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update() {
        UpdateCameraDirection();
        UpdatePlayerPosition();
        ReadOtherPlayerInputs();
    }

    /// <summary>
    /// manage input presses from player
    /// </summary>
    void ReadOtherPlayerInputs()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObject flashLight = GameObject.Find("FlashLight");
            flashLight.GetComponent<Light>().enabled = !flashLight.GetComponent<Light>().enabled;
        }
    }

    /// <summary>
    /// updates the direction the player is looking based on mouse input
    /// </summary>
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

    /// <summary>
    /// updates players physical location based on keyboard input
    /// </summary>
    private void UpdatePlayerPosition()
    {
        //set horizontal velocity
        zVelocity = Input.GetAxis("Vertical") * characterMoveSpeed;
        xVelocity = Input.GetAxis("Horizontal") * characterMoveSpeed;

        //add sprint
        if (Input.GetButton("Sprint"))
        {
            zVelocity = zVelocity * 2;
            xVelocity = xVelocity * 2;
        }

        //set vertical velocity
        yVelocity += Physics.gravity.y * Time.deltaTime;
        if (Input.GetButton("Jump") && cc.isGrounded)
        {
            yVelocity = jumpHeight;
        }

        //combine velocity into momentum
        Vector3 momentum = new Vector3(xVelocity, yVelocity, zVelocity);
        momentum = transform.rotation * momentum;

        //move player based on momentum
        cc.Move(momentum * Time.deltaTime);
    }

    /// <summary>
    /// set ui on load
    /// </summary>
    private void OnGUI()
    {
        //set cross hair
        float xMin = (Screen.width / 2) - (crosshairImage.width / 2);
        float yMin = (Screen.height / 2) - (crosshairImage.height / 2);
        GUI.DrawTexture(new Rect(xMin, yMin, crosshairImage.width, crosshairImage.height), crosshairImage);
    }
}
