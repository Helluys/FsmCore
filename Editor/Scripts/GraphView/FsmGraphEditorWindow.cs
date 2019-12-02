using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.UIElements;
using Helluys.FsmCore;
using Helluys.FsmCore.Editor.Data;
using Helluys.FsmCore.Editor.Views;

public class FsmGraphEditorWindow : EditorWindow {

	private const string FsmGraphEditorUssPath = "Assets/FsmGraphEditor.uss";

	private static FsmGraphEditorWindow graphEditorWindow;
	private FiniteStateMachine finiteStateMachine;
	private FsmGraphObject graphObject;

	private FsmGraphView _fsmGraphView = null;
	private FsmGraphView fsmGraphView {
		get { return _fsmGraphView; }
		set {
			if (_fsmGraphView != null ) {
				_fsmGraphView.RemoveFromHierarchy();
				_fsmGraphView.Dispose();
			}
		}
	}

	private void OnEnable() {
		StyleSheet uss = AssetDatabase.LoadAssetAtPath<StyleSheet>(FsmGraphEditorUssPath);
		if(!rootVisualElement.styleSheets.Contains(uss)) {
			rootVisualElement.styleSheets.Add(uss);
		}
	}

	private void OnDisable() {
		rootVisualElement.Clear();
	}

	[UnityEditor.Callbacks.OnOpenAsset(1)]
	public static bool OnOpenAsset(int instanceID, int line) {
		if(Selection.activeObject as FiniteStateMachine != null) {
			graphEditorWindow = GetWindow<FsmGraphEditorWindow>();
			graphEditorWindow.OpenAsset(Selection.activeObject as FiniteStateMachine);
			return true;
		}

		return false;
	}

	private void OpenAsset(FiniteStateMachine fsm) {
		finiteStateMachine = fsm;

		CreateGraph();
	}

	private void CreateGraph() {
		if (graphObject != null) {
			Destroy(graphObject);
		}

		graphObject = CreateInstance<FsmGraphObject>();
		graphObject.Initialize(finiteStateMachine);
		rootVisualElement.Add(new FsmGraphView(graphObject));
	}
}
