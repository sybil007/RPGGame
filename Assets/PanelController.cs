using System;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour {

    public UnityEngine.UI.Text textbox;
    public UnityEngine.UI.Image panel;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        panel.gameObject.SetActive(!String.IsNullOrEmpty(textbox.text));
	}
}
