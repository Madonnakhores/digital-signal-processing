using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DSPAlgorithms.Algorithms
{
    public class QuantizationAndEncoding : Algorithm
    {
        // You will have only one of (InputLevel or InputNumBits), the other property will take a negative value
        // If InputNumBits is given, you need to calculate and set InputLevel value and vice versa
        public int InputLevel { get; set; }
        public int InputNumBits { get; set; }
        public Signal InputSignal { get; set; }
        public Signal OutputQuantizedSignal { get; set; }
        public List<int> OutputIntervalIndices { get; set; }
        public List<string> OutputEncodedSignal { get; set; }
        public List<float> OutputSamplesError { get; set; }


        public override void Run()
        {
            float delta;
            List<float> arrayOfIntervals = new List<float>();
            List<float> midPoint = new List<float>();
            List<float> signalResult = new List<float>();
            OutputIntervalIndices = new List<int>();
            OutputSamplesError = new List<float>();
            OutputEncodedSignal = new List<string>();
            List<float> quantizedSamples = new List<float>();
            if (InputLevel <= 0)
            {
                InputLevel = (int)Math.Pow(2, InputNumBits);
            }
            else if (InputNumBits <= 0)
            {
                InputNumBits = (int)Math.Log(InputLevel, 2);
            }
            delta = (InputSignal.Samples.Max() - InputSignal.Samples.Min()) / InputLevel;
            
            arrayOfIntervals.Add(InputSignal.Samples.Min());
            for (int i = 1; i <= InputLevel; i++) //0.2 0.4 0.6 0.8 1
            {
                    arrayOfIntervals.Add(arrayOfIntervals[i-1] + delta);
            }

            for (int i = 0; i < arrayOfIntervals.Count() -1 ; i++)
            {
                midPoint.Add((arrayOfIntervals[i] + arrayOfIntervals[i + 1]) / 2);
            }
            for (int i = 0; i < InputSignal.Samples.Count(); i++)
            {
                int index = -1;
                for (int j = 0; j < arrayOfIntervals.Count() ; j++)
                {
                    if (InputSignal.Samples[i] >= arrayOfIntervals[j] && InputSignal.Samples[i] <= arrayOfIntervals[j]+delta +0.001)
                    {
                        index = j + 1;
                        quantizedSamples.Add(((arrayOfIntervals[j] * 2 )+delta) / 2.0f);
                        OutputIntervalIndices.Add(index);
                        break;
                    }
                }

            }

            for (int i = 0; i < quantizedSamples.Count(); i++)
            {
                OutputSamplesError.Add(quantizedSamples[i] - InputSignal.Samples[i]);
            }

            for (int i = 0; i < OutputIntervalIndices.Count(); i++)
            {
                int len = Convert.ToString(OutputIntervalIndices[i]-1, 2).Length;
                string zeros = "";
                while (len < (int)Math.Log(InputLevel, 2))
                {
                    zeros += "0";
                    len++;
                }
                OutputEncodedSignal.Add(zeros + Convert.ToString(OutputIntervalIndices[i]-1, 2));
            }

            OutputQuantizedSignal = new Signal(quantizedSamples, InputSignal.Periodic);
            Console.WriteLine(String.Join(", ", OutputEncodedSignal));

        }
    }
}
