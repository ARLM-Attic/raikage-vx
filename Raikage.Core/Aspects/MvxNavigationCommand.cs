using System;
using Cirrious.MvvmCross.ViewModels;
using PostSharp.Aspects;
using PostSharp.Serialization;
using Raikage.Core.Base;

namespace Raikage.Core.Aspects
{
    [PSerializable]
    public class MvxNavigationCommand : LocationInterceptionAspect
    {
        private Type _targetType;

        public MvxNavigationCommand(Type targetType)
        {
            _targetType = targetType;
        }

        public override void OnGetValue(LocationInterceptionArgs args)
        {
            args.Value = new MvxCommand(() => ((BaseMvxViewModel)args.Instance).StartViewModel(_targetType));
        }
    }
}
