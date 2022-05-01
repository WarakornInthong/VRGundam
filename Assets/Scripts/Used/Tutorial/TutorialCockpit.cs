using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCockpit : MonoBehaviour
{
    Animator animator;
    public bool isOpen = false;
    public TutorialJagan jaganController;
    public Transform deployGate;
    private int step = 0;//
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isOpen", isOpen);
        jaganController.GetComponent<Animator>().SetInteger("Step", step);
    }

    public void SetCockpit(bool status){
        isOpen = status;
    }

    public void SetupDeploy(){
        step = 1;
    }

    public void PrepareDeploy(){
        step = 2;
    }
}
