using System.IO;
using System.Threading;
using System.Reflection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Kontr
{
    class Program
    {
        static void Method(FileInfo[] file)
        {
            foreach(var e in file)
            {
                //Do Something
            }
        }

        static void Main(string[] args)
        {
            var dir = new DirectoryInfo("C:\\Users\\Администратор");
            var subDirs = dir.GetDirectories();
            foreach(var e in subDirs)
            {
                var files = e.GetFiles();
                var thread = new Thread(() => Method(files));
            }
            //Второй способ
            //Parallel.ForEach(subDirs, (e) => { Method(e.GetFiles()); });
        }

        public void Solve()
        {
            var dir = new DirectoryInfo(@"C: \Users\Администратор");
            var files = dir.GetFiles();
            var correctAnsNum = 0;
            Parallel.ForEach(files, (e) => { if (CheckTasks(e)) correctAnsNum++; });
            //foreach (var e in files)
            //{
            //    var thread = new Thread(() => CheckTasks(e));
            //}
        }

        private static bool CheckTasks(FileInfo e)
        {
            // Порядок в файле: название класса; название метода; результат; остальное
            var str = e.OpenText().ReadToEnd();
            var data = str.Split(';');
            object[] @params = data.Skip(3).Select(a => Type.GetType(a)).ToArray();
            var @class = Type.GetType(data[0], false);
            var method = @class.GetMethod(data[1]);
            var result = method.Invoke(null, @params);
            // data[2] - результат студента
            return result == (object)Type.GetType(data[2]);
        }
    }
}
