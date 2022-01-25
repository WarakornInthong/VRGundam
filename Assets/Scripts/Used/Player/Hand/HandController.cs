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
    public float gripValue;
    public float triggerValue;
    public bool isGrab;

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
        // เช็คการรับค่าจากปุ่ม Grip
        device.TryGetFeatureValue(CommonUsages.grip, out gripValue);
        hand.SetGrip(gripValue);

        // เช็คการรับค่าจากปุ่ม Trigger
        device.TryGetFeatureValue(CommonUsages.trigger, out triggerValue);
        hand.SetTrigger(triggerValue);


        
    }

    
}
