using System;
using System.Collections.Generic;

namespace Core
{
    sealed public class SingleManager : IDisposable
    {
        private static SingleManager instance;
        public static SingleManager Instance { get { return instance ?? (instance = new SingleManager()); } }

        private SingleManager() {
            Debug.Log("single manager init.");
        }

        private readonly Dictionary<Type, IDisposable> mangers = new Dictionary<Type, IDisposable>();

        public void Dispose() {
            foreach (var mag in mangers.Values) {
                mag?.Dispose();
            }
            mangers.Clear();
            GC.Collect();
            Debug.Log("single manager dispose.");
        }

        public T Get<T>() where T : IDisposable {
            return (T)Get(typeof(T));
        }

        public IDisposable Get(Type type) {
            if (!mangers.ContainsKey(type))
                mangers.Add(type, (IDisposable)Activator.CreateInstance(type));
            return mangers[type];
        }
    }
}