using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cirrious.MvvmCross.ViewModels;
using PostSharp.Aspects;
using PostSharp.Serialization;

namespace KitApp.Core.Aspects
{

    [PSerializable]
    class NotifyPropertyChanged : LocationInterceptionAspect
    {
        public override void OnSetValue(LocationInterceptionArgs args)
        {
            args.SetNewValue(args.Value);

            ((MvxViewModel)args.Instance).RaisePropertyChanged(args.LocationName);
        }
    }
}

