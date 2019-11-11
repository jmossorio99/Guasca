using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MIOSimulation
{
    class DataProcessor
    {
        public void reading(string url)
        {
            StreamReader datagram = new StreamReader(url);
            StreamWriter newData = new StreamWriter("dataSimulation1.txt");
            newData.Flush();
            string lines = datagram.ReadLine();
            lines = datagram.ReadLine();
            while (lines != null)
            {
                string[] text = lines.Split(',');
                if ((!text[4].Equals("-1") || !text[5].Equals("-1")))
                {
                    if ((Double.Parse(text[4]) / 10000000) < -76 && (Double.Parse(text[4]) / 10000000) > -77)
                    {
                        if ((Double.Parse(text[5]) / 10000000) < 4 && (Double.Parse(text[5]) / 10000000) > 3)
                        {
                            string a = text[0] + "," + text[1] + "," + text[2] + "," + text[3] + "," + text[4] + "," +
                                text[5] + "," + text[6] + "," + text[7] + "," + text[8];
                            newData.WriteLine(a);
                        }
                    }
                }
                lines = datagram.ReadLine();
            }

            newData.Close();
            datagram.Close();
        }
    }
}
