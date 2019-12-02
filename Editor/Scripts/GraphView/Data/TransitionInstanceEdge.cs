namespace Helluys.FsmCore.Editor.Data {
	public class TransitionInstanceEdge {
		public FsmGraphObject graph { get; }
		public FiniteStateMachine.TransitionInstance transitionInstance { get; }
		
		public TransitionInstanceEdge(FsmGraphObject owner, FiniteStateMachine.TransitionInstance transition) {
			graph = owner;
			transitionInstance = transition;
		}
	}
}