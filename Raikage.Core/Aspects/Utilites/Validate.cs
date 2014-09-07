using System;
using Cirrious.CrossCore.IoC;
using FluentValidation;
using PostSharp.Aspects;
using PostSharp.Serialization;
using RaikageFramework.Base;

namespace RaikageFramework.Aspects.Utilites
{
    [PSerializable]
    public class Validate : OnMethodBoundaryAspect
    {
        private Type _validator;
        public Validate(Type validator)
        {
            _validator = validator;
        }

        public override void OnEntry(MethodExecutionArgs args)
        {
            var validator = (IValidator)Activator.CreateInstance(_validator);
            var validationResult = validator.Validate(args.Instance);
            if (validationResult.IsValid) return;

            ((IValidable)args.Instance).OnValidationFailed(args.Method.Name, validationResult.Errors);
            args.FlowBehavior = FlowBehavior.Return;
        }
    }
}
