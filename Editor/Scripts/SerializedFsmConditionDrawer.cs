using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;

namespace Helluys.FsmCore.Editor
{/*
    [CustomPropertyDrawer(typeof(SerializedFsmCondition))]
    public class SerializedFsmConditionDrawer : PropertyDrawer
    {
        public override void OnGUI (Rect rect, SerializedProperty property, GUIContent label) {
            EditorGUI.BeginProperty(rect, label, property);

            rect.height = EditorGUIUtility.singleLineHeight;
            property.isExpanded = EditorGUI.Foldout(rect, property.isExpanded, label, true);
            if (property.isExpanded) {
                EditorGUI.indentLevel++;

                rect = DrawParameter(rect, property);
                rect = DrawType(rect, property);
                DrawConstant(rect, property);

                EditorGUI.indentLevel--;
            }

            EditorGUI.EndProperty();
        }

        private static Rect DrawParameter (Rect rect, SerializedProperty property) {
            List<string> parameters = ExtractFsmParameters(property);

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

        private static Rect DrawType (Rect rect, SerializedProperty property) {
            // Retrieve type and check validity
            SerializedFsmCondition.Type type = (SerializedFsmCondition.Type) property.FindPropertyRelative("type").enumValueIndex;
            if (!IsAllowedConditionType(type, property)) {
                type = GetDefaultConditionType(property);
            }

            rect.yMin += EditorGUIUtility.singleLineHeight;
            rect.yMax += EditorGUIUtility.singleLineHeight;
            type = (SerializedFsmCondition.Type) EditorGUI.EnumPopup(rect, new GUIContent("Type"), type, t => IsAllowedConditionType(t, property));

            property.FindPropertyRelative("type").enumValueIndex = (int) type;

            return rect;
        }

        private static bool IsAllowedConditionType (Enum t, SerializedProperty property) {
            FiniteStateMachine fsm = property.serializedObject.targetObject as FiniteStateMachine;
            FsmParameter parameter = fsm.GetParameter(property.FindPropertyRelative("parameterName").stringValue);
            SerializedFsmCondition.Type type = (SerializedFsmCondition.Type) t;

            if (parameter is TriggerParameter) {
                return type == SerializedFsmCondition.Type.TRIGGER;
            } else if (parameter is BooleanParameter) {
                return type == SerializedFsmCondition.Type.EQUALS;
            } else if (parameter is IntegerParameter || parameter is FloatParameter) {
                return type == SerializedFsmCondition.Type.EQUALS || type == SerializedFsmCondition.Type.GREATER_THAN || type == SerializedFsmCondition.Type.SMALLER_THAN;
            } else {
                throw new NotSupportedException("Parameter type unknown: " + parameter.GetType());
            }
        }

        private static SerializedFsmCondition.Type GetDefaultConditionType (SerializedProperty property) {
            FiniteStateMachine fsm = property.serializedObject.targetObject as FiniteStateMachine;
            FsmParameter parameter = fsm.GetParameter(property.FindPropertyRelative("parameterName").stringValue);

            if (parameter is TriggerParameter) {
                return SerializedFsmCondition.Type.TRIGGER;
            } else if (parameter is BooleanParameter || parameter is IntegerParameter || parameter is FloatParameter) {
                return SerializedFsmCondition.Type.EQUALS;
            } else {
                throw new NotSupportedException("Parameter type unknown: " + parameter.GetType());
            }
        }

        private static Rect DrawConstant (Rect rect, SerializedProperty property) {
            rect.yMin += EditorGUIUtility.singleLineHeight;
            rect.yMax += EditorGUIUtility.singleLineHeight;
            EditorGUI.PropertyField(rect, property.FindPropertyRelative("constant"));

            return rect;
        }

        private static List<string> ExtractFsmParameters (SerializedProperty property) {
            FiniteStateMachine fsm = property.serializedObject.targetObject as FiniteStateMachine;
            List<string> parameters = new List<string>();
            foreach (KeyValuePair<string, FsmParameter> kvp in fsm.parameters) {
                parameters.Add(kvp.Key);
            }

            return parameters;
        }

        public override float GetPropertyHeight (SerializedProperty property, GUIContent label) {
            return property.isExpanded ? 4 * EditorGUIUtility.singleLineHeight : EditorGUIUtility.singleLineHeight;
        }
    }*/
}