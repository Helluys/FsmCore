using Helluys.FsmCore.Editor.Data;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace Helluys.FsmCore.Editor.Views {

	public sealed class StateInstanceNodeView : Node {
		StateInstanceNode stateInstanceNode;

		public Port inputPort { get; private set; }
		public Port outputPort { get; private set; }

		public StateInstanceNodeView(StateInstanceNode node) {
			stateInstanceNode = node;
			layer = 1;
			
			inputPort = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(Port));
			inputContainer.Add(inputPort);

			outputPort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(Port));
			outputContainer.Add(outputPort);
		}
	}
}