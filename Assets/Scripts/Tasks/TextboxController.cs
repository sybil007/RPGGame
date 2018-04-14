using UnityEngine;

namespace Assets.Scripts.Tasks
{
    public class TextboxController : MonoBehaviour
    {
        public UnityEngine.UI.Text textbox;

        private object lockObject = new object();
        private float timeLeft = 0;

        public TextboxController()
        {
            TaskTextboxChangeEvent.Handler += OnNewText;
        }

        public void OnNewText(string newText, int seconds)
        {
            if (textbox == null)
                return;
            lock (lockObject)
            {
                timeLeft = seconds;
                textbox.text = newText;
            }
        }

        public void Update()
        {
            if (timeLeft == 0)
                return;

            lock (lockObject)
            {
                timeLeft -= Time.deltaTime;
                if (timeLeft <= 0)
                {
                    timeLeft = 0;
                    textbox.text = "";
                }
            }
        }
    }
}
