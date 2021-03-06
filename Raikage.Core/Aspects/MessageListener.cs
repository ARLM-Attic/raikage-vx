﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Messenger;
using PostSharp.Aspects;
using PostSharp.Aspects.Advices;
using PostSharp.Reflection;
using PostSharp.Serialization;

namespace RaikageFramework.Aspects
{
    [PSerializable]
    public class MessageListener : OnMethodBoundaryAspect
    {
        [IntroduceMember(IsVirtual = false, OverrideAction = MemberOverrideAction.Ignore, Visibility = Visibility.Private)]
        public IMvxMessenger _messenger
        {
            get
            {
                return Mvx.Resolve<IMvxMessenger>();
            }
        }
        [IntroduceMember(IsVirtual = false, OverrideAction = MemberOverrideAction.OverrideOrIgnore, Visibility = Visibility.Private)]
        public IList<MvxSubscriptionToken> MvxSubscriptionTokens
        {
            get;
            set;
        }

        private IList<Type> _types = MessageTypesStorage.Types;
        private IList<Type> _instancetypes = MessageTypesStorage.InstanceTypes;
        private IList<MethodBase> _methods = MessageTypesStorage.Methods;

        [IntroduceMember(OverrideAction = MemberOverrideAction.OverrideOrIgnore, IsVirtual = true)]
        public void InitializeMessenger(object instance)
        {
            for (var index = 0; index < _types.Count; index++)
            {
                if (instance.GetType() == _instancetypes[index])
                {
                    var method = typeof(IMvxMessenger).GetMethod("Subscribe",
                        BindingFlags.Public | BindingFlags.Instance);
                    var type = _types[index];
                    var _method = _methods[index];
                    // Build a method with the specific type argument you're interested in
                    method = method.MakeGenericMethod(type);


                    // The "null" is because it's optional parameters
                    var token = method.Invoke(_messenger,
                        new object[] { new Action<object>((param) => _method.Invoke(instance, new[] { param })), null, null });

                    MvxSubscriptionTokens.Add((MvxSubscriptionToken)token);
                }

            }
            //MessageTypesStorage.Dispose();

        }

        public override bool CompileTimeValidate(MethodBase method)
        {
            if (!(typeof(MvxMessage).IsAssignableFrom(method.GetParameters().First().ParameterType)))
                return false;
            return base.CompileTimeValidate(method);
        }

        public override void CompileTimeInitialize(MethodBase method, AspectInfo aspectInfo)
        {
            _instancetypes.Add(method.DeclaringType);
            MvxSubscriptionTokens = new List<MvxSubscriptionToken>();
            _methods.Add(method);
            _types.Add(method.GetParameters().First().ParameterType);
            base.CompileTimeInitialize(method, aspectInfo);
        }


    }
}
