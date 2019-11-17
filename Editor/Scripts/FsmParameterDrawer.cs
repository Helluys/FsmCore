using System;
using UnityEditor;
using UnityEngine;

namespace Helluys.FsmCore.Editor {
	[CustomPropertyDrawer(typeof(FsmParameter))]
	public class FsmParameterDrawer : PropertyDrawer {

		public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label) {
			rect.height = EditorGUIUtility.singleLineHeight;
			FiniteStateMachine fsm = (property.serializedObject.targetObject as FiniteStateMachine);

			// Draw name and propagate renames to the whole FSM
			EditorGUI.BeginProperty(rect, label, property);

			string oldName = property.FindPropertyRelative("_name").stringValue;
			string newName = EditorGUI.DelayedTextField(rect, "Name", property.FindPropertyRelative("_name").stringValue);
			if(!oldName.Equals(newName)) {
				fsm.RenameParameter(oldName, newName);
				property.serializedObject.Update();
			}

			// Draw type
			rect.yMin += EditorGUIUtility.singleLineHeight;
			rect.yMax += EditorGUIUtility.singleLineHeight;
			EditorGUI.PropertyField(rect, property.FindPropertyRelative("_type"));

			// Draw value
			rect.yMin += EditorGUIUtility.singleLineHeight;
			rect.yMax += EditorGUIUtility.singleLineHeight;
			string valuePropertyName;
			FsmParameter parameter = fsm.GetParameter(property.FindPropertyRelative("_name").stringValue);
			switch(parameter.type) {
				case FsmParameter.Type.BOOLEAN:
					valuePropertyName = "_boolValue";
					break;
				case FsmParameter.Type.INTEGER:
					valuePropertyName = "_intValue";
					break;
				case FsmParameter.Type.FLOAT:
					valuePropertyName = "_floatValue";
					break;
				default:
					throw new InvalidOperationException("Unkown parameter type " + parameter.type);
			}
			EditorGUI.PropertyField(rect, property.FindPropertyRelative(valuePropertyName));

			EditorGUI.EndProperty();
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
			return 3 * EditorGUIUtility.singleLineHeight;
		}
	}
}