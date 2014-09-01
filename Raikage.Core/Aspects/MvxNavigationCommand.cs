using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cirrious.MvvmCross.ViewModels;
using PostSharp.Aspects;
using PostSharp.Serialization;
using Raikage.Core.Base;

namespace KitApp.Core.Aspects
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
