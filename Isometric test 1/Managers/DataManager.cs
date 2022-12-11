using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace TileLands
{
    class DataManager
    {
        public void BinarySerialize(object data, string filePath)
        {
            FileStream _filestream;
            BinaryFormatter _bf = new BinaryFormatter();

            if(File.Exists(filePath))
                File.Delete(filePath);

            _filestream = File.Create(filePath);
            _bf.Serialize(_filestream, data);
            _filestream.Close();
        }

        public object BinaryDeserialize(string filepath)
        {
            object _obj = null;

            FileStream _filestream;
            BinaryFormatter _bf = new BinaryFormatter();

            if(File.Exists(filepath))
            {
                _filestream = File.OpenRead(filepath);
                _obj = _bf.Deserialize(_filestream);
                _filestream.Close();
            }
            return _obj;
        }
    }
}
