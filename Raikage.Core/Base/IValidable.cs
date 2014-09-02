using System.Collections.Generic;
using FluentValidation.Results;

namespace RaikageFramework.Base
{
    public interface IValidable
    {
        void OnValidationFailed(string sender, IList<ValidationFailure> errors);
    }
}
