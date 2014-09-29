using RaikageFramework.Aspects;
using RaikageFramework.Aspects.Storage;
using RaikageFramework.Base;

namespace Raikage.Test.Core.ViewModels
{
    [NotifyPropertyChanged]
    public class Person
        : BaseMvxViewModel
    {
        [SettingField]
        public string Name { get; set; }
        [SettingField]
        public string Age { get; set; }

        [SettingField(true)]
        public string Position { get; set; }

        public Person()
        {
            this.InitializeSettingsManager();

        }
    }
}