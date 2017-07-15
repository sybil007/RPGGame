using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorController : MonoBehaviour {

    private bool IsOpening = false;
    private bool IsClosing = false;
    public Text OpeningText;

    public float Speed = 40.0F;
    public float OpenRotation;
    public float CloseRotation;
    public bool IsOpened = false;

	// Use this for initialization
	void Start () {
        OpeningText.text = "";
    }
	
	// Update is called once per frame
	void Update ()
    {
        var rotation = gameObject.transform.localEulerAngles;
        if (IsOpening)
        {
            float leftRotation = OpenRotation - rotation.z;
            if (Time.deltaTime * Speed > leftRotation)
            {
                IsOpening = false;
                IsOpened = true;
                gameObject.transform.Rotate(Vector3.forward * leftRotation);
            }
            else
                gameObject.transform.Rotate(Vector3.forward * Time.deltaTime * Speed);
        }
        else if (IsClosing)
        {
            float leftRotation = rotation.z - CloseRotation;
            if (Time.deltaTime * Speed > leftRotation)
            {
                IsClosing = false;
                IsOpened = false;
                gameObject.transform.Rotate(Vector3.back * leftRotation);
            }
            else
                gameObject.transform.Rotate(Vector3.back * Time.deltaTime * Speed);
        }
    }
    
    void OnTriggerStay(Collider col)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (IsOpened)
            {
                IsClosing = true;
                IsOpening = false;
            }
            else
            {
                IsOpening = true;
                IsClosing = false;
            }

            IsOpened = !IsOpened;
        }

        SetOpenCloseText();
    }

    void OnTriggerExit(Collider col)
    {
        OpeningText.text = "";
    }

    void OnTriggerEnter(Collider col)
    {
        SetOpenCloseText();
    }

    void SetOpenCloseText()
    {
        if (IsOpened)
            OpeningText.text = "Naciśnij 'E', aby zamknąć drzwi";
        else
            OpeningText.text = "Naciśnij 'E', aby otworzyć drzwi";
    }


}
