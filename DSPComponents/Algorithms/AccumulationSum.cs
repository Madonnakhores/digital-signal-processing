using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;


namespace DSPAlgorithms.Algorithms
{
    public class AccumulationSum : Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }

        public override void Run()
        {
            List<float> x = new List<float>();
            Signal newSignal = new Signal(x, false);
            OutputSignal = newSignal;

            for (int i = 0; i < InputSignal.Samples.Count(); i++)
            {
                
                if (i == 0) {
                    OutputSignal.Samples.Add(InputSignal.Samples[i]);
                }
                else
                {
                    OutputSignal.Samples.Add(InputSignal.Samples[i] + OutputSignal.Samples[OutputSignal.Samples.Count() - 1]);
                }
                
            }
        }
    }
}
