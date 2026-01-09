using System;

namespace AMU.store.Mngt.Events
{
    public static class EventBus
    {
        public static event Action<string, object> OnEvent;

        public static void Publish(string name, object payload = null)
        {
            try { OnEvent?.Invoke(name, payload); } catch { }
        }
    }
}
