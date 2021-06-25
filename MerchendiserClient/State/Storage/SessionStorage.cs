using MerchendiserClient.Models;
using System.Collections.Generic;

namespace MerchendiserClient.State.Storage
{
    public sealed class SessionStorage : ObservableObject
    {
        private Dictionary<string, object> values = new Dictionary<string, object>();

        private static readonly SessionStorage storage = new SessionStorage();
        public static SessionStorage GetStorage => storage;

        private SessionStorage()
        {

        }

        public object this[string key]
        {
            get
            {
                if (values.TryGetValue(key, out var value))
                {
                    return value;
                }
                else
                {
                    return null;
                }
            }

            set
            {
                if (values.ContainsKey(key))
                {
                    values[key] = value;
                }
                else
                {
                    values.Add(key, value);
                }

                OnPropertyChanged();
            }
        }
    }
}
