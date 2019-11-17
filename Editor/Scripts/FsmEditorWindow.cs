using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Helluys.FsmCore.Editor {
	public class FsmEditorWindow : EditorWindow {
		private const string FsmEditorUxmlPath = "Assets/FsmEditor.uxml";
		private const string FsmEditorUssPath = "Packages/com.helluys.fsmcore/Editor/UI/FsmEditor.uss";

		private static FsmEditorWindow graphEditorWindow;
		private FiniteStateMachine currentFsm;

		private ScrollView parametersScrollView;

		[MenuItem("Window/Finite State Machine")]
		public static void OpenWindow() {
			graphEditorWindow = GetWindow<FsmEditorWindow>();
			EditorUtility.SetDirty(graphEditorWindow);
			graphEditorWindow.titleContent.text = "Helluys.FsmCore Editor";
		}

		private void OnEnable() {
			VisualTreeAsset uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(FsmEditorUxmlPath);
			StyleSheet uss = AssetDatabase.LoadAssetAtPath<StyleSheet>(FsmEditorUssPath);

			uxml.CloneTree(rootVisualElement);
			rootVisualElement.styleSheets.Add(uss);
		}

		private void OnDisable() {
			rootVisualElement.Clear();
		}

		private void OpenAsset(FiniteStateMachine fsm) {
			currentFsm = fsm;
			RebuildView();
		}

		[UnityEditor.Callbacks.OnOpenAsset(1)]
		public static bool OnOpenAsset(int instanceID, int line) {
			if(Selection.activeObject as FiniteStateMachine != null) {
				graphEditorWindow = GetWindow<FsmEditorWindow>();
				EditorUtility.SetDirty(graphEditorWindow);
				graphEditorWindow.OpenAsset(Selection.activeObject as FiniteStateMachine);
				return true;
			}

			return false;
		}

		private void RebuildView() {
			// Create toolbar generic menu
			ToolbarMenu fileMenu = rootVisualElement.Q<ToolbarMenu>(name: "file-menu");
			fileMenu.menu.AppendAction("Save", SaveAsset);

			// Bind parameter + button
			Button parameterButton = rootVisualElement.Q<Button>(name: "add-param-button");
			parameterButton.clickable.clicked += AddParameter;

			// Fill parameters view
			parametersScrollView = rootVisualElement.Q<ScrollView>(name: "ParametersView");
			if(parametersScrollView != null) {
				parametersScrollView.Clear();
				if(currentFsm != null) {
					foreach(FsmParameter param in currentFsm.parameters) {
						VisualElement paramRow = CreateParameterField(param);
						parametersScrollView.Add(paramRow);
					}
				}
			}
			Repaint();
		}

		private void AddParameter() {
			FsmParameter parameter = FsmParameter.NewBoolean("New parameter");
			currentFsm.AddParameter(parameter);
			parametersScrollView.Add(CreateParameterField(parameter));
			Repaint();
		}

		private void SaveAsset(DropdownMenuAction action) {
			Debug.Log("Saved");
		}

		private VisualElement CreateParameterField(FsmParameter param) {
			// Create a row container
			VisualElement paramRow = new VisualElement();
			paramRow.AddToClassList("Row");
			paramRow.style.flexGrow = 1;

			// Create the label
			TextField nameField = new TextField {
				value = param.name,
				isDelayed = true,
				isReadOnly = false
			};

			nameField.RegisterValueChangedCallback(changeEvent => {
				currentFsm.RenameParameter(param.name, changeEvent.newValue);
				EditorUtility.SetDirty(currentFsm);
			});

			nameField.style.width = 100;
			nameField.style.marginRight = 5;
			paramRow.Add(nameField);

			// Create the type field
			/*EnumField enumField = new EnumField(param.Value.Serialize(param.Key).type);
			enumField.style.flexGrow = 1;
			paramRow.Add(enumField);*/
			return paramRow;
		}
	}
}
