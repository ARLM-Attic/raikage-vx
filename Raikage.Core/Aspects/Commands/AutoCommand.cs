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
using PostSharp.Serialization;
using RaikageFramework.Base;

namespace RaikageFramework.Aspects.Commands
{
    [PSerializable]
    public class AutoCommand : LocationInterceptionAspect
    {
        private string _method;
        private string[] _args;
        private Type[] _parametersTypes;
        private object[] _parameters;
        public AutoCommand()
        {
            _method = null;
            _args = new string[0];
            _parametersTypes = new Type[0];
        }
        public AutoCommand(string method, string[] args)
        {
            _method = method;
            _args = args;
            if (args.Length > 0)
            {
                _parametersTypes = args.Select(arg => arg.GetType()).ToArray();
            }
        }

        public override void OnGetValue(LocationInterceptionArgs args)
        {
            if (args.Location.LocationType == typeof(ICommand))
            {
                if (string.IsNullOrEmpty(_method))
                {
                    _method = args.LocationName.Replace("Command", string.Empty);

                }
                GetParameters(args.Instance);
                var command = args.Location.LocationType;

                args.Value = new MvxCommand<object>((param) => args.Instance.GetType()
                    .GetRuntimeMethod(_method, new Type[] { typeof(object) })
                    .Invoke(args.Instance, new object[] { param }));
            }
            else
            {
                args.ProceedGetValue();
            }
        }


        private void GetParameters(object instance)
        {
            if (_args == null)
                _parameters = new object[0];
            _parameters =
                _args.Select(param => (instance.GetType().GetRuntimeField(param)).GetValue(instance)).ToArray();

        }
    }
}
