using PostSharp.Aspects;
using PostSharp.Serialization;
using RaikageFramework.Base;
using RaikageFramework.Base.Managers;

namespace RaikageFramework.Aspects.Authentication
{
    [PSerializable]
    public class Authentication : OnMethodBoundaryAspect
    {
        public override void OnEntry(MethodExecutionArgs args)
        {
            if (!SecurityManager.IsAuthenticated)
                ((BaseMvxViewModel)args.Instance).StartViewModel(SecurityManager.LoginType, null);
            args.FlowBehavior = FlowBehavior.Return;
        }
    }
}