using System.Windows.Input;
using Raikage.Core.Aspects;
using Raikage.Core.Base;

namespace Raikage.Core.ViewModels
{
    [NotifyPropertyChanged]
    public class FirstViewModel
        : BaseMvxViewModel
    {
        public string Hello { get; set; }

        //This Command Will Navigate To SecondViewModel,
        //Note That The Current Class Must Inherit From BaseMvxViewModel
        [MvxNavigationCommand(typeof(SecondViewModel))]
        public ICommand GoNext { get; set; }
    }
}
