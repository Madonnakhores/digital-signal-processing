using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class DirectConvolution : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public Signal OutputConvolvedSignal { get; set; }

        /// <summary>
        /// Convolved InputSignal1 (considered as X) with InputSignal2 (considered as H)
        /// </summary>
        public override void Run()
        {
            List<float> x = new List<float>();
            Signal newSignal = new Signal(x, false);
            OutputConvolvedSignal = newSignal;
       
            for(int n= InputSignal1.SamplesIndices[0]+InputSignal2.SamplesIndices[0]; n <= InputSignal1.SamplesIndices[InputSignal1.Samples.Count() - 1] + InputSignal2.SamplesIndices[InputSignal2.Samples.Count() - 1]; n++)
            {
                float sum = 0;
                for(int k = InputSignal1.SamplesIndices[0]; k < InputSignal1.Samples.Count(); k++)
                {
                    if ((n - k) < InputSignal2.SamplesIndices[0] || k > InputSignal1.SamplesIndices[InputSignal1.Samples.Count() - 1])
                        break ;
                    if ((n - k) > InputSignal2.SamplesIndices[InputSignal2.Samples.Count() - 1] || k< InputSignal1.SamplesIndices[0])
                        continue;
                    
                        sum += (InputSignal1.Samples[InputSignal1.SamplesIndices.IndexOf(k)] * InputSignal2.Samples[InputSignal2.SamplesIndices.IndexOf(n-k)]);
                }
                if (n == InputSignal1.SamplesIndices[InputSignal1.Samples.Count() - 1] + InputSignal2.SamplesIndices[InputSignal2.Samples.Count() - 1] && sum == 0)
                    continue;

                OutputConvolvedSignal.Samples.Add(sum);
                OutputConvolvedSignal.SamplesIndices.Add(n);
            }
        }
    }
}
