namespace Assets.Scripts.UI
{
    public class LogbookEvent
    {
        public delegate void LogbookEventHandler(string commandPrompt, string message);
        public static LogbookEventHandler Handler;
    }
}
