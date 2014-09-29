using System;
using System.CodeDom.Compiler;
using System.Windows.Input;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.ViewModels;
using Raikage.Test.Core.Messeges;
using RaikageFramework.Aspects;
using RaikageFramework.Aspects.Commands;
using RaikageFramework.Base;

namespace Raikage.Test.Core.ViewModels
{
    [NotifyPropertyChanged]
    [AutoCommand]
    public class SettingsViewModel
        : BaseMvxViewModel
    {
        public ICommand RanCommand { get; set; }

        [NotifyPropertyChanged(AttributeExclude = true)]
        public Person Person { get; set; }

        public SettingsViewModel()
        {
            Person = new Person();

        }

        public void Ran(object obj)
        {
            Person.Name = Guid.NewGuid().ToString();
            Person.Age = Guid.NewGuid().ToString();
            Person.Position = Guid.NewGuid().ToString();
        }
    }
}