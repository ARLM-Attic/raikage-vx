using System;
using Cirrious.MvvmCross.ViewModels;

namespace RaikageFramework.Base
{
    public class BaseMvxViewModel : MvxViewModel
    {
        public bool StartViewModel(Type viewModel, IMvxBundle parameterBundle = null, MvxBundle presentationBundle = null, MvxRequestedBy requestedBy = null)
        {
            return this.ShowViewModel(viewModel, parameterBundle, (IMvxBundle)presentationBundle, requestedBy);
        }
    }
}
