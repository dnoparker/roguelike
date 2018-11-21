using System;
using System.Collections.Generic;

namespace RogueLike
{
    public class EventManager
    {

        public delegate void EventDelegate<T>(T e) where T : GameEvent;
        private delegate void EventDelegate(GameEvent e);

        private Dictionary<Type, EventDelegate> delegates = new Dictionary<Type, EventDelegate>();
        private Dictionary<Delegate, EventDelegate> delegateLookup = new Dictionary<Delegate, EventDelegate>();
        private Dictionary<Delegate, Delegate> onceLookups = new Dictionary<Delegate, Delegate>();


        private EventDelegate AddDelegate<T>(EventDelegate<T> del) where T : GameEvent
        {
            // Early-out if we've already registered this delegate
            if (delegateLookup.ContainsKey(del))
                return null;

            // Create a new non-generic delegate which calls our generic one.
            // This is the delegate we actually invoke.
            EventDelegate internalDelegate = (e) => del((T)e);
            delegateLookup[del] = internalDelegate;

            EventDelegate tempDel;
            if (delegates.TryGetValue(typeof(T), out tempDel))
            {
                delegates[typeof(T)] = tempDel += internalDelegate;
            }
            else
            {
                delegates[typeof(T)] = internalDelegate;
            }

            return internalDelegate;
        }

        public void AddListener<T>(EventDelegate<T> del) where T : GameEvent
        {
            AddDelegate<T>(del);
        }

        public void AddListenerOnce<T>(EventDelegate<T> del) where T : GameEvent
        {
            EventDelegate result = AddDelegate<T>(del);

            if (result != null)
            {
                // remember this is only called once
                onceLookups[result] = del;
            }
        }

        public void RemoveListener<T>(EventDelegate<T> del) where T : GameEvent
        {
            EventDelegate internalDelegate;
            if (delegateLookup.TryGetValue(del, out internalDelegate))
            {
                EventDelegate tempDel;
                if (delegates.TryGetValue(typeof(T), out tempDel))
                {
                    tempDel -= internalDelegate;
                    if (tempDel == null)
                    {
                        delegates.Remove(typeof(T));
                    }
                    else
                    {
                        delegates[typeof(T)] = tempDel;
                    }
                }

                delegateLookup.Remove(del);
            }
        }

        public void RemoveAll()
        {
            delegates.Clear();
            delegateLookup.Clear();
            onceLookups.Clear();
        }

        public bool HasListener<T>(EventDelegate<T> del) where T : GameEvent
        {
            return delegateLookup.ContainsKey(del);
        }

        public void TriggerEvent(GameEvent e)
        {
            EventDelegate del;
            if (delegates.TryGetValue(e.GetType(), out del))
            {
                del.Invoke(e);

                // remove listeners which should only be called once
                foreach (EventDelegate k in delegates[e.GetType()].GetInvocationList())
                {
                    if (onceLookups.ContainsKey(k))
                    {
                        delegates[e.GetType()] -= k;

                        if (delegates[e.GetType()] == null)
                        {
                            delegates.Remove(e.GetType());
                        }

                        delegateLookup.Remove(onceLookups[k]);
                        onceLookups.Remove(k);
                    }
                }
            }
            else
            {
                Console.WriteLine("Event: " + e.GetType() + " has no listeners");
            }
        }

    }
}

public class GameEvent
{

}

public class OnPlayerSpotted : GameEvent
{
    public string enemyName;

    public OnPlayerSpotted(string enemyName)
    {
        this.enemyName = enemyName;
    }
}