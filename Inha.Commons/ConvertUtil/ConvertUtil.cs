using System.IO;
using System.Text;

namespace Inha.Commons.ConvertUtil
{
    public class ConvertUtil
    {
        public static MemoryStream ConvertStringToStream(string input)
        {
            byte[] byteArray = Encoding.ASCII.GetBytes(input);
            MemoryStream stream = new MemoryStream(byteArray);
            return stream;
        }

        public static string ConvertStreamToString(Stream stream)
        {
            StreamReader reader = new StreamReader(stream);
            string output = reader.ReadToEnd();
            return output;
        }
    }
}
