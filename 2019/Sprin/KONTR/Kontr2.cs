using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Kontr2
{
    class Program
    {
        static void Main(string[] args)
        {
            var jsonFile = new FileInfo("Comments.txt");
            var text = jsonFile.OpenText().ReadToEnd();
            //пропустить первый симво '['
            text.Skip(1);
            var comments = new List<object>();
            var builder = new StringBuilder();
            foreach(var e in text)
            {
                builder.Append(e);
                if (e == '}')
                {
                    if (builder[0] == ',') builder.Remove(0, 1);
                    comments.Add(builder.ToString());
                    builder.Clear();
                }
            }
            var result = comments.Select(e => Solve((string)e));
        }

        static int Solve(string str)
        {
            var comment = JsonConvert.DeserializeObject(str);
            var a = comment.GetType();
            var body = a.GetField("body");
            var resultStr = (string)body.GetValue(body);
            return resultStr.Length;
        }
    }
}
