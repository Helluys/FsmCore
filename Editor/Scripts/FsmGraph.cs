using System.Collections.Generic;
using fsm;
using UnityEditor.Graphs;
using UnityEngine;

internal class FsmGraph : Graph
{
    private IDictionary<FsmState, Node> fsmNodes = new Dictionary<FsmState, Node>();

    public static FsmGraph Create (FiniteStateMachine fsm) {
        FsmGraph graph = CreateInstance<FsmGraph>();
        graph.Init(fsm);

        return graph;
    }

    private void Init (FiniteStateMachine fsm) {
        nodes = new List<Node>();
        hideFlags = HideFlags.HideAndDontSave;

        foreach (FiniteStateMachine.StateInstance stateInstance in fsm.states) {
            AddState(stateInstance.state);
        }
    }

    public void AddState (FsmState state) {
        StateNode node = CreateInstance<StateNode>();
        node.position = new Rect(50f, 150f, 100f, 100f * (nodes.Count + 1));
        node.name = state.name;

        fsmNodes.Add(state, node);
        AddNode(node);
    }
}