using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace TileLands
{
    /// <summary>
    /// class not used in current version
    /// saved for later functionality
    /// </summary>
    class DataManager
    {
        /// <summary>
        /// Create filestream and binaryformatter(obsolete)
        /// create savefile and serialize data as binary 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="filePath"></param>
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

        /// <summary>
        /// Load binary data and deserialize it into data        
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
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
