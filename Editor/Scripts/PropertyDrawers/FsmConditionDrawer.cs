using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;

namespace Helluys.FsmCore.Editor {
	[CustomPropertyDrawer(typeof(FsmCondition))]
	public class FsmConditionDrawer : PropertyDrawer {
		private class Context {
			public FiniteStateMachine fsm;
			public List<string> parameterNames;
			public FsmCondition.Type type;

			public Context(SerializedProperty property) {
				fsm = property.serializedObject.targetObject as FiniteStateMachine;
				parameterNames = new List<string>();
				foreach(FsmParameter parameter in fsm.parameters) {
					parameterNames.Add(parameter.name);
				}

				type = (FsmCondition.Type) property.FindPropertyRelative("_type").enumValueIndex;
			}
		}

		public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label) {
			Context context = new Context(property);

			EditorGUI.BeginProperty(rect, label, property);

			rect.height = EditorGUIUtility.singleLineHeight;
			property.isExpanded = EditorGUI.Foldout(rect, property.isExpanded, label, true);
			if(property.isExpanded) {
				EditorGUI.indentLevel++;

				rect = DrawParameter(rect, property, context);
				rect = DrawType(rect, property, context);
				DrawConstant(rect, property, context);

				EditorGUI.indentLevel--;
			}

			EditorGUI.EndProperty();
		}

		private static Rect DrawParameter(Rect rect, SerializedProperty property, Context context) {
			// Retrieve selected index
			string parameterName = property.FindPropertyRelative("_parameterName").stringValue;
			int selectedIndex = context.parameterNames.FindIndex(name => name.Equals(parameterName));

			// Draw parameter list label and popup while retrieving newly selected index
			rect.yMin += EditorGUIUtility.singleLineHeight;
			rect.yMax += EditorGUIUtility.singleLineHeight;
			Rect popuRect = EditorGUI.PrefixLabel(rect, GUIUtility.GetControlID(FocusType.Passive), new GUIContent("Parameter"));
			int newSelectedIndex = EditorGUI.Popup(popuRect, selectedIndex, context.parameterNames.ToArray());

			if(newSelectedIndex != selectedIndex) {
				property.FindPropertyRelative("_parameterName").stringValue = context.parameterNames[newSelectedIndex];
			}

			return rect;
		}

		private static Rect DrawType(Rect rect, SerializedProperty property, Context context) {
			// Retrieve type and check validity
			if(!IsAllowedConditionType(context.type, property, context)) {
				context.type = FsmCondition.Type.EQUALS;
			}

			rect.yMin += EditorGUIUtility.singleLineHeight;
			rect.yMax += EditorGUIUtility.singleLineHeight;
			context.type = (FsmCondition.Type) EditorGUI.EnumPopup(rect, new GUIContent("Type"), context.type, t => IsAllowedConditionType(t, property, context));

			property.FindPropertyRelative("_type").enumValueIndex = (int) context.type;

			return rect;
		}

		private static bool IsAllowedConditionType(Enum t, SerializedProperty property, Context context) {
			FsmParameter parameter = context.fsm.GetParameter(property.FindPropertyRelative("_parameterName").stringValue);
			FsmCondition.Type type = (FsmCondition.Type) t;

			switch(parameter.type) {
				case FsmParameter.Type.BOOLEAN:
					return type == FsmCondition.Type.TRIGGER || type == FsmCondition.Type.EQUALS;
				case FsmParameter.Type.INTEGER:
				case FsmParameter.Type.FLOAT:
					return type == FsmCondition.Type.EQUALS || type == FsmCondition.Type.GREATER_THAN || type == FsmCondition.Type.SMALLER_THAN;
				default:
					throw new NotSupportedException("Parameter type unknown: " + parameter.type);
			}
		}

		private static Rect DrawConstant(Rect rect, SerializedProperty property, Context context) {
			FsmParameter parameter = context.fsm.GetParameter(property.FindPropertyRelative("_parameterName").stringValue);

			rect.yMin += EditorGUIUtility.singleLineHeight;
			rect.yMax += EditorGUIUtility.singleLineHeight;

			switch(parameter.type) {
				case FsmParameter.Type.BOOLEAN:
					EditorGUI.PropertyField(rect, property.FindPropertyRelative("_boolValue"));
					break;
				case FsmParameter.Type.INTEGER:
					EditorGUI.PropertyField(rect, property.FindPropertyRelative("_intValue"));
					break;
				case FsmParameter.Type.FLOAT:
					EditorGUI.PropertyField(rect, property.FindPropertyRelative("_floatValue"));
					break;
				default:
					throw new NotSupportedException("Parameter type unknown: " + parameter.type);
			}

			return rect;
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
			return property.isExpanded ? 4 * EditorGUIUtility.singleLineHeight : EditorGUIUtility.singleLineHeight;
		}
	}
}