using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class HandTutorial : MonoBehaviour
{
    public float speed;
    private SkinnedMeshRenderer mesh;
    private Animator animator;
    private string animatorGripParam = "Grip";
    private string animatorTriggerParam = "Trigger";
    private string[] animatorButtonsParam = new string[4];
    private bool[] buttonsTarget = new bool[4];
    private float gripTarget;
    private float triggerTarget;
    private float gripCurrent;
    private float triggerCurrent;

    // ตั้งค่าการทำงานเริ่มต้นของมือ
    void Start()
    {
        // Animation
        animator = GetComponent<Animator>();

        // เเสดงผล
        mesh = GetComponentInChildren<SkinnedMeshRenderer>();

        for(int i = 0 ; i < buttonsTarget.Length ; i++ ){
            buttonsTarget[i] = false;
            animatorButtonsParam[i] = "button" + (i+1).ToString();
        }
    }

    void Update()
    {
        AnimateHand();
    }

    // รับค่า Grip
    public void SetGrip(float v)
    {
        gripTarget = v;
    }

    // รับค่า Trigger
    public void SetTrigger(float v)
    {
        triggerTarget = v;
    }

    // primary button
    public void SetButton1(bool b){
        buttonsTarget[0] = b;
    }

    // secondary button
    public void SetButton2(bool b){
        buttonsTarget[1] = b;
    }

    // menu button
    public void SetButton3(bool b){
        buttonsTarget[2] = b;
    }

    // analog button
    public void SetButton4(bool b){
        buttonsTarget[3] = b;
    }

    void AnimateHand(){

        // กดปุ่ม Grip 
        if(gripTarget != gripCurrent){
            gripCurrent = Mathf.MoveTowards(gripCurrent, gripTarget, Time.deltaTime * speed);
            animator.SetFloat(animatorGripParam, gripCurrent);
        }

        // กดปุ่ม Trigger 
        if(triggerTarget != triggerCurrent){
            triggerCurrent = Mathf.MoveTowards(triggerCurrent, triggerTarget, Time.deltaTime * speed);
            animator.SetFloat(animatorTriggerParam, triggerCurrent);
        }

        for(int i = 0 ; i < buttonsTarget.Length ; i++ ){
            animator.SetBool(animatorButtonsParam[i], buttonsTarget[i]);
        }
        
    }

    // ควบคุมการเเสดงมือ
    public void ToggleVisibility(){
        mesh.enabled = !mesh.enabled;
    }
}

