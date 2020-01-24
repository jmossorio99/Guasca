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

        private List<stringDate> vector;
        public void reading(string url)
        {
            sort(url);
            StreamWriter newData = new StreamWriter("dataSimulation1.txt");
            newData.Flush();
            for (int i = 0; i < vector.Count; i++)
            {
                string[] text = vector[i].Data().Split(',');
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
            }

            newData.Close();
        }

        private void sort(string url)
        {
            StreamReader datagram = new StreamReader(url);
            string lines = datagram.ReadLine();
            lines = datagram.ReadLine();
            vector = new List<stringDate>();
            while (lines != null)
            {
                //2019-06-20 11:16:47  yyyy-MM-dd hh:mm:ss
                string a = lines.Split(',')[0];
                DateTime date = DateTime.Parse(a);
                vector.Add(new stringDate(date, lines));
                lines = datagram.ReadLine();
            }
            quicksort(0, vector.Count - 1);
            // MergeSort(vector);
        }

        public void quicksort(int izq, int der)
        {
            int i, j;
            stringDate pivote, aux;
            i = izq;
            j = der;
            pivote = vector[(izq + der) / 2];
            do
            {
                while (vector[i].compareTo(pivote) < 0 && i < der)
                {
                    i++;
                }
                while (vector[j].compareTo(pivote) > 0 && j > izq)
                {
                    j--;
                }
                if (i <= j)
                {
                    aux = vector[i];
                    vector[i] = vector[j];
                    vector[j] = aux;
                    i++;
                    j--;
                }
            } while (i <= j);
            if (izq < j)
                quicksort(izq, j);
            if (i < der)
                quicksort(i, der);
        }

    }
}
