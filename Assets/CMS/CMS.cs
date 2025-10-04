using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;
using System.Linq;

public static class CMS
{
    private static CMSTable table;
    private static bool inited = false;
    public static void Init()
    {
        if (!inited)
        {
            table = new CMSTable();
        }
    }

    public static T Get<T>() where T : CMSEntity => table.Get<T>();

    public static CMSEntity Get(Type type) => table.Get(type);
}

public abstract class CMSEntity
{
    private Dictionary<Type, CMSComponent> tags = new Dictionary<Type, CMSComponent>();
    public T Define<T> () where T : CMSComponent, new()
    {
        Type type = typeof(T);
        if (tags.ContainsKey(type)) return tags[type] as T;

        T tag = Activator.CreateInstance(type) as T;

        tags.Add(type, tag);

        return tag;
    }
}

public class CMSTable
{
    public Dictionary<Type, CMSEntity> entities = new Dictionary<Type, CMSEntity>();

    public T Get<T>() where T : CMSEntity => entities[typeof(T)] as T;
    public CMSEntity Get(Type type) => entities[type];

    public CMSTable()
    {
        entities = ReflectionUtil.GetAllInstances<CMSEntity>().ToDictionary(keySelector: (x) => x.GetType(), elementSelector: (x) => x);
    }
}

public abstract class CMSComponent
{

}