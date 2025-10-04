#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Linq;
using System;

[CustomPropertyDrawer(typeof(TypeSelectAttribute))]
public class TypeSelectAttributeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        TypeSelectAttribute SelectAttribute = (TypeSelectAttribute)attribute;

        SerializedProperty TypeName = HardFindProperty("typeName", property.serializedObject);

        int index = SelectAttribute.subClassesNames.FindIndex((x) => x == TypeName.stringValue);

        EditorGUILayout.BeginHorizontal("Box");
        EditorGUILayout.LabelField(label);
        int newIndex = EditorGUILayout.Popup(index, SelectAttribute.subClassesNames.ToArray());
        EditorGUILayout.EndHorizontal();

        if (newIndex != -1)
        {
            TypeName.stringValue = SelectAttribute.subClassesNames[newIndex];
            property.serializedObject.ApplyModifiedProperties();
        }
    }

    private SerializedProperty HardFindProperty(string name, SerializedObject serializedObject)
    {
        SerializedProperty HardFind = serializedObject.GetIterator();

        while (HardFind.name != "typeName")
        {
            HardFind.NextVisible(true);
        }

        return HardFind;
    }
}

#endif