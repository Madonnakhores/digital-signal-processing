using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class MovingAverage : Algorithm
    {
        public Signal InputSignal { get; set; }
        public int InputWindowSize { get; set; }
        public Signal OutputAverageSignal { get; set; }
 
        public override void Run()
        {
            List<float> x = new List<float>();
            Signal newSignal = new Signal(x, false);
            OutputAverageSignal = newSignal;
            
                for (int i = InputWindowSize/2; i < InputSignal.Samples.Count()-InputWindowSize/2; i++)
                {
                    float sum = 0;

                    for(int j=i- InputWindowSize / 2 ; j<=i+InputWindowSize/2 ; j++)
                       sum += InputSignal.Samples[j] ; 

                    OutputAverageSignal.Samples.Add((float)sum / InputWindowSize);
                    OutputAverageSignal.SamplesIndices.Add(InputSignal.SamplesIndices[i]);
                }
            

        }
    }
}
