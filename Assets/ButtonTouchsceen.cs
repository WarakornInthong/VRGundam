using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTouchsceen : MonoBehaviour
{
    private Button button;
    void Start()
    {
        button = GetComponent<Button>();
        // button.onClick.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Test(){
        Debug.Log("click");
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Hand")){
            button.onClick.Invoke();
        }
    }
}
