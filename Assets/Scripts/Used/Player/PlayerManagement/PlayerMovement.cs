using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

// ควบคุมการเคลื่อนที่ของ player
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{

    [SerializeField]private XRNode inputSource;
    [SerializeField][Range(0,3)]private float speed = 1;

    public LayerMask groundLayer;
    // public GameObject floor;
    public float fallingSpeed;
    public float additionalHeight;

    private XROrigin rig;
    private Vector2 inputAxis;
    private CharacterController character;
    private float gravity = -9.81f;
    

    // ตั้งค่าเริ่มต้นเมื่อเริ่มเกม ตัว Rig ของ player, การควบคุมตัวละคร
    void Start() {
        rig = GetComponent<XROrigin>();
        character = GetComponent<CharacterController>();
    }
    void Update()
    {
        // เช็คการรับค่าของ joystick จาก controller
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
    }
    private void FixedUpdate() {
        // ควบคุมเเรงโน้มถ่วง การตกจากที่สูง
        bool isGrounded = CheckIfGrounded();
        if(isGrounded)
            fallingSpeed = 0;
        else
            fallingSpeed += gravity * Time.fixedDeltaTime;

        // ควบคุมการเคลื่อนที่ทั้งหมดของ player ว่าจะเป็นยังไง เดิน ตก หรือหัน รวมถึงการก้ม
        if(character.enabled){

            // การหันหน้าไปมา ให้การเคลื่อนที่เเละควบคุมตามทิศทางที่หัน
            CapsuleFollowHeadset();
            Quaternion headYaw = Quaternion.Euler(0, rig.Camera.transform.eulerAngles.y, 0);
            Vector3 direction = headYaw * new Vector3(inputAxis.x, 0, inputAxis.y);
            
            // การเคลื่อนที่
            character.Move(direction * Time.fixedDeltaTime * speed);

            // การตก
            character.Move(Vector3.up * fallingSpeed * Time.fixedDeltaTime);
        }

        
        
    }

    // เช็คว่ายืนอยู่บนพื้น
    bool CheckIfGrounded(){
        Vector3 rayStart = transform.TransformPoint(character.center);
        float rayLength = character.center.y + 0.01f;
        bool hasHit = Physics.SphereCast(rayStart, 0, Vector3.down, out RaycastHit hiyInfo, rayLength, groundLayer);
        return hasHit;
    }

    // เช็คการหันหน้า ทิศทางของ player เเละการก้ม
    void CapsuleFollowHeadset(){
        character.height = rig.CameraInOriginSpaceHeight + additionalHeight;
        Vector3 capsuleCenter = transform.InverseTransformPoint( rig.Camera.transform.position);
        character.center = new Vector3(capsuleCenter.x, character.height/2 + character.skinWidth, capsuleCenter.z);
    }
}
