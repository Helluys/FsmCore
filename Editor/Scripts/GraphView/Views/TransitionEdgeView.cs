using Helluys.FsmCore.Editor.Data;
using UnityEditor.Experimental.GraphView;

namespace Helluys.FsmCore.Editor.Views {
	public class TransitionEdgeView : Edge {
		private TransitionInstanceEdge transitionInstance;

		public TransitionEdgeView() {
			layer = 0;
		}
	
		public TransitionEdgeView(TransitionInstanceEdge transition) : this() {
			transitionInstance = transition;
		}
	}
}