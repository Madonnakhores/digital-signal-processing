using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace DSPAlgorithms.Algorithms
{
    public class DCT : Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }

        public override void Run()
        {
            List<float> x = new List<float>();  
            Signal newSignal = new Signal(x, false);
            OutputSignal = newSignal;
            Console.WriteLine("Enter the coefficient");
            //string m = Console.ReadLine();
            //int M = int.Parse(m);
            int M = 3;
            for ( int k = 0; k < InputSignal.Samples.Count(); k++)
            {
                float sum = 0;
                for (int n = 0; n < InputSignal.Samples.Count(); n++)
                {
                    sum += (float)(InputSignal.Samples[n] * Math.Cos(((2*n+1)*k*Math.PI)/(2*InputSignal.Samples.Count())));
                }
                if (k == 0)
                {
                    OutputSignal.Samples.Add((float)(Math.Sqrt(1.0 / InputSignal.Samples.Count()) * sum));
                }
                else
                {
                    OutputSignal.Samples.Add((float)(Math.Sqrt(2.0 / InputSignal.Samples.Count()) * sum));
                }
            }

            //text file
            StreamWriter writeData = new StreamWriter("DCT data.txt");
            writeData.WriteLine("M Cofficient "+M);
            for (int i = 1; i <= M; i++)
            {
                writeData.WriteLine(i + " " + OutputSignal.Samples[i]);
            }
            writeData.Close();
        }
    }
}