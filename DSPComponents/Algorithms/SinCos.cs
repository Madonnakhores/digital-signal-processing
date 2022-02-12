using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class SinCos : Algorithm
    {
        public string type { get; set; }
        public float A { get; set; }
        public float PhaseShift { get; set; }
        public float AnalogFrequency { get; set; }
        public float SamplingFrequency { get; set; }
        public List<float> samples { get; set; }
        public override void Run()
        {
            samples = new List<float>();
            float f = AnalogFrequency / SamplingFrequency;
         
            if (type == "sin")
            {
                for (int n = 0; n < SamplingFrequency; n++)
                {
                    float result = (float)(A * Math.Sin((2 * n * f * Math.PI) + PhaseShift));
                    samples.Add(result);
                }


            }
            else if (type == "cos")
            {
                for (int n=0;n<SamplingFrequency; n ++)
                {
                    float result =(float)( A * Math.Cos((2 * n * f * Math.PI) + PhaseShift));
                    samples.Add(result);
                }
            }
        }
    }
}