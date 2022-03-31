using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JaganController : MonoBehaviour
{
    // Aiming
    public Transform aimPoint;
    public Transform upperChest;

    [SerializeField][Range (0,1)]
    private float aimSpeed;

    public LayerMask groundLayer;
    // public GameObject floor;
    public float fallingSpeed;
    private CharacterController character;
    private float gravity = -9.81f;

    // Controller
    [Range (4, 10)]
    public float moveSpeed;

    // Animation
    private Animator animator;

    public float rayLength;
    public Vector3 moveDirection;

    [SerializeField]
    private float hp = 100;
    private CharacterStatus characterStatus;
    
    void Start()
    {
        // Set Animation Fight Mode
        animator = GetComponent<Animator>();
        animator.SetBool("Aim",true);

        character = GetComponent<CharacterController>();
        if(GetComponent<CharacterStatus>() != null){
            characterStatus = GetComponent<CharacterStatus>();
            characterStatus.StartEngine(moveSpeed, hp, 100, 100, 100);
        }
    }

    void Update()
    {
        // Check Speed
        moveSpeed = characterStatus.GetMoveSpeed();
        if(moveSpeed >= 10){
            moveSpeed = 10;
        }
        else if(moveSpeed < 4f){
            moveSpeed = 4f;
        }
        hp = characterStatus.GetHP();
    }

    void FixedUpdate(){
        bool isGrounded = CheckIfGrounded();
        if(isGrounded)
            fallingSpeed = 0;
        else
            fallingSpeed += gravity * Time.fixedDeltaTime;
        if(character.enabled){
            
            // การเดิน
            character.Move(moveDirection);
            // การตก
            character.Move(Vector3.up * fallingSpeed * Time.fixedDeltaTime);
        }
    }

    void LateUpdate()
    {
        Aiming();
    }

    private void Aiming(){
        // Find Aim Vector
        Vector3 aimVector = aimPoint.position - upperChest.position;
        Quaternion aimRotation = Quaternion.LookRotation(aimVector);
        
        // Change Degree Setting
        Vector3 aimEulerAngles = aimRotation.eulerAngles;
        ChangeDegreeBound(ref aimEulerAngles);
        
        upperChest.rotation = Quaternion.Lerp(upperChest.rotation, aimRotation, aimSpeed);

        // Limit Rotation
        if(Mathf.Abs(aimEulerAngles.x) >= 30){
            if(aimEulerAngles.x > 0)
                upperChest.localEulerAngles = new Vector3(30, aimEulerAngles.y, aimEulerAngles.z);
            else
                upperChest.localEulerAngles = new Vector3(-30, aimEulerAngles.y, aimEulerAngles.z);
        }
    }

    private void ChangeDegreeBound(ref Vector3 eulerAngles){
        if(eulerAngles.x > 180){
            eulerAngles.x -= 360;
        }
        if(eulerAngles.y > 180){
            eulerAngles.y -= 360;
        }
        if(eulerAngles.z > 180){
            eulerAngles.z -= 360;
        }
    }

    bool CheckIfGrounded(){
        Vector3 rayStart = transform.TransformPoint(character.center);
        // float rayLength = character.center.y;
        bool hasHit = Physics.SphereCast(rayStart, 0, Vector3.down, out RaycastHit hiyInfo, rayLength, groundLayer);
        return hasHit;
    }

    public void GetTakingDamage(float damage){
            characterStatus.GetDamaged(damage);
            Debug.Log("Och!");
    }

    // IEnumerator CooldownDamage(){
    //     yield return new WaitForSeconds(0.5f);
    //     isTakingDamage = false;
    // }
}
