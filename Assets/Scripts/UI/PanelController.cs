using System;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class PanelController : MonoBehaviour
    {

        public UnityEngine.UI.Text textbox;
        public UnityEngine.UI.Image panel;

        // Update is called once per frame
        void Update()
        {
            panel.gameObject.SetActive(!String.IsNullOrEmpty(textbox.text));
        }
    }
}