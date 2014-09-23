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
        /// <summary>
        /// must be called in the constructor to initialize the messages listeners
        /// </summary>
        /// <param name="instance">the current instance</param>
        public virtual void InitializeMessenger(object instance)
        {
            var x = instance;
            //to be called in constractor will be replaced with aspect code
        }
    }
}
