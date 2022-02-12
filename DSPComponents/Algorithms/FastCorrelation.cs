using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class FastCorrelation : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public Signal InputSignal3 { get; set; }
        public List<float> OutputNonNormalizedCorrelation { get; set; }
        public List<float> OutputNormalizedCorrelation { get; set; }

        // List<float> listforsignal3 = new List<float>();

        public override void Run()
        {

            List<float> signal1 = new List<float>();
            List<float> signal2 = new List<float>();
            List<Complex> list1complex = new List<Complex>();
            List<Complex> list2complex = new List<Complex>();
            float sumofsignal1 = 0;
            float sumofsignal2 = 0;
            float normalizationresult;

            for (int k = 0; k < InputSignal1.Samples.Count; k++)
            {
                float cosval = 0;
                float sinval = 0;
                for (int n = 0; n < InputSignal1.Samples.Count; n++)
                {

                    float cosvalueforeach = (float)Math.Cos((k * 2 * Math.PI * n) / InputSignal1.Samples.Count);

                    cosval = cosval + (cosvalueforeach * InputSignal1.Samples[n]);

                    float sinvalueforeach = -((float)Math.Sin((k * 2 * Math.PI * n) / InputSignal1.Samples.Count));

                    sinval = sinval + (sinvalueforeach * InputSignal1.Samples[n]);

                }

                Complex number = new Complex(cosval, sinval);
                list1complex.Add(number);
            }
            if (InputSignal2 != null)
            {
                for (int k = 0; k < InputSignal2.Samples.Count; k++)
                {
                    float cosval = 0;
                    float sinval = 0;
                    for (int n = 0; n < InputSignal2.Samples.Count; n++)
                    {

                        float cosvalueforeach = (float)Math.Cos((k * 2 * Math.PI * n) / InputSignal2.Samples.Count);

                        cosval = cosval + (cosvalueforeach * InputSignal2.Samples[n]);

                        float sinvalueforeach = -((float)Math.Sin((k * 2 * Math.PI * n) / InputSignal2.Samples.Count));

                        sinval = sinval + (sinvalueforeach * InputSignal2.Samples[n]);

                    }

                    Complex number = new Complex(cosval, sinval);
                    list2complex.Add(number);
                }
            }

            List<Complex> listcomplex = new List<Complex>();
            for (int i = 0; i < InputSignal1.Samples.Count; i++)
            {
                sumofsignal1 = sumofsignal1 + (InputSignal1.Samples[i] * InputSignal1.Samples[i]);
            }
            if (InputSignal2 == null)
            {
                for (int i = 0; i < InputSignal1.Samples.Count; i++)
                {

                    listcomplex.Add(list1complex[i] * Complex.Conjugate(list1complex[i]));
                }


                normalizationresult = (float)Math.Sqrt(sumofsignal1 * sumofsignal1) / InputSignal1.Samples.Count();
            }
            else
            {
                for (int i = 0; i < InputSignal2.Samples.Count; i++)
                {
                    listcomplex.Add(list2complex[i] * Complex.Conjugate(list1complex[i]));
                }
                for (int i = 0; i < InputSignal2.Samples.Count; i++)
                {
                    sumofsignal2 = sumofsignal2 + (InputSignal2.Samples[i] * InputSignal2.Samples[i]);
                }

                normalizationresult = (float)Math.Sqrt(sumofsignal1 * sumofsignal2) / InputSignal1.Samples.Count;

            }
            List<float> samples = new List<float>();
            float N = 0;
            if (InputSignal2 == null)
            {
                N = InputSignal1.Samples.Count();
            }
            else
            {
                N = InputSignal2.Samples.Count();
            }
            List<float> listofnormalized = new List<float>();
            for (int i = 0; i < N; i++)
            {
                Complex number = 0;
                Complex j = new Complex(0, 1);
                float sample = 0;
                for (int k = 0; k < N; k++)
                {
                    number += listcomplex[k] * (Math.Cos(2 * Math.PI * k * i / N)
                        + j * Math.Sin(2 * Math.PI * k * i / N));
                }

                sample = (float)number.Real / (N * N);
                samples.Add(sample);
                Console.WriteLine(sample / normalizationresult);
                listofnormalized.Add(sample / normalizationresult);
            }
            OutputNonNormalizedCorrelation = samples;
            OutputNormalizedCorrelation = listofnormalized;











        }
    }
}