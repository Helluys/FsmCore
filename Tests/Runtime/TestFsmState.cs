using UnityEngine;

namespace Helluys.FsmCore.Tests {
	public class TestFsmState : FsmState {
		public int onEnterCount, onStayCount, onExitCount;

		public override void OnEnter() {
			Debug.Log("OnEnter " + name);
			onEnterCount++;
		}

		public override void OnStay() {
			Debug.Log("OnStay " + name);
			onStayCount++;
		}

		public override void OnExit() {
			Debug.Log("OnExit " + name);
			onExitCount++;
		}
	}
}
