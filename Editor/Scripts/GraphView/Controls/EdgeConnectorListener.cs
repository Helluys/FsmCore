using Helluys.FsmCore.Editor.Data;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Helluys.FsmCore.Editor.Controls {
	public class EdgeConnectorListener : IEdgeConnectorListener {
		private FsmGraphObject graph;

		public EdgeConnectorListener(FsmGraphObject graph) {
			this.graph = graph;
		}

		public void OnDrop(GraphView graphView, Edge edge) {

		}

		public void OnDropOutsidePort(Edge edge, Vector2 position) {

		}
	}
}