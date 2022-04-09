using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class playerObj : MonoBehaviour
{
    // Start is called before the first frame update
    public int obj = 0;
    public TextMeshProUGUI objText ;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (obj==1) {
            objText.text = "GO to mark2";
        } 
        if (obj==2) {
            objText.text = "Go to next point";
        } 
        if (obj==3) {
            objText.text = "Into the Gundam";
        } 
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Objective"){
            obj = obj + 1;
        }
    }

    



}
