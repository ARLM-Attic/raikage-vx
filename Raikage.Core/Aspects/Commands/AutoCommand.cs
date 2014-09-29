using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Cirrious.MvvmCross.Platform;
using Cirrious.MvvmCross.ViewModels;
using PostSharp.Aspects;
using PostSharp.Reflection;
using PostSharp.Serialization;
using RaikageFramework.Base;

namespace RaikageFramework.Aspects.Commands
{
    [PSerializable]
    public class AutoCommand : LocationInterceptionAspect
    {
        private string _method;


        public AutoCommand()
        {
            _method = null;

        }
        public AutoCommand(string method)
        {
            _method = method;
        }

        public override void OnGetValue(LocationInterceptionArgs args)
        {

            if (string.IsNullOrEmpty(_method))
            {
                _method = args.LocationName.Replace("Command", string.Empty);
            }

            if (args.Value == null)
            {
                args.Value = new MvxCommand<object>((param) => args.Instance.GetType()
                    .GetRuntimeMethod(_method, new Type[] { typeof(object) })
                    .Invoke(args.Instance, new object[] { param }));
            }
            else
                args.ProceedGetValue();
        }



        public override bool CompileTimeValidate(LocationInfo locationInfo)
        {
            if (locationInfo.LocationType != typeof(ICommand))
                return false;
            return base.CompileTimeValidate(locationInfo);
        }
    }
}
