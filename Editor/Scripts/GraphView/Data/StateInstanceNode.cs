namespace Helluys.FsmCore.Editor.Data {
	public class StateInstanceNode {
		public FsmGraphObject graph { get; }
		public FiniteStateMachine.StateInstance stateInstance { get; }

		public StateInstanceNode(FsmGraphObject owner, FiniteStateMachine.StateInstance instance) {
			graph = owner;
			stateInstance = instance;
		}
	}
}