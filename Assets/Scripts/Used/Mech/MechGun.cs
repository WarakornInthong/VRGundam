using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechGun : MonoBehaviour
{
    public Transform pivot;
    public GameObject bullet;
    private LineRenderer line;
    public LayerMask layerMask;
    private float fireRate = 0;
    private AudioSource audioSource;

    void Start()
    {
        // line = GetComponent<LineRenderer>();
        audioSource = GetComponent<AudioSource>();
    }


    void Update()
    {
        // RaycastHit hit;
        // if(Physics.Raycast(pivot.position, pivot.forward, out hit, 50, layerMask)){
        //     line.SetPosition(0, pivot.position);
        //     line.SetPosition(1, hit.point); 
        // }
        // else{
        //     line.SetPosition(0, pivot.position);
        //     line.SetPosition(1, pivot.position + pivot.forward * 50);
        // }
    }
    
    void FixedUpdate()
    {
        if(fireRate > 0){
            fireRate -= Time.fixedDeltaTime;
        }
    }
    public void fire(){
        if(fireRate <= 0){
            GameObject b = Instantiate(bullet, pivot.position, pivot.rotation);
            b.GetComponent<Rigidbody>().AddForce(pivot.forward * 500, ForceMode.Impulse);
            fireRate = 0.25f;
            audioSource.Play();
        }
    }
}
