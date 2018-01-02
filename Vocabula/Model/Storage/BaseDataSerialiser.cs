using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Vocabula.Model.Storage.DataFormats;

namespace Vocabula.Model.Storage
{
    public abstract class BaseDataSerialiser<TDataStoreType> where TDataStoreType : BaseDataStorageFileFormat
    {
        protected BaseDataSerialiser(string dataStorePath)
        {
            _dataStorePath = dataStorePath;
            _fileData = NewFileDataObject();
        }

        private string _dataStorePath;
        protected TDataStoreType _fileData;

        public void Write()
        {
            using (FileStream stream = new FileStream(_dataStorePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                XmlSerializer serialiser = new XmlSerializer(typeof(TDataStoreType));
                serialiser.Serialize(stream, _fileData);
            }
        }

        public void Read()
        {
            if (!File.Exists(_dataStorePath))
                return;

            using (FileStream stream = new FileStream(_dataStorePath, FileMode.OpenOrCreate, FileAccess.Read))
            {
                XmlSerializer serialiser = new XmlSerializer(typeof(TDataStoreType));
                _fileData = (TDataStoreType)serialiser.Deserialize(stream);
            }
        }

        public void ResetInMemoryFileData()
        {
            _fileData = NewFileDataObject();
        }

        protected abstract TDataStoreType NewFileDataObject();
    }
}
