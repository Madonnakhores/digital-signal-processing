using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class DiscreteFourierTransform : Algorithm
    {
        public Signal InputTimeDomainSignal { get; set; }
        public float InputSamplingFrequency { get; set; }
        public Signal OutputFreqDomainSignal { get; set; }

        public override void Run()
        {
            
            List<float> amplitudes = new List<float>();
            List<float> phaseShifts = new List<float>();
            List<float> X = InputTimeDomainSignal.Samples;
            List<Tuple<float,float>> result = new List<Tuple<float, float>>();
            
            for(int k = 0; k < InputTimeDomainSignal.Samples.Count(); k++)
            {
                float real = 0, imaginary = 0;
                for (int n = 0; n < InputTimeDomainSignal.Samples.Count(); n++)
                {
                    real +=( InputTimeDomainSignal.Samples[n]*((float)Math.Cos(2 * k * n * (float)Math.PI / InputTimeDomainSignal.Samples.Count())));
                    imaginary +=( -1* InputTimeDomainSignal.Samples[n]* ((float)Math.Sin(2 * k * n * (float)Math.PI / InputTimeDomainSignal.Samples.Count())));
                    Console.WriteLine(real);
                    Console.WriteLine(imaginary);
                }
                result.Add(new Tuple<float, float>(real, imaginary));
            }
            for(int i = 0; i < InputTimeDomainSignal.Samples.Count(); i++)
            {
                float phaseShift =(float) Math.Atan2(result[i].Item2, result[i].Item1);

                float amplitude =(float) Math.Sqrt(result[i].Item1*result[i].Item1 + result[i].Item2*result[i].Item2);
     
                amplitudes.Add(amplitude);
                phaseShifts.Add(phaseShift);
            }
            OutputFreqDomainSignal = new Signal(X, false);
            OutputFreqDomainSignal.FrequenciesAmplitudes = amplitudes;
            OutputFreqDomainSignal.FrequenciesPhaseShifts = phaseShifts;
        }
    }
}
