using fsm;
using UnityEditor;
using UnityEngine;

public class FsmEditorWindow : EditorWindow
{
    private static FsmEditorWindow graphEditorWindow;
    private FsmGraph stateMachineGraph;
    private FsmGraphGUI stateMachineGraphGUI;
    private FiniteStateMachine currentFsm;

    [MenuItem("Window/Finite State Machine")]
    private static void Do () {
        graphEditorWindow = GetWindow<FsmEditorWindow>();
        graphEditorWindow.titleContent.text = "FSM Editor";
    }

    private void Awake () {
        stateMachineGraphGUI = CreateInstance<FsmGraphGUI>();
    }

    private void OnSelectionChange () {
        FiniteStateMachine fsm = Selection.activeObject as FiniteStateMachine;
        if (fsm != null) {
            currentFsm = fsm;
            stateMachineGraph = FsmGraph.Create(currentFsm);
            stateMachineGraphGUI.graph = stateMachineGraph;
        } else {
            currentFsm = null;
            stateMachineGraph = null;
            stateMachineGraphGUI.graph = null;
        }

        Repaint();
    }

    private void OnGUI () {
        if (graphEditorWindow && stateMachineGraphGUI != null && currentFsm != null) {
            stateMachineGraphGUI.BeginGraphGUI(graphEditorWindow, new Rect(0, 0, graphEditorWindow.position.width, graphEditorWindow.position.height));
            stateMachineGraphGUI.OnGraphGUI();
            stateMachineGraphGUI.EndGraphGUI();
        }
    }
}
