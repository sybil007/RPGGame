using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class MenuController : MonoBehaviour
    {

        bool isMenuVisible;
        GameObject menu;
        public Button continueButton;
        public Button exitButton;
        public Button newGameButton;

        // Use this for initialization
        void Start()
        {
            menu = GameObject.Find("MainMenu");
            menu.SetActive(false);

            continueButton.onClick.AddListener(() => DeactivateMenu());
            newGameButton.onClick.AddListener(() => Application.LoadLevel(Application.loadedLevel));
            exitButton.onClick.AddListener(() => Application.Quit());
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isMenuVisible)
                    DeactivateMenu();
                else
                    ActivateMenu();
            }

        }

        private void DeactivateMenu()
        {
            menu.SetActive(false);
            isMenuVisible = false;
            PauseEvent.Handler(false);
        }

        private void ActivateMenu()
        {
            menu.SetActive(true);
            isMenuVisible = true;
            PauseEvent.Handler(true);
        }
    }
}