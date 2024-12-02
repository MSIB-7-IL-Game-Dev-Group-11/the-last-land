using System.Collections.Generic;
using UnityEngine;

namespace TheLastLand._Project.Scripts.EventSystem.Common
{
    public abstract class EventChannel<T> : ScriptableObject
    {
        private readonly HashSet<EventListener<T>> _observers = new();

        public void Invoke(T value)
        {
            foreach (var observer in _observers)
            {
                observer.Raise(value);
            }
        }

        public void Register(EventListener<T> observer) => _observers.Add(observer);
        public void Deregister(EventListener<T> observer) => _observers.Remove(observer);
    }

    public readonly struct Empty
    {
    }
}