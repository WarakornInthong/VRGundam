using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ControllerHider : MonoBehaviour
{
    public GameObject controllerObject = null;

    private PhysicsPoser physicsPoser = null;
    private XRDirectInteractor interactor = null;

    private void Awake() {
        physicsPoser = GetComponent<PhysicsPoser>();
        interactor = GetComponent<XRDirectInteractor>();
    }

    [System.Obsolete]
    private void OnEnable() {
        interactor.onSelectEntered.AddListener(Hide);
        interactor.onSelectExited.AddListener(Show);
    }

    [System.Obsolete]
    private void OnDisable() {
        interactor.onSelectEntered.RemoveListener(Hide);
        interactor.onSelectExited.RemoveListener(Show);
    }

    private void Hide(XRBaseInteractable interactable){
        controllerObject.SetActive(false);
    }

    private void Show(XRBaseInteractable interactable){
        StartCoroutine(WaitForRange());
    }

    private IEnumerator WaitForRange(){
        yield return new WaitWhile(physicsPoser.WithinPhysicsRange);
        yield return new WaitForSeconds(0.3f);
        controllerObject.SetActive(true);
    }
}
