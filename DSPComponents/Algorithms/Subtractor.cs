using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Subtractor : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public Signal OutputSignal { get; set; }

        /// <summary>
        /// To do: Subtract Signal2 from Signal1 
        /// i.e OutSig = Sig1 - Sig2 
        /// </summary>
        public override void Run()
        {
            List<float> x = new List<float>();
            Signal newSignal = new Signal(x, false);
            OutputSignal = newSignal;
            int minimum, maximum, j;
            if (InputSignal1.Samples.Count() > InputSignal2.Samples.Count)
            {
                minimum = InputSignal2.Samples.Count();
                maximum = InputSignal1.Samples.Count();
                j = 1;
            }
            else
            {
                minimum = InputSignal1.Samples.Count();
                maximum = InputSignal2.Samples.Count();
                j = 2;
            }

            for (int i = 0; i < maximum; i++)
            {
                if (i < minimum)
                    OutputSignal.Samples.Add(InputSignal1.Samples[i] - InputSignal2.Samples[i]);
                else
                {
                    if(j==1)
                    OutputSignal.Samples.Add(InputSignal1.Samples[i]);
                    else
                    OutputSignal.Samples.Add(InputSignal2.Samples[i]);
                }

            }


        }
    }
    
}