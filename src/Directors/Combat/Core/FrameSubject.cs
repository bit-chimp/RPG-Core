using System.Collections.Generic;
using Libraries.Patterns.Observer;
using UnityEngine;

namespace Mine.Combat
{
    public class FrameListener
    {
        private Dictionary<float, List<Observer>> m_listeners = new Dictionary<float, List<Observer>>();

        public void AddFrameListener(int frame, string id, IFrameObserver observer)
        {
            var obs = new Observer();
            obs.Id = id;
            obs.observer = observer;

            GetListeners(frame).Add(obs);
        }

        private List<Observer> GetListeners(float frame)
        {
            if (m_listeners.ContainsKey(frame) == false)
            {
                m_listeners.Add(frame, new List<Observer>());
            }

            return m_listeners[frame];
        }

        public void RemoveFrameListener(int frame, string id, IFrameObserver observer)
        {
            var listeners = GetListeners(frame);


            for (var i = listeners.Count - 1; i >= 0; i--)
            {
                var listener = listeners[i];
                if (listener.Id == id && listener.observer == observer)
                {
                    listeners.RemoveAt(i);
                }
            }
        }

        public void Update(AnimationEvent evt)
        {
            var listeners = GetListeners(evt.time);

            foreach (var listener in listeners)
            {
                listener.observer.OnAnimationEvent(evt);
            }
        }
    }

    struct Observer
    {
        public string Id;
        public IFrameObserver observer;
    }
}