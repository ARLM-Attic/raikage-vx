using System;
using System.Collections.Generic;
using Cirrious.MvvmCross.ViewModels;
using PCLStorage;

namespace RaikageFramework.Base
{
    public class BaseMvxViewModel : MvxViewModel
    {
        public bool StartViewModel(Type viewModel, IMvxBundle parameterBundle = null, MvxBundle presentationBundle = null, MvxRequestedBy requestedBy = null)
        {
            return this.ShowViewModel(viewModel, parameterBundle, (IMvxBundle)presentationBundle, requestedBy);
        }
        public virtual Dictionary<string, object> _settings { get; set; }

        public IFile _file { get; set; }

        /// <summary>
        /// must be called in the constructor to initialize the messages listeners
        /// </summary>
        /// <param name="instance">the current instance</param>
        public virtual void InitializeMessenger(object instance)
        {
            var x = instance;
            //to be called in constractor will be replaced with aspect code
        }
        protected virtual async void InitializeSettingsManager()
        {
            throw new NotImplementedException();
        }
    }
}
