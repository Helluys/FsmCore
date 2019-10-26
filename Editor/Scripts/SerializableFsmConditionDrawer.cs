using UnityEngine;
using UnityEditor;
using Helluys.FsmCore.Serialization;
using System.Collections.Generic;

namespace Helluys.FsmCore.Editor
{
    [CustomPropertyDrawer(typeof(SerializableFsmCondition))]
    public class SerializableFsmConditionDrawer : PropertyDrawer
    {
        public override void OnGUI (Rect rect, SerializedProperty property, GUIContent label) {
            EditorGUI.BeginProperty(rect, label, property);

            rect.height = EditorGUIUtility.singleLineHeight;
            property.isExpanded = EditorGUI.Foldout(rect, property.isExpanded, label);
            if (property.isExpanded) {
                EditorGUI.indentLevel++;

                rect.yMin += EditorGUIUtility.singleLineHeight;
                rect.yMax += EditorGUIUtility.singleLineHeight;
                EditorGUI.PropertyField(rect, property.FindPropertyRelative("type"));

                FiniteStateMachine fsm = property.serializedObject.targetObject as FiniteStateMachine;

                List<string> parameters = new List<string>();
                foreach (KeyValuePair<string, FsmParameter> kvp in fsm.GetParameters()) {
                    parameters.Add(kvp.Key);
                }

                string parameterName = property.FindPropertyRelative("parameterName").stringValue;
                int selectedIndex = parameters.FindIndex(name => name.Equals(parameterName));

                rect.yMin += EditorGUIUtility.singleLineHeight;
                rect.yMax += EditorGUIUtility.singleLineHeight;
                GUIContent parameterLabel = new GUIContent("Parameter");
                Rect popuRect = EditorGUI.PrefixLabel(rect, GUIUtility.GetControlID(FocusType.Passive), parameterLabel);

                int newSelectedIndex = EditorGUI.Popup(popuRect, selectedIndex, parameters.ToArray());
                property.FindPropertyRelative("parameterName").stringValue = parameters[newSelectedIndex];

                // Selected parameter was changed, checked if constant type needs to be changed
                if (newSelectedIndex != selectedIndex) {
                    // TODO
                }

                rect.yMin += EditorGUIUtility.singleLineHeight;
                rect.yMax += EditorGUIUtility.singleLineHeight;
                EditorGUI.PropertyField(rect, property.FindPropertyRelative("constant"));

                EditorGUI.indentLevel--;
            }

            EditorGUI.EndProperty();
        }

        private static List<string> ExtractFsmParameters (SerializedProperty property) {
        }

        public override float GetPropertyHeight (SerializedProperty property, GUIContent label) {
            return property.isExpanded ? 4 * EditorGUIUtility.singleLineHeight : EditorGUIUtility.singleLineHeight;
        }
    }
}