using System;
using System.Reflection;
using Cirrious.MvvmCross.ViewModels;
using PostSharp.Aspects;
using PostSharp.Serialization;
using RaikageFramework.Base;

namespace RaikageFramework.Aspects.Commands
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
