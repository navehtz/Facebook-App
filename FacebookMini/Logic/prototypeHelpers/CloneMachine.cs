using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace FacebookMini.logic.prototypeHelpers
{
    public static class CloneMachine
    {
        public static T DeepClone<T>(this T i_ToClone)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, i_ToClone);
                stream.Seek(0, SeekOrigin.Begin);
                T theClone = (T)formatter.Deserialize(stream);
                stream.Close();
                return theClone;
            }
        }
    }
}
