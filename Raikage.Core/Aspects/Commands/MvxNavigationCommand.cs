using System;
using System.Reflection;
using System.Windows.Input;
using Cirrious.MvvmCross.ViewModels;
using FluentValidation;
using PostSharp.Aspects;
using PostSharp.Reflection;
using PostSharp.Serialization;
using RaikageFramework.Base;

namespace RaikageFramework.Aspects.Commands
{
    [PSerializable]
    public class MvxNavigationCommand : LocationInterceptionAspect
    {
        private Type _targetType;

        private Type _validatorType;
        public MvxNavigationCommand(Type targetType, Type validatorType = null)
        {
            _targetType = targetType;
            _validatorType = validatorType;
        }

        private void Navigate(BaseMvxViewModel instance, string name)
        {
            if (_validatorType != null)
            {
                var validator = (IValidator)Activator.CreateInstance(_validatorType);
                var validationResult = validator.Validate(instance);
                if (!validationResult.IsValid)
                {
                    ((IValidable)instance).OnValidationFailed(name, validationResult.Errors);
                    return;
                }
            }
            instance.StartViewModel(_targetType);
        }
        public override void OnGetValue(LocationInterceptionArgs args)
        {
            args.Value = new MvxCommand(() => Navigate((BaseMvxViewModel)args.Instance, args.LocationName));
        }

        public override bool CompileTimeValidate(LocationInfo locationInfo)
        {
            return locationInfo.LocationType == typeof(ICommand) && base.CompileTimeValidate(locationInfo);
        }
    }
}
