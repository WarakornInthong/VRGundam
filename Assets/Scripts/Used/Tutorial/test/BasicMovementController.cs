using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class BasicMovementController : MonoBehaviour
{
    private CharacterController character;
    private Vector3 currentMovementInput;
    private Vector3 currentLookInput;
    public float moveSpeed;
    public float camSpeed;
    public static bool currentEnterInput;
    public static bool currentLestMInput;
    void Start()
    {
        character = GetComponent<CharacterController>();
    }

    void FixedUpdate(){
        Movement();
        transform.Rotate(currentLookInput * camSpeed);
    }

    void Movement(){
        Quaternion headYaw = Quaternion.Euler(0, transform.eulerAngles.y, 0);
        Vector3 direction = headYaw * currentMovementInput;
        character.Move(direction * Time.fixedDeltaTime * moveSpeed);
    }
    public void OnMovement(InputAction.CallbackContext value){
        // Debug.Log(value.ReadValue<Vector2>());
        Vector2 inputMovement = value.ReadValue<Vector2>();
        currentMovementInput = new Vector3(inputMovement.x, 0, inputMovement.y);
    }

    public void OnLook(InputAction.CallbackContext value){
        // Debug.Log(value.ReadValue<Vector2>());
        Vector2 inputLook = value.ReadValue<Vector2>();
        currentLookInput = new Vector3(0, inputLook.x, 0);
    }

    public void OnEnterDown(InputAction.CallbackContext value){
        // Debug.Log(value.ReadValueAsButton());
        currentEnterInput = value.ReadValueAsButton();
    }

    public void OnLeftMouseDown(InputAction.CallbackContext value){
        // Debug.Log(value.ReadValueAsButton());
        currentLestMInput = value.ReadValueAsButton();
    }
}
