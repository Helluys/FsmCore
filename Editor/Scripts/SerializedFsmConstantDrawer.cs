using UnityEditor;
using UnityEngine;

namespace Helluys.FsmCore.Editor
{/*
    [CustomPropertyDrawer(typeof(SerializedFsmConstant))]
    public class SerializedFsmConstantDrawer : PropertyDrawer
    {
        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label) {
            EditorGUI.BeginProperty(position, label, property);

            // Draw label
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            // Extract type
            SerializedFsmConstant.Type type = (SerializedFsmConstant.Type) property.FindPropertyRelative("type").enumValueIndex;

            // Draw value
            switch (type) {

                case SerializedFsmConstant.Type.BOOLEAN:
                    EditorGUI.PropertyField(position, property.FindPropertyRelative("booleanValue"), GUIContent.none);
                    break;

                case SerializedFsmConstant.Type.INTEGER:
                    EditorGUI.PropertyField(position, property.FindPropertyRelative("integerValue"), GUIContent.none);
                    break;

                case SerializedFsmConstant.Type.FLOAT:
                    EditorGUI.PropertyField(position, property.FindPropertyRelative("floatValue"), GUIContent.none);
                    break;

            }

            EditorGUI.EndProperty();
        }
    }*/
}