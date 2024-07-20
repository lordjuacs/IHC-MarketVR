using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrabManagerLeft : MonoBehaviour
{
    private DistanceHandGrabInteractor handGrab;
    public string objectName;
    public GameObject canvas;
    //private GameObject panel;
    private void Awake()
    {
        handGrab = GetComponent<DistanceHandGrabInteractor>();
        canvas.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (handGrab != null && InteractorState.Select == handGrab.State)
        {
            var interactable = handGrab.SelectedInteractable;
            objectName = interactable.gameObject.name;
            //Debug.Log("producto " + objectName);
            canvas.SetActive(true);

        }
        else
        {
            objectName = "";
            canvas.SetActive(false);

        }

    } 
}
