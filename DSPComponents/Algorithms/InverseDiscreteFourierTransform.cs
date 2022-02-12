using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class InverseDiscreteFourierTransform : Algorithm
    {
        public Signal InputFreqDomainSignal { get; set; }
        public Signal OutputTimeDomainSignal { get; set; }

        public override void Run()
        {

            List<Complex> numbers = new List<Complex>();
            List<Complex> result = new List<Complex>();
            List<float> samples = new List<float>();
            for(int i = 0; i < InputFreqDomainSignal.FrequenciesAmplitudes.Count(); i++)
            {
                numbers.Add(Complex.FromPolarCoordinates(InputFreqDomainSignal.FrequenciesAmplitudes[i], InputFreqDomainSignal.FrequenciesPhaseShifts[i]));
            }
            for(int n = 0; n < InputFreqDomainSignal.FrequenciesAmplitudes.Count(); n++)
            {
                result.Add(0);
                for(int k=0; k< InputFreqDomainSignal.FrequenciesAmplitudes.Count(); k++)
                {
                    result[n] += numbers[k] * (Math.Cos((2 * k * Math.PI * n) / InputFreqDomainSignal.FrequenciesAmplitudes.Count()) + Complex.ImaginaryOne * Math.Sin((2 * k * Math.PI * n) / InputFreqDomainSignal.FrequenciesAmplitudes.Count()));
                }
                result[n] /= InputFreqDomainSignal.FrequenciesAmplitudes.Count();
            }
            for(int i = 0; i < InputFreqDomainSignal.FrequenciesAmplitudes.Count(); i++)
            {
                samples.Add((float)result[i].Real);
            }
            OutputTimeDomainSignal = new Signal(samples, false);
        }
    }
}
