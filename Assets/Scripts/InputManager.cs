using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onFoot;

    private PlayerMotor motor;
    private PlayerCam look;
    private bool isSprinting = false;
    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerCam>();
        Cursor.lockState = CursorLockMode.Locked;
        onFoot.Jump.performed += ctx => motor.Jump();
        onFoot.Sprint.started += ctx => isSprinting = true;
        onFoot.Sprint.canceled += ctx => isSprinting = false;
    }

    void FixedUpdate()
    {
        motor.MovProcess(onFoot.Movement.ReadValue<Vector2>(), isSprinting);
        Debug.Log("Sprinting: " + isSprinting);

    }

    void LateUpdate()
    {
        look.ProcessCam(onFoot.LookAround.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        onFoot.Enable();
    }

    private void OnDisable()
    {
        onFoot.Disable();
    }
}
