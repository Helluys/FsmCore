using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Helluys.FsmCore.Editor.Data {

	/// <summary>
	/// Hold a FiniteStateMachine graph.<br />
	/// In addition to the graph itself, this object contains the position of each element.
	/// </summary>
	public class FsmGraphObject : ScriptableObject {
		private FiniteStateMachine finiteStateMachine;

		internal void Initialize(FiniteStateMachine fsm) {
			finiteStateMachine = fsm;

			foreach(FiniteStateMachine.StateInstance stateInstance in finiteStateMachine.stateInstances) {
				AddNode(new StateInstanceNode(this, stateInstance));

				foreach(FiniteStateMachine.TransitionInstance transitionInstance in stateInstance.transitions) {
					AddEdge(new TransitionInstanceEdge(this, transitionInstance));
				}
			}
		}

		[SerializeField]
		private List<StateInstanceNode> m_Nodes = new List<StateInstanceNode>();

		public void AddNode(StateInstanceNode stateNode) {
			m_Nodes.Add(stateNode);
		}

		public StateInstanceNode FindNode(string name) {
			return m_Nodes.Find(node => node.stateInstance.name.Equals(name));
		}

		public IEnumerable<StateInstanceNode> GetNodes() {
			return m_Nodes.Where(x => x != null);
		}

		[SerializeField]
		List<TransitionInstanceEdge> m_Edges = new List<TransitionInstanceEdge>();

		private void AddEdge(TransitionInstanceEdge transitionEdge) {
			m_Edges.Add(transitionEdge);
		}

		public IEnumerable<TransitionInstanceEdge> edges {
			get { return m_Edges; }
		}
	}
}