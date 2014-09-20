using System;
using System.Collections.Generic;
using System.Windows.Input;
using FluentValidation.Results;
using Raikage.Test.Core.Validation;
using RaikageFramework.Aspects;
using RaikageFramework.Aspects.Commands;
using RaikageFramework.Aspects.Utilites;
using RaikageFramework.Base;

namespace Raikage.Test.Core.ViewModels
{
    [NotifyPropertyChanged]
    [AutoCommand]
    public class FirstViewModel
        : BaseMvxViewModel, IValidable
    {

        public string Hello { get; set; }

        public int TestCount = 0;

        //This Command Will Navigate To SecondViewModel,
        //Note That The Current Class Must Inherit From BaseMvxViewModel
        [MvxNavigationCommand(typeof(SecondViewModel))]
        public ICommand GoNext { get; set; }

        public ICommand LoginCommand { get; set; }

        public ICommand TestCommand { get; set; }

        public virtual void Test(object param)
        {
            TestCount = (int)param;
        }
        [AutoCommand("Command1Method", AttributeReplace = true)]
        public ICommand Command1 { get; set; }

        public virtual void Command1Method(object p)
        {
            var x = p;
        }
        //If Validation Failed Method Will Not Be Called and On Validation Failed Will be Invoked
        //Note That The Current Class Must Implement From IValidable
        [Validate(typeof(FirstViewModelValidator))]
        public void Login()
        {

        }
        public void OnValidationFailed(string sender, IList<ValidationFailure> errors)
        {
            //Handle Vaildation Failure
        }
    }
}
