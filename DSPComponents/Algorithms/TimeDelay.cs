using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class TimeDelay : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public float InputSamplingPeriod { get; set; }
        public float OutputTimeDelay { get; set; }
        List<float> Output = new List<float>();
        public override void Run()
        {
            
            float OutputTimeDelay = new float();
            DirectCorrelation dc = new DirectCorrelation();
            dc.InputSignal1=InputSignal1; 
            dc.InputSignal2=InputSignal2;
            dc.Run();
            for(int i = 0; i < InputSignal1.Samples.Count(); i++)
            {
                Output.Add(dc.OutputNormalizedCorrelation[i]);
            }
            float x = Math.Abs(Output.Max());
            float y = Math.Abs(Output.Min());
            if (x > y)
            {
                int j = Output.IndexOf(x);
                OutputTimeDelay = j * InputSamplingPeriod;
            }
            else
            {
                int j = Output.IndexOf(y);
                OutputTimeDelay = j * InputSamplingPeriod;
            }
        }
    }
}