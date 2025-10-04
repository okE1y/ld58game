using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using System.Collections.Generic;

public class TypeSelectAttribute : PropertyAttribute
{
    public Type[] subClasses;
    public List<string> subClassesNames;

    public TypeSelectAttribute(Type baseType)
    {
        subClasses = baseType.Assembly.GetTypes().Where((x) => x.IsSubclassOf(baseType)).ToArray();

        subClassesNames = new List<string>();

        for (int i = 0; i < subClasses.Length; i++)
        {
            subClassesNames.Add(subClasses[i].Name);
        }
    }
}