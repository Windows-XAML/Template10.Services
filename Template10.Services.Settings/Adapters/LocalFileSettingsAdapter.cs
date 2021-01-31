using System;
using Template10.Services.Compression;
using Template10.Services.File;
using Template10.Services.Serialization;

namespace Template10.Services.Settings
{
    public class LocalFileSettingsAdapter : ISettingsAdapter
    {
        private readonly IFileService _helper;

        public LocalFileSettingsAdapter(ISerializationService serializationService, ICompressionService compressionService)
        {
            _helper = new FileService(serializationService);
            SerializationService = serializationService;
            CompressionService = compressionService;
        }

        public ISerializationService SerializationService { get; }

        public ICompressionService CompressionService { get; }

        public LocalFileSettingsAdapter(IFileService fileService)
        {
            _helper = fileService;
        }

        public (bool successful, string result) ReadString(string key)
        {
            try
            {
                return (true, _helper.ReadStringAsync(key).Result);
            }
            catch (Exception exc)
            {

                return (false, exc.Message);
            }
        }

        public void WriteString(string key, string value)
        {
            if (!_helper.WriteStringAsync(key, value).Result)
            {
                throw new Exception();
            }
        }
    }
}
