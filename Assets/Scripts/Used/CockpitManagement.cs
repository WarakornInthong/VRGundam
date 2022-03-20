using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CockpitManagement : MonoBehaviour
{
    private Animator animator;
    private bool cockpitDoor = false;
    private bool isPlayAnimation = false;
    public GameObject player;
    public GameObject playerInCockpit;
    private bool isPower = false;
    public TextMeshProUGUI text;

    void Start()
    {
        player = GameObject.Find("XR Origin");
        animator = GetComponent<Animator>();
        animator.SetBool("isOpen", cockpitDoor);
    }


    // Update is called once per frame
    void Update()
    {
    }

    public void ActiveCockpitDoor(){
        if(!isPlayAnimation){
            cockpitDoor = !cockpitDoor;
            animator.SetBool("isOpen", cockpitDoor);
            StartCoroutine(PlayingAnimation());
        }
        // Debug.Log(cockpitDoor);
    }
    private IEnumerator PlayingAnimation(){
        isPlayAnimation = true;
        yield return new WaitForSeconds(1);
        isPlayAnimation = false;
    }

    public void CockpitPower(){
        isPower = !isPower;
        ActiveCockpitDoor();
        if(isPower){
            text.text = "Stop";
            Debug.Log("Start");
        }
        else{
            text.text = "Start";
            Debug.Log("Stop");
        }
        player.SetActive(!isPower);
        playerInCockpit.SetActive(isPower);
        
    }
}
