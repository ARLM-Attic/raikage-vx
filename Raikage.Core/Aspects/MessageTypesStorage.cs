using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RaikageFramework.Aspects
{
    public static class MessageTypesStorage
    {
        private static IList<Type> _types;

        public static IList<Type> Types
        {
            get { return _types ?? (_types = new List<Type>()); }
        }


        private static IList<MethodBase> _methods;

        public static IList<MethodBase> Methods
        {
            get { return _methods ?? (_methods = new List<MethodBase>()); }
        }

        public static void Dispose()
        {
            _methods = null;
            _types = null;
            Methods.Clear();
            Types.Clear();
        }
    }

    public static class IntermediateStorage
    {
        private static IDictionary<string, object> _dataStore = new Dictionary<string, object>();

        public static void GetEntry(string aspectId, object data)
        {
            if (_dataStore == null)
                _dataStore = new Dictionary<string, object>();

            _dataStore.Add(aspectId, data);
        }
        public static void AddEntry(string aspectId, string taskId, object data)
        {
            if (_dataStore == null)
                _dataStore = new Dictionary<string, object>();
            if (!_dataStore.ContainsKey(aspectId))
                _dataStore[aspectId] = new Dictionary<string, object> { { taskId, data } };
            else
            {
                ((Dictionary<string, object>)_dataStore[aspectId]).Add(taskId, data);
            }

        }
        public static object GetEntry(string aspectId, string taskId)
        {
            if (_dataStore == null)
                _dataStore = new Dictionary<string, object>();

            if (!_dataStore.ContainsKey(aspectId))
                return null;
            var internalStore = ((IDictionary<string, object>)_dataStore[aspectId]);

            return !internalStore.ContainsKey(taskId) ? null : internalStore[taskId];
        }
        public static IEnumerable<object> GetEntries(string aspectId)
        {
            if (_dataStore == null)
                _dataStore = new Dictionary<string, object>();

            if (!_dataStore.ContainsKey(aspectId))
                return new List<object>();
            return ((IDictionary<string, object>)_dataStore[aspectId]).Select(ee => ee.Value);

        }
        public static void RemoveEntry(string aspectId)
        {
            if (_dataStore == null)
                _dataStore = new Dictionary<string, object>();

            _dataStore.Remove(aspectId);
        }

    }
}