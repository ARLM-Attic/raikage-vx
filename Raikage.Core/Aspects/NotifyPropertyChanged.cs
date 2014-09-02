﻿using Cirrious.MvvmCross.ViewModels;
using PostSharp.Aspects;
using PostSharp.Serialization;

namespace Raikage.Core.Aspects
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

