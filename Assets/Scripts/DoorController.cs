using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorController : MonoBehaviour {

    private bool IsOpening = false;
    private bool IsClosing = false;
    private bool CanChangeDoorState = true;
    public Text OpeningText;

    public float Speed = 40.0F;
    public float OpenRotation;
    public float CloseRotation;
    public bool IsOpened = false;
    public Axis Axis;

    // Zmienne na potrzeby samouczka
    private bool publishEvent = true;
    private bool isActive = true;

	// Use this for initialization
	void Start () {
        TaskCompletedEvent.Handler += TaskCompleted;
    }
	
	// Update is called once per frame
	void Update ()
    {
        var rotation = gameObject.transform.localEulerAngles;
        if (IsOpening)
        {
            float leftRotation = OpenRotation - (Axis == Axis.Y ? rotation.y : rotation.z);
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
            float leftRotation = (Axis == Axis.Y ? rotation.y : rotation.z) - CloseRotation;
            if (Time.deltaTime * Speed > leftRotation)
            {
                IsClosing = false;
                IsOpened = false;
                gameObject.transform.Rotate(Vector3.back * leftRotation);
            }
            else
                gameObject.transform.Rotate(Vector3.back * Time.deltaTime * Speed);
        }

        if (!Input.GetKey(KeyCode.E)) // Jeśli gracz ma puszczony przycisk, to może na nowo otworzyć drzwi
            CanChangeDoorState = true;
    }
    
    void OnTriggerStay(Collider col)
    {
        if (!isActive)
            return;
        if (Input.GetKey(KeyCode.E) && CanChangeDoorState)
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

            CanChangeDoorState = false;
            IsOpened = !IsOpened;

            if (publishEvent)
                TaskCompletedEvent.Handler(TaskNames.OpenDoor);
        }

        if (col.tag == "Player")
            SetOpenCloseText();
    }

    void OnTriggerExit(Collider col)
    {
        OpeningText.text = "";
    }

    void OnTriggerEnter(Collider col)
    {
        if (!isActive)
            return;
        if (col.tag == "Player")
            SetOpenCloseText();
    }

    void SetOpenCloseText()
    {
        if (IsOpened)
            OpeningText.text = "Naciśnij 'E', aby zamknąć drzwi";
        else
            OpeningText.text = "Naciśnij 'E', aby otworzyć drzwi";
    }

    void TaskCompleted(string name)
    {
        if (name == TaskNames.OpenDoor)
            publishEvent = false;
    }

    void Activate(object value)
    {
        if (value is bool)
        {
            if ((bool)value == true)
                isActive = true;
            else
                isActive = false;
        }
    }
}
