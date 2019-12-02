using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using Helluys.FsmCore.Editor.Data;
using UnityEngine;
using System.Collections.Generic;

namespace Helluys.FsmCore.Editor.Views {

	/// <summary>
	/// Visual element for rendering a FiniteStateMachine graph object
	/// </summary>
	public class FsmGraphView : GraphView, IDisposable {

		/// <summary>
		/// Node views by their state instance name
		/// </summary>
		private IDictionary<string, StateInstanceNodeView> nodeViews = new Dictionary<string, StateInstanceNodeView>();

		public FsmGraphView(FsmGraphObject fsmGraphObject) {
			VisualElementExtensions.AddManipulator(this, new ContentDragger());
			VisualElementExtensions.AddManipulator(this, new SelectionDragger());
			style.flexGrow = 1f;

			foreach(StateInstanceNode node in fsmGraphObject.GetNodes()) {
				StateInstanceNodeView nodeView = new StateInstanceNodeView(node);
				nodeView.SetPosition(FindPosition(nodeView));

				nodeViews.Add(node.stateInstance.name, nodeView);
				AddElement(nodeView);
			}

			foreach(TransitionInstanceEdge transitionEdge in fsmGraphObject.edges) {
				TransitionEdgeView transitionView = new TransitionEdgeView(transitionEdge) {
					input = nodeViews[transitionEdge.transitionInstance.originState].inputPort,
					output = nodeViews[transitionEdge.transitionInstance.targetState].outputPort
				};

				AddElement(transitionView);
			}
		}

		private Rect FindPosition(StateInstanceNodeView nodeView) {
			Rect position = nodeView.GetPosition();
			position.x += 150f * (nodes.ToList().Count % 2);
			position.y += 150f * (nodes.ToList().Count / 2);
			return position;
		}

		public void Dispose() {
			// Nothing to do right now
		}
	}
}