using Helluys.FsmCore;

namespace Helluys.FsmCore.Tests
{
    public class TestFsmState : FsmState
    {
        public int onEnterCount, onStayCount, onExitCount;

        public override void OnEnter () {
            onEnterCount++;
        }

        public override void OnStay () {
            onStayCount++;
        }

        public override void OnExit () {
            onExitCount++;
        }
    }
}
