using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

using DSPAlgorithms;
namespace DSPAlgorithms.Algorithms
{
    public class Sampling : Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }
        public Signal preProcessing1OutputSignal { get; set; }
        public Signal preProcessing2OutputSignal { get; set; }
        public int M { get; set; }
        public int L { get; set; }
        public List<float> OutputList = new List<float>();
        public List<float> callFIR(Signal InputSignal)
        {
            FIR lowFilter = new FIR();
            lowFilter.InputTimeDomainSignal = InputSignal;
            lowFilter.InputFilterType = DSPAlgorithms.DataStructures.FILTER_TYPES.LOW;
            lowFilter.InputFS = 8000;
            lowFilter.InputCutOffFrequency = 1500;
            lowFilter.InputStopBandAttenuation = 50;
            lowFilter.InputTransitionBand = 500;
            lowFilter.Run();
            return lowFilter.OutputYn.Samples;
        }
        public override void Run()
        {
            List<float> output1 = new List<float>();
            Signal new_signal1 = new Signal(output1, false);
            OutputSignal = new_signal1;
            List<float> output2 = new List<float>();
            Signal new_signal2 = new Signal(output2, false);
            preProcessing1OutputSignal = new_signal2;
            List<float> output3 = new List<float>();
            Signal new_signal3 = new Signal(output2, false);
            preProcessing2OutputSignal = new_signal3;


            if (M == 0 && L != 0)
            {
                L = L - 1;
                for (int i = 0; i < InputSignal.Samples.Count() - 1; i++)
                {
                    preProcessing1OutputSignal.Samples.Add(InputSignal.Samples[i]);
                    for (int j = 0; j < L; j++)
                    {
                        preProcessing1OutputSignal.Samples.Add(0);
                    }
                }
                preProcessing1OutputSignal.Samples.Add(InputSignal.Samples[InputSignal.Samples.Count() - 1]);
                OutputList = callFIR(preProcessing1OutputSignal);
                for (int i = 0; i <1165; i++)
                {
                    OutputSignal.Samples.Add(OutputList[i]);
                }
            }
            else if (M != 0 && L == 0)
            {
                OutputList = callFIR(InputSignal);

                for (int i = 0; i < OutputList.Count() ; i = (i + M))
                {
                    OutputSignal.Samples.Add(OutputList[i]);
                }
            }
            else if (M != 0 && L != 0)
            {
                L = L - 1;
                M = M - 1;
                for (int i = 0; i < InputSignal.Samples.Count(); i++)
                {
                    preProcessing1OutputSignal.Samples.Add(InputSignal.Samples[i]);
                    for (int j = 1; j <= L; j++)
                    {
                        preProcessing1OutputSignal.Samples.Add(0);
                    }
                }
                OutputList = callFIR(preProcessing1OutputSignal);
                preProcessing2OutputSignal.Samples = OutputList;
                for (int i = 0; i < preProcessing2OutputSignal.Samples.Count(); i = (i + M + 1))
                {
                    OutputSignal.Samples.Add(preProcessing2OutputSignal.Samples[i]);
                    Console.WriteLine(preProcessing2OutputSignal.Samples[i]);
                }
            }
            else
            {
                Console.WriteLine("ERROR :(");
            }
        }
    }
}