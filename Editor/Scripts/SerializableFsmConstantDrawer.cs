using UnityEditor;
using Helluys.FsmCore.Serialization;
using UnityEngine;

namespace Helluys.FsmCore.Editor
{
    [CustomPropertyDrawer(typeof(SerializableFsmConstant))]
    public class SerializableFsmConstantDrawer : PropertyDrawer
    {
        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label) {
            EditorGUI.BeginProperty(position, label, property);

            // Draw label
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            // Extract type
            SerializableFsmConstant.Type type = (SerializableFsmConstant.Type) property.FindPropertyRelative("type").enumValueIndex;

            // Draw value
            switch (type) {

                case SerializableFsmConstant.Type.BOOLEAN:
                    EditorGUI.PropertyField(position, property.FindPropertyRelative("booleanValue"), GUIContent.none);
                    break;

                case SerializableFsmConstant.Type.INTEGER:
                    EditorGUI.PropertyField(position, property.FindPropertyRelative("integerValue"), GUIContent.none);
                    break;

                case SerializableFsmConstant.Type.FLOAT:
                    EditorGUI.PropertyField(position, property.FindPropertyRelative("floatValue"), GUIContent.none);
                    break;

            }

            EditorGUI.EndProperty();
        }
    }
}