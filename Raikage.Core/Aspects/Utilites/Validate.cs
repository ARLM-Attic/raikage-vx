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
        private IValidator _validator;

        public Validate(Type validator)
        {
            _validator = (IValidator)validator.CreateDefault();
        }

        public override void OnEntry(MethodExecutionArgs args)
        {
            var validationResult = _validator.Validate(args.Instance);
            if (validationResult.IsValid) return;

            ((IValidable)args.Instance).OnValidationFailed(args.Method.Name, validationResult.Errors);
            args.FlowBehavior = FlowBehavior.Return;
        }
    }
}
