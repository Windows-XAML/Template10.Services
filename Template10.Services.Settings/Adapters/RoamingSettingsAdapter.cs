using System;
using Template10.Services.Serialization;
using Windows.Storage;
using Template10.Services.Compression;

namespace Template10.Services.Settings
{
    public class RoamingSettingsAdapter : ISettingsAdapter
    {
        private ApplicationDataContainer _container;

        public RoamingSettingsAdapter(ISerializationService serializationService, ICompressionService compressionService)
        {
            _container = ApplicationData.Current.RoamingSettings;
            SerializationService = serializationService;
            CompressionService = compressionService;
        }

        public ISerializationService SerializationService { get; }

        public ICompressionService CompressionService { get; }

        public (bool successful, string result) ReadString(string key)
        {
            if (_container.Values.ContainsKey(key))
            {
                return (true, _container.Values[key].ToString());
            }
            else
            {
                return (false, string.Empty);
            }
        }

        public void WriteString(string key, string value)
            => _container.Values[key] = value;
    }
}
