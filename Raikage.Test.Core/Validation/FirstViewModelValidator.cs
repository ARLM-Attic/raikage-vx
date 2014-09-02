using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Raikage.Test.Core.ViewModels;

namespace Raikage.Test.Core.Validation
{
    class FirstViewModelValidator : AbstractValidator<FirstViewModel>
    {
        public FirstViewModelValidator()
        {
            RuleFor(firstViewModel => firstViewModel.Hello).NotEmpty();
        }
    }
}
