using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    class LogbookController : MonoBehaviour
    {
        public Text textbox;
        public RectTransform content;

        private StringBuilder builder = new StringBuilder();

        private void Start()
        {
            LogbookEvent.Handler += OnNewLog;
        }

        public void OnNewLog(string commandPrompt, string message)
        {
            builder.Append(Environment.NewLine).Append(commandPrompt).Append("> ").Append(message);
            textbox.text = builder.ToString();
            content.sizeDelta = new Vector2(textbox.minWidth, textbox.preferredHeight + 20);
        }
    }
}
