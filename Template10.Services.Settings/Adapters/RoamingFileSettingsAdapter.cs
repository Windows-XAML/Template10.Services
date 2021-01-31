using System;
using Template10.Services.File;
using Template10.Services.Serialization;
using Template10.Services.Compression;

namespace Template10.Services.Settings
{
    public class RoamingFileSettingsAdapter : ISettingsAdapter
    {
        private readonly IFileService _helper;

        public RoamingFileSettingsAdapter(ISerializationService serializationService, ICompressionService compressionService)
        {
            _helper = new FileService(serializationService);
            SerializationService = serializationService;
            CompressionService = compressionService;
        }

        public ISerializationService SerializationService { get; }

        public ICompressionService CompressionService { get; }

        public RoamingFileSettingsAdapter(IFileService fileService)
        {
            _helper = fileService;
        }

        public (bool successful, string result) ReadString(string key)
        {
            return (true, _helper.ReadStringAsync(key, StorageStrategies.Roaming).Result);
        }

        public void WriteString(string key, string value)
        {
            if (!_helper.WriteStringAsync(key, value, StorageStrategies.Roaming).Result)
            {
                throw new Exception();
            }
        }
    }
}
