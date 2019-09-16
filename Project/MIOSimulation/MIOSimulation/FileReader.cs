using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIOSimulation
{
    class FileReader
    {

        private String filePath;

        public FileReader(String filePath) { this.filePath = filePath; }

        public List<String> readFile()
        {
            var reader = new StreamReader(File.OpenRead(filePath));
            List<String> output = new List<string>();
            reader.ReadLine();
            while (!reader.EndOfStream)
            {
                output.Add(reader.ReadLine());
            }
            return output;
        }

    }
}
