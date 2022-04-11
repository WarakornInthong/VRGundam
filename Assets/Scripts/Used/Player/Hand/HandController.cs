using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

// ควมคุมการใช้งานที่เกี่ยวกับมือแต่ละข้างทั้งหมด
public class HandController : MonoBehaviour
{
    public InputDevice device;
    public Hand hand;
    public HandTutorial hand_t;
    public float gripValue;
    public float triggerValue;
    public bool isGrab;
    [SerializeField]
    mode handMode;

    private XRNode node;
    // private bool isHold = false;


    // ตั้งค่าเริ่มต้นเริ่มเกม ที่เกี่ยวกับ player เเละสิ่งที่ใช้มือ
    void Start()
    {

        // เช็ครายละเอียดของมือว่ามาจากมือไหน
        node = GetComponent<XRController>().controllerNode;
        // เซ็ตการควบคุมอุปกรณ์
        device = InputDevices.GetDeviceAtXRNode(node);
        
    }

    void Update()
    {
        // กรณีที่จอยไม่ได้ทำงานเมื่อเริ่มเกมจะให้ทำการเช็คจนกว่าจอยจะพร้อมใช้งาน
        if(!device.isValid){
            // เซ็ตการควบคุมอุปกรณ์
            device = InputDevices.GetDeviceAtXRNode(node);
        }

        // ถ้าเกมหาจอยไม่เจอบรรทัดข้างล่างนี้ก็ทำงานไม่ได้อยู่ดี
        
        // Input
        if(handMode == mode.Story){
            // เช็คการรับค่าจากปุ่ม Grip
            device.TryGetFeatureValue(CommonUsages.grip, out gripValue);
            hand.SetGrip(gripValue);

            // เช็คการรับค่าจากปุ่ม Trigger
            device.TryGetFeatureValue(CommonUsages.trigger, out triggerValue);
            hand.SetTrigger(triggerValue);
        }
        else if(handMode == mode.Tutorial){
            device.TryGetFeatureValue(CommonUsages.grip, out gripValue);
            hand_t.SetGrip(gripValue);

            // เช็คการรับค่าจากปุ่ม Trigger
            device.TryGetFeatureValue(CommonUsages.trigger, out triggerValue);
            hand_t.SetTrigger(triggerValue);

            // primary button touch
            device.TryGetFeatureValue(CommonUsages.primaryTouch, out bool button1);
            hand_t.SetButton1(button1);

            // Secondary button touch
            device.TryGetFeatureValue(CommonUsages.secondaryTouch, out bool button2);
            hand_t.SetButton1(button2);

            // menu button touch
            device.TryGetFeatureValue(CommonUsages.menuButton, out bool button3);
            hand_t.SetButton1(button3);

            // analog axis touch
            device.TryGetFeatureValue(CommonUsages.primary2DAxisTouch, out bool button4);
            hand_t.SetButton1(button4);
        }

        


        
    }


    private enum mode{
        Story = 0,
        Tutorial = 1
    }
    
}
