using UnityEngine;
using System;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

public static class ReflectionUtil
{
    public static Type[] GetAllSubclasses<T>() where T : class
    {
        return typeof(T).Assembly.GetTypes().Where((x) => x.IsSubclassOf(typeof(T))).ToArray();
    }

    public static Type[] GetAllSubclasses(Type type)
    {
        return type.Assembly.GetTypes().Where((x) => x.IsSubclassOf(type)).ToArray();
    }

    public static T[] GetAllInstances<T>() where T : class
    {
        Type[] types = GetAllSubclasses<T>();
        T[] instances = new T[types.Length];

        for (int i = 0; i < types.Length; i++)
        {
            instances[i] = Activator.CreateInstance(types[i]) as T;
        }

        return instances;
    }
}