using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Tools.Unity.EnumFlags.Editor
{
    [CustomPropertyDrawer(typeof(EnumFlags<>), true)]
    public class EnumMaskPropertyDrawer: PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property,
            GUIContent label)
        {
            EditorGUI.BeginChangeCheck();
            SerializedProperty prop = property.FindPropertyRelative("_flags");
            
            var targetObjectType = property.serializedObject.targetObject.GetType();
            var fieldInfo = targetObjectType.GetField(property.name, BindingFlags.Instance | BindingFlags.NonPublic);
            var fieldGenericType = fieldInfo.FieldType.GenericTypeArguments.First();
            
            var a = EditorGUI.MaskField(position, label, prop.intValue, Enum.GetNames(fieldGenericType));
            if (EditorGUI.EndChangeCheck())
            {
                prop.intValue = a;
            }
        }
    }
}