using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Tasks;

public class TutorialController : MonoBehaviour
{
    private enum TutorialState
    {
        NotStarted = 0,
        Intro = 1,
        CameraRotation = 2,
        CameraZoom = 3,
        CharacterMovement = 4,
        CharacterSprint = 5,
        CharacterJump = 6,
        DoorOpening = 7,
        Exit = 8,
        Finished = 9
    }
    private DateTime lastChangeTime;
    private TutorialState state;
    private bool wrongShoeTaskDone, doorOpened;

    private float mouseX,mouseScroll;

    private bool WPressed, APressed, DPressed, SPressed;

    public GameObject trapObject;

	// Use this for initialization
	void Start ()
    {
        TaskCompletedEvent.Handler += TaskCompleted;

        trapObject = GameObject.Find("/Dom północny/_Door_parent/Door");
        trapObject.SendMessage("Activate", false);
	}
	
	// Update is called once per frame
	void Update ()
    {
	    switch(state)
        {
            case TutorialState.NotStarted:
                TaskTextboxChangeEvent.Handler("Witaj w samouczku! W kilku krokach zapoznasz się z podstawami sterowania i interakcji z otoczeniem.", 7);
                lastChangeTime = DateTime.Now;
                state = TutorialState.Intro;
                break;
            case TutorialState.Intro:
                if (DateTime.Now < lastChangeTime.AddSeconds(7))
                    break;
                TaskTextboxChangeEvent.Handler("Porusz myszką, aby obrócić kamerę i postać.", 20);
                state = TutorialState.CameraRotation;
                mouseX = Input.GetAxis("Mouse X");
                break;
            case TutorialState.CameraRotation:
                if (Math.Abs(Input.GetAxis("Mouse X") - mouseX) < 2)
                    break;
                TaskTextboxChangeEvent.Handler("Użyj pokrętła myszy, aby przybliżać i oddalać kamerę. Klikając 'TAB' możesz odwrócić kamerę 'za siebie'", 20);
                state = TutorialState.CameraZoom;
                mouseScroll = Input.GetAxis("Mouse ScrollWheel");
                break;
            case TutorialState.CameraZoom:
                if (Math.Abs(Input.GetAxis("Mouse ScrollWheel") - mouseScroll) < 0.1)
                    break;
                TaskTextboxChangeEvent.Handler("Bardzo dobrze, umiesz już kontrolować kamerę. Użyj przycisków W,S,A,D lub strzałek, aby poruszyć postacią", 20);
                state = TutorialState.CharacterMovement;
                break;
            case TutorialState.CharacterMovement:
                if (WPressed && SPressed && APressed && DPressed)
                {
                    state = TutorialState.CharacterSprint;
                    TaskTextboxChangeEvent.Handler("Naciśnij dodatkowo Shift, aby biec szybciej", 20);
                }
                break;
            case TutorialState.CharacterSprint:
                if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) 
                    && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) 
                        || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) 
                        || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) 
                        || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)))
                {
                    TaskTextboxChangeEvent.Handler("Naciśnij Spację, aby skoczyć", 20);
                    state = TutorialState.CharacterJump;
                }
                break;
            case TutorialState.CharacterJump:
                if (Input.GetKey(KeyCode.Space))
                {
                    trapObject.SendMessage("Activate", true);
                    TaskTextboxChangeEvent.Handler("Brawo, umiesz już sterować postacią. Drzwi niektórych budynków możesz otwierać. Podbiegnij do drzwi i naciśnij 'E', aby otworzyć je i wyjść na zewnątrz", 20);
                    state = TutorialState.DoorOpening;
                }
                break;
            case TutorialState.DoorOpening:
                if (doorOpened)
                {
                    TaskTextboxChangeEvent.Handler("Gratulacje, ukończyłeś samouczek! Pomóż postaciom koło głownego placu wioski", 20);
                    Assets.Scripts.UI.LogbookEvent.Handler("Samouczek", "Ukończono!");
                    state = TutorialState.Finished;
                }
                break;
        }

        if (state == TutorialState.CharacterMovement)
        {
            if (!WPressed && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)))
                WPressed = true;
            if (!SPressed && (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)))
                SPressed = true;
            if (!APressed && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)))
                APressed = true;
            if (!DPressed && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)))
                DPressed = true;
        }
	}

    void TaskCompleted(string taskName)
    {
        if (taskName == TaskNames.WrongShoes)
        {
            wrongShoeTaskDone = true;
            lastChangeTime = DateTime.Now;
        }
        else if (taskName == TaskNames.OpenDoor)
        {
            lastChangeTime = DateTime.Now;
            doorOpened = true;
        }
    }
}
