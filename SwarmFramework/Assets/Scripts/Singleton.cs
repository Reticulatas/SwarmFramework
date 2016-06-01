using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
    public class BehaviourSingleton<T> : MonoBehaviour where T : class, new()
    {
        public static T instance;

        public BehaviourSingleton()
        {
            instance = this as T;
        }

        public static T get()
        {
            return instance;
        }
    }

    public class Singleton<T> where T : class, new()
    {
        public static T instance;

        public static T get()
        {
            if (instance == null)
                instance = new T();
            return instance;
        }
    }
}
