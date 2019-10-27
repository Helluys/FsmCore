using UnityEngine;
using UnityEditor;
using Helluys.FsmCore.Serialization;
using System.Collections.Generic;

namespace Helluys.FsmCore.Editor
{
    [CustomPropertyDrawer(typeof(SerializedFsmCondition))]
    public class SerializedFsmConditionDrawer : PropertyDrawer
    {
        public override void OnGUI (Rect rect, SerializedProperty property, GUIContent label) {
            EditorGUI.BeginProperty(rect, label, property);

            rect.height = EditorGUIUtility.singleLineHeight;
            property.isExpanded = EditorGUI.Foldout(rect, property.isExpanded, label);
            if (property.isExpanded) {
                EditorGUI.indentLevel++;
                rect = DrawType(rect, property);
                rect = DrawParameter(rect, property);
                DrawConstant(rect, property);

                EditorGUI.indentLevel--;
            }

            EditorGUI.EndProperty();
        }

        private static Rect DrawType (Rect rect, SerializedProperty property) {
            rect.yMin += EditorGUIUtility.singleLineHeight;
            rect.yMax += EditorGUIUtility.singleLineHeight;
            EditorGUI.PropertyField(rect, property.FindPropertyRelative("type"));
            return rect;
        }

        private static Rect DrawParameter (Rect rect, SerializedProperty property) {
            // Retrieve FSM and its parameters from serializedObject
            FiniteStateMachine fsm = property.serializedObject.targetObject as FiniteStateMachine;
            List<string> parameters = new List<string>();
            foreach (KeyValuePair<string, FsmParameter> kvp in fsm.GetParameters()) {
                parameters.Add(kvp.Key);
            }

            // Retrieve selected index
            string parameterName = property.FindPropertyRelative("parameterName").stringValue;
            int selectedIndex = parameters.FindIndex(name => name.Equals(parameterName));

            // Draw parameter list label and popup while retrieving newly selected index
            rect.yMin += EditorGUIUtility.singleLineHeight;
            rect.yMax += EditorGUIUtility.singleLineHeight;
            Rect popuRect = EditorGUI.PrefixLabel(rect, GUIUtility.GetControlID(FocusType.Passive), new GUIContent("Parameter"));
            int newSelectedIndex = EditorGUI.Popup(popuRect, selectedIndex, parameters.ToArray());
            
            if (newSelectedIndex != selectedIndex) {
                property.FindPropertyRelative("parameterName").stringValue = parameters[newSelectedIndex];
            }

            return rect;
        }

        private static Rect DrawConstant (Rect rect, SerializedProperty property) {
            rect.yMin += EditorGUIUtility.singleLineHeight;
            rect.yMax += EditorGUIUtility.singleLineHeight;
            EditorGUI.PropertyField(rect, property.FindPropertyRelative("constant"));

            return rect;
        }

        public override float GetPropertyHeight (SerializedProperty property, GUIContent label) {
            return property.isExpanded ? 4 * EditorGUIUtility.singleLineHeight : EditorGUIUtility.singleLineHeight;
        }
    }
}