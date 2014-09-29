using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Threading.Tasks;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.ViewModels;
using Newtonsoft.Json;
using PCLStorage;
using PostSharp.Aspects;
using PostSharp.Aspects.Advices;
using PostSharp.Reflection;
using PostSharp.Serialization;

namespace RaikageFramework.Aspects.Storage
{
    [PSerializable]
    public class SettingField : LocationInterceptionAspect, IInstanceScopedAspect
    {
        private string _id = "settingField";

        private bool _SaveOnSet;

        public SettingField(bool saveOnSet = false)
        {
            _SaveOnSet = saveOnSet;
        }

        [IntroduceMember(IsVirtual = true, OverrideAction = MemberOverrideAction.OverrideOrIgnore, Visibility = Visibility.Public)]
        public IFile _file { get; set; }

        [IntroduceMember(IsVirtual = true, OverrideAction = MemberOverrideAction.OverrideOrIgnore, Visibility = Visibility.Public)]
        public Dictionary<string, object> _settings { get; set; }

        [IntroduceMember(IsVirtual = true, OverrideAction = MemberOverrideAction.OverrideOrIgnore, Visibility = Visibility.Family)]
        public async void InitializeSettingsManager()
        {
            if (StorageHelper._files == null)
                StorageHelper._files = new Dictionary<string, IFile>();

            await LoadSettingsManager();

            foreach (var entry in IntermediateStorage.GetEntries(_id).Where(ee => ee is Action))
            {
                ((Action)entry).Invoke();
            }
        }
        private static Dictionary<string, object> GetSettings(LocationInterceptionArgs args)
        {
            return (Dictionary<string, object>)
                args.Instance.GetType().GetRuntimeProperty("_settings").GetValue(args.Instance);
        }
        private static IFile GetFile(LocationInterceptionArgs args)
        {
            return StorageHelper._files["settings"];
        }
        public override void OnSetValue(LocationInterceptionArgs args)
        {
            SetValue(args);

            base.OnSetValue(args);
        }


        private void SetValue(LocationInterceptionArgs args)
        {
            if (_settings == null)
                _settings = GetSettings(args);

            if (_file == null)
                _file = GetFile(args);

            if (_settings.ContainsKey(args.LocationFullName))
                _settings[args.LocationFullName] = args.Value;
            else
            {
                _settings.Add(args.LocationFullName, args.Value);
            }
            var settings = JsonConvert.SerializeObject(_settings);
            if (_SaveOnSet)
                StorageHelper.Save(_file, settings);

        }


        public override void OnGetValue(LocationInterceptionArgs args)
        {
            if (_settings == null)
                _settings = GetSettings(args);

            if (_settings == null)
            {
                IntermediateStorage.AddEntry(_id, args.LocationFullName, new Action(() =>
                {
                    GetValue(args);
                    typeof(MvxViewModel)
                        .GetRuntimeMethod("RaisePropertyChanged", new[] { typeof(string) })
                        .Invoke(args.Instance, new object[] { args.LocationName });
                }));
            }
            else
            {

                GetValue(args);
            }

        }

        private void GetValue(LocationInterceptionArgs args)
        {
            if (_settings == null)
                _settings = GetSettings(args);

            if (_settings.ContainsKey(args.LocationFullName))
            {
                var value = (dynamic)_settings[args.LocationFullName];

                args.Value = value;
            }
            else
                args.ProceedGetValue();
        }

        public object CreateInstance(AdviceArgs adviceArgs)
        {
            return this.MemberwiseClone();
        }

        public async void RuntimeInitializeInstance()
        {

        }

        private async Task LoadSettingsManager()
        {
            _file = await StorageHelper.LoadFile("settings");

            StorageHelper._files.Add("settings", _file);
            var result = await StorageHelper.ReadFile(_file);
            _settings = JsonConvert.DeserializeObject<Dictionary<string, object>>(result);
        }
    }
}