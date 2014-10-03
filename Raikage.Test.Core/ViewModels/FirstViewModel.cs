using System;
using System.Collections.Generic;
using System.Windows.Input;
using FluentValidation.Results;
using Raikage.Test.Core.Messeges;
using Raikage.Test.Core.Services;
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

        private readonly ISendMessageService _iSendMessageService;
        public string Hello { get; set; }

        public int TestCount = 0;

        public FirstViewModel(ISendMessageService iSendMessageService)
        {
            _iSendMessageService = iSendMessageService;
            Hello = "hello";
            this.InitializeMessenger(this);
        }

        //This Command Will Navigate To SecondViewModel,
        //Note That The Current Class Must Inherit From BaseMvxViewModel
        [MvxNavigationCommand(typeof(SettingsViewModel), typeof(FirstViewModelValidator))]
        [AutoCommand(AttributeExclude = true)]
        public ICommand GoNext { get; set; }

        public ICommand LoginCommand { get; set; }

        public ICommand TestCommand { get; set; }

        public virtual void Test(object param)
        {
            _iSendMessageService.SendHello();
        }
        [AutoCommand("Command1Method", AttributeReplace = true)]
        public ICommand Command1 { get; set; }

        public virtual void Command1Method(object p)
        {
            _iSendMessageService.SendWorld();
        }
        //If Validation Failed Method Will Not Be Called and On Validation Failed Will be Invoked
        //Note That The Current Class Must Implement From IValidable
        [Validate(typeof(FirstViewModelValidator))]
        public void Login()
        {

        }
        [MessageListener]
        public void ReceiveMessage(TestMessage testMessage)
        {
            var x = testMessage;
        }
        [MessageListener]
        public void ReceiveMessage2(TestMessage2 testMessage)
        {
            var x = testMessage;
        }
        public void OnValidationFailed(string sender, IList<ValidationFailure> errors)
        {
            var x = errors;
            //Handle Vaildation Failure
        }
    }
}
