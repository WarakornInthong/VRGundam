using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[AddComponentMenu("Nokobot/Modern Guns/Simple Shoot")]
public class SimpleShoot : MonoBehaviour
{
    [Header("Prefab Refrences")]
    public GameObject bulletPrefab;
    public GameObject casingPrefab;
    public GameObject muzzleFlashPrefab;

    [Header("Location Refrences")]
    [SerializeField] private Animator gunAnimator;
    [SerializeField] private Transform barrelLocation;
    [SerializeField] private Transform casingExitLocation;

    [Header("Settings")]
    [Tooltip("Specify time to destory the casing object")] [SerializeField] private float destroyTimer = 2f;
    [Tooltip("Bullet Speed")] [SerializeField] private float shotPower = 500f;
    [Tooltip("Casing Ejection Speed")] [SerializeField] private float ejectPower = 150f;

    private bool buff = false;
    public Magazine magazine;
    public XRBaseInteractor socketInteractor;
    private bool hasSlide = true;
    [Header("Audio")]
    private AudioSource audioSource;
    [SerializeField]
    private GunAudio[] gunAudios;

    [System.Obsolete]
    void Start()
    {
        if (barrelLocation == null)
            barrelLocation = transform;

        if (gunAnimator == null)
            gunAnimator = GetComponentInChildren<Animator>();

        socketInteractor.onSelectEnter.AddListener(AddMagazine);
        socketInteractor.onSelectExit.AddListener(RemoveMagazine);
        audioSource = GetComponent<AudioSource>();
        
    }

    private AudioClip FindAudioClip(string name){
        for(int i = 0 ; i < gunAudios.Length ; i++){
            if(gunAudios[i].name == name)
                return gunAudios[i].audio;
        }
        return null;
    }

    public void AddMagazine(XRBaseInteractable interactable){

        // Mechanic
        magazine = interactable.GetComponent<Magazine>();
        hasSlide = false;
        // Audio
        audioSource.clip = FindAudioClip("Mag_Insert");
        audioSource.Play();
        
    }

    public void RemoveMagazine(XRBaseInteractable interactable){
        // Mechanic
        magazine = null;
        // Debug.Log("Remove");
        // Audio
        audioSource.clip = FindAudioClip("Mag_Remove");
        audioSource.Play();
    }

    public void Slide(){
        if(buff){
            // Mechanic
            hasSlide = true;
            // Audio
            audioSource.clip = FindAudioClip("Slide_Pull");
            audioSource.Play();
        }
        else{
            buff = true;
        }
        
    }
    public void ReleaseSlide(){
        // Audio
        audioSource.clip = FindAudioClip("Slide_Push");
        audioSource.Play();
    }
    public void UseTrigger(){
        // Have Ammo & slide
        if(magazine && magazine.numberOfBullet > 0 && hasSlide){
            gunAnimator.SetTrigger("Fire");
            // Audio
            audioSource.clip = FindAudioClip("Fire");
            audioSource.Play();
        }
        else if(!magazine || magazine.numberOfBullet == 0 || !hasSlide){
            // Audio
            audioSource.clip = FindAudioClip("Fire_Without_Slide");
            audioSource.Play();
        }
    }

    //This function creates the bullet behavior
    void Shoot()
    {
        if(magazine)
            magazine.numberOfBullet--;

        if (muzzleFlashPrefab)
        {
            //Create the muzzle flash
            GameObject tempFlash;
            tempFlash = Instantiate(muzzleFlashPrefab, barrelLocation.position, barrelLocation.rotation);

            //Destroy the muzzle flash effect
            Destroy(tempFlash, destroyTimer);
        }

        //cancels if there's no bullet prefeb
        if (!bulletPrefab)
        { return; }

        // Create a bullet and add force on it in direction of the barrel
        Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation).GetComponent<Rigidbody>().AddForce(barrelLocation.forward * shotPower);

    }

    //This function creates a casing at the ejection slot
    void CasingRelease()
    {
        //Cancels function if ejection slot hasn't been set or there's no casing
        if (!casingExitLocation || !casingPrefab)
        { return; }

        //Create the casing
        GameObject tempCasing;
        tempCasing = Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation) as GameObject;
        //Add force on casing to push it out
        tempCasing.GetComponent<Rigidbody>().AddExplosionForce(Random.Range(ejectPower * 0.7f, ejectPower), (casingExitLocation.position - casingExitLocation.right * 0.3f - casingExitLocation.up * 0.6f), 1f);
        //Add torque to make casing spin in random direction
        tempCasing.GetComponent<Rigidbody>().AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(100f, 1000f)), ForceMode.Impulse);

        //Destroy casing after X seconds
        Destroy(tempCasing, destroyTimer);
    }

    [System.Serializable]
    public class GunAudio{
        public string name;
        public AudioClip audio;
    }

}
