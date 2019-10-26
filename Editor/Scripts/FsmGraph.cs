using System.Collections.Generic;
using UnityEditor.Graphs;
using UnityEngine;

namespace Helluys.FsmCore.Editor
{
    internal class FsmGraph : Graph
    {
        private IDictionary<FsmState, Node> FsmNodes = new Dictionary<FsmState, Node>();

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

            FsmNodes.Add(state, node);
            AddNode(node);
        }
    }
}
