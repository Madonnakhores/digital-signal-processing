using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Adder : Algorithm
    {
        public List<Signal> InputSignals { get; set; }
        public Signal OutputSignal { get; set; }

        public override void Run()
        {
            List<float> x = new List<float>();
            Signal newSignal = new Signal(x, false);
            OutputSignal = newSignal;
            int minimum,maximum,j;
                if (InputSignals[0].Samples.Count() > InputSignals[1].Samples.Count)
            {
                minimum = InputSignals[1].Samples.Count();
                maximum = InputSignals[0].Samples.Count();
                j = 0;
            }
            else
            {
                minimum = InputSignals[0].Samples.Count();
                maximum = InputSignals[1].Samples.Count();
                j = 1;
            }
            
            for (int i = 0; i < maximum; i++) {
                if (i < minimum)
                    OutputSignal.Samples.Add(InputSignals[0].Samples[i] + InputSignals[1].Samples[i]);
                else
                    OutputSignal.Samples.Add(InputSignals[j].Samples[i]);

            }
           
            
        }
    }
}