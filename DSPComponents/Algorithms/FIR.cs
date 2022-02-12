using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class FIR : Algorithm
    {
        public Signal InputTimeDomainSignal { get; set; }
        public FILTER_TYPES InputFilterType { get; set; }
        public float InputFS { get; set; }
        public float? InputCutOffFrequency { get; set; }
        public float? InputF1 { get; set; }
        public float? InputF2 { get; set; }
        public float InputStopBandAttenuation { get; set; }
        public float InputTransitionBand { get; set; }
        public Signal OutputHn { get; set; }
        public Signal OutputYn { get; set; }
        public List<float> function(Signal input1, Signal input2)
        {
            DirectConvolution dc = new DirectConvolution();
            dc.InputSignal1 = new Signal(InputTimeDomainSignal.Samples, false);
            dc.InputSignal2 = new Signal(OutputHn.Samples, false);
            dc.Run();
            return dc.OutputConvolvedSignal.Samples;
        }
        public override void Run()
        {
            List<float> x = new List<float>();
            Signal newSignal1 = new Signal(x, false);
            OutputHn = newSignal1;
            List<float> y= new List<float>();
            Signal newSignal2 = new Signal(y, false);
            OutputYn = newSignal2;
            float N =0;
            float Hn ;
            float Wn ;
            if (InputFilterType.Equals(FILTER_TYPES.LOW))
            {
                float deltaF = InputTransitionBand / InputFS;
                if (InputStopBandAttenuation <= 21)
                {
                    N = (float)(0.9 / deltaF);
                    N = (float)Math.Ceiling(N);
                    if (N % 2 == 0) N += 1;
                    float fcNormalized = (float)InputCutOffFrequency + (InputTransitionBand / 2);
                    fcNormalized /= (InputFS);
                    for (int i = -1 * (int)N / 2; i <= (int)N / 2; i++)
                    {
                        if (i == 0)
                        {
                            OutputHn.Samples.Add(2 * fcNormalized);
                        }
                        else
                        {
                            Hn = (float)(((2 * fcNormalized) * Math.Sin(i * 2 * Math.PI * fcNormalized)) / (2 * i * Math.PI * fcNormalized));
                            OutputHn.Samples.Add(Hn);

                        }
                    }

                }
                else if (InputStopBandAttenuation <= 44)
                {
                    N = (float)(3.1 / deltaF);
                    N = (float)Math.Ceiling(N);
                    if (N % 2 == 0) N += 1;
                    float fcNormalized = (float)InputCutOffFrequency + (InputTransitionBand / 2);
                    fcNormalized /= (InputFS);
                    for (int i = -1 * (int)N / 2; i <= (int)N / 2; i++)
                    {
                        if (i == 0)
                        {
                            OutputHn.Samples.Add(2 * fcNormalized * (0.5f + 0.5f));
                        }
                        else
                        {
                            Hn = (float)((2 * fcNormalized * Math.Sin(i * 2 * Math.PI * fcNormalized)) / (2 * i * Math.PI * fcNormalized));
                            Wn = (float)(0.5 + 0.5 * Math.Cos((2 * i * Math.PI) / N));
                            OutputHn.Samples.Add(Hn);
                            OutputHn.Samples.Add(Hn * Wn);
                        }
                    }
                }
                else if (InputStopBandAttenuation <= 53)
                {
                    N = (float)(3.3f / deltaF);
                    N = (float)Math.Ceiling(N);
                    if (N % 2 == 0) N += 1;
                    float fcNormalized = (float)InputCutOffFrequency + (InputTransitionBand / 2);
                    fcNormalized /= (InputFS);
                    for (int i = -1 * (int)N / 2; i <= (int)N / 2; i++)
                    {
                        float doubleFcNormlized = 2 * fcNormalized;
                        if (i == 0)
                        {
                            OutputHn.Samples.Add(doubleFcNormlized * (0.54f + 0.46f));
       
                        }
                        else
                        {
                            Hn = (float)((doubleFcNormlized * Math.Sin(i * Math.PI * doubleFcNormlized)) / (i * Math.PI * doubleFcNormlized));
                            Wn = (float)(0.54f + 0.46f * Math.Cos((2 * i * Math.PI) / N));
                            OutputHn.Samples.Add(Hn * Wn);
                        }
                        OutputHn.SamplesIndices.Add(i);
                        
                    }
                    

                }
                else if (InputStopBandAttenuation <= 74 )
                {
                    N = (float)(5.5 / deltaF);
                    N = (float)Math.Ceiling(N);
                    if (N % 2 == 0) N += 1;
                    float fcNormalized = (float)InputCutOffFrequency + (InputTransitionBand / 2);
                    fcNormalized /= (InputFS);
                    for (int i = -1 * (int)N / 2; i <= (int)N / 2; i++)
                    {
                        if (i == 0)
                        {
                            OutputHn.Samples.Add(2 * fcNormalized * (0.42f + 0.5f + 0.08f));
                        }
                        else
                        {
                            Hn = (float)(((2 * fcNormalized) * Math.Sin(i * 2 * Math.PI * fcNormalized)) / (2 * i * Math.PI * fcNormalized));
                            Wn = (float)(0.42 + 0.5 * Math.Cos((2 * i * Math.PI) / (N - 1)) + 0.08f * Math.Cos((4 * i * Math.PI) / (N - 1)));
                            OutputHn.Samples.Add(Hn * Wn);
                        }
                    }

                }


            }
            else if (InputFilterType.Equals(FILTER_TYPES.HIGH))
            {
                float deltaF = InputTransitionBand / InputFS;
                if (InputStopBandAttenuation <= 21)
                {
                    N = (float)(0.9 / deltaF);
                    N = (float)Math.Ceiling(N);
                    if (N % 2 == 0) N += 1;
                    float fcNormalized = (float)InputCutOffFrequency + (InputTransitionBand / 2);
                    for (int i = -1 * (int)N / 2; i <= (int)N / 2; i++)
                    {
                        if (i == 0)
                        {
                            OutputHn.Samples.Add((1 - 2 * fcNormalized));
                        }
                        else
                        {
                            Hn = -1 * (float)(((2 * fcNormalized) * Math.Sin(i * 2 * Math.PI * fcNormalized)) / (2 * i * Math.PI * fcNormalized));
                            //OutputHn.Samples.Add(Hn);
                            OutputHn.Samples.Add(Hn);
                        }
                    }

                }
                else if (InputStopBandAttenuation <= 44)
                {
                    N = 3.1f / deltaF;
                    N = (float)Math.Ceiling(N);
                    if (N % 2 == 0) N += 1;
                    float fcNormalized = (float)InputCutOffFrequency + (InputTransitionBand / 2);
                    fcNormalized /= (InputFS);
                    for (int i = -1 * (int)N / 2; i <= (int)N / 2; i++)
                    {
                        if (i == 0)
                        {
                            OutputHn.Samples.Add((1 - 2 * fcNormalized) * (0.5f + 0.5f));
                        }
                        else
                        {
                            Hn = -1 * (float)((2 * fcNormalized * Math.Sin(i * 2 * Math.PI * fcNormalized)) / (2 * i * Math.PI * fcNormalized));
                            Wn = (float)(0.5 + 0.5 * Math.Cos((2 * i * Math.PI) / N));
                            OutputHn.Samples.Add(Hn * Wn);
                        }
                    }

                }
                else if (InputStopBandAttenuation <= 53)
                {
                    N = 3.3f / deltaF;
                    N = (float)Math.Ceiling(N);
                    if (N % 2 == 0) N += 1;
                    float fcNormalized = (float)InputCutOffFrequency + (InputTransitionBand / 2);
                    fcNormalized /= (InputFS);
                    for (int i = -1 * (int)N / 2; i <= (int)N / 2; i++)
                    {
                        if (i == 0)
                        {
                            OutputHn.Samples.Add((1 - 2 * fcNormalized) * (0.54f + 0.46f));
                        }
                        else
                        {
                            Hn = -1 * (float)((2 * fcNormalized * Math.Sin(i * 2 * Math.PI * fcNormalized)) / (2 * i * Math.PI * fcNormalized));
                            Wn = (float)(0.54 + 0.46 * Math.Cos((2 * i * Math.PI) / N));
                            OutputHn.Samples.Add(Hn * Wn);
                        }
                    }

                }
                else if (InputStopBandAttenuation <= 74)
                {
                    N = (float)(5.5 / deltaF);
                    N = (float)Math.Ceiling(N);
                    if (N % 2 == 0) N += 1;
                   
                    float fcNormalized = (float)(InputCutOffFrequency + (InputTransitionBand / 2));
                    fcNormalized /= (InputFS);
                    for (int i = -1 * (int)N / 2; i <= (int)N / 2; i++)
                    {
                        float doubleFcNormlized = 2 * fcNormalized;
                        if (i == 0)
                        {
                            OutputHn.Samples.Add((float)((1 - doubleFcNormlized) * (0.42 + 0.5 + 0.08)));
                        }
                        else
                        {
                            Hn = (float)(((-1*doubleFcNormlized) * Math.Sin(i *  Math.PI * doubleFcNormlized)) / ( i * Math.PI * doubleFcNormlized));
                            Wn = (float)(0.42 + (0.5 * Math.Cos((2 * i * Math.PI) / (N - 1))) + (0.08 * Math.Cos((4 * i * Math.PI) / (N - 1))));
                            OutputHn.Samples.Add(Hn * Wn);
                        }
                        OutputHn.SamplesIndices.Add(i);
                    }

                }
            }
            else if (InputFilterType.Equals(FILTER_TYPES.BAND_PASS))
            {
                float deltaF = InputTransitionBand / InputFS;
                if (InputStopBandAttenuation <= 21)
                {
                    N = 0.9f / deltaF;
                    N = (float)Math.Ceiling(N);
                    if (N % 2 == 0) N += 1;
                    float fcNormalized1 = (float)InputF1 - (InputTransitionBand / 2);
                    fcNormalized1 /= (InputFS);
                    float fcNormalized2 = (float)InputF2 + (InputTransitionBand / 2);
                    fcNormalized2 /= (InputFS);
                    for (int i = -1 * (int)N / 2; i <= (int)N / 2; i++)
                    {
                        if (i == 0)
                        {
                            OutputHn.Samples.Add(2 * (fcNormalized2 - fcNormalized1));
                        }
                        else
                        {
                            Hn = (float)(((2 * fcNormalized2) * Math.Sin(i * 2 * Math.PI * fcNormalized2)) / (2 * i * Math.PI * fcNormalized2))
                                - 1 * (float)(((2 * fcNormalized1) * Math.Sin(i * 2 * Math.PI * fcNormalized1)) / (2 * i * Math.PI * fcNormalized1));
                            OutputHn.Samples.Add(Hn);
                        }
                    }

                }
                else if (InputStopBandAttenuation <= 44)
                {
                    N = (float)(3.1 / deltaF);
                    N = (float)Math.Ceiling(N);
                    if (N % 2 == 0) N += 1;
                    float fcNormalized1 = (float)InputF1 - (InputTransitionBand / 2);
                    fcNormalized1 /= (InputFS);
                    float fcNormalized2 = (float)InputF2 + (InputTransitionBand / 2);
                    fcNormalized2 /= (InputFS);
                    for (int i = -1 * (int)N / 2; i <= (int)N / 2; i++)
                    {
                        if (i == 0)
                        {
                            OutputHn.Samples.Add(2 * (fcNormalized2 - fcNormalized1) * (0.5f + 0.5f));
                        }
                        else
                        {
                            Hn = (float)(((2 * fcNormalized2) * Math.Sin(i * 2 * Math.PI * fcNormalized2)) / (2 * i * Math.PI * fcNormalized2))
                              - 1 * (float)(((2 * fcNormalized1) * Math.Sin(i * 2 * Math.PI * fcNormalized1)) / (2 * i * Math.PI * fcNormalized1));
                            Wn = (float)(0.5 + 0.5 * Math.Cos((2 * i * Math.PI) / N));
                            OutputHn.Samples.Add(Hn * Wn);
                        }
                    }

                }
                else if (InputStopBandAttenuation <= 53)
                {
                    N = (float)(3.3 / deltaF);
                    N = (float)Math.Ceiling(N);
                    if (N % 2 == 0) N += 1;
                    float fcNormalized1 = (float)InputF1 - (InputTransitionBand / 2);
                    fcNormalized1 /= (InputFS);
                    float fcNormalized2 = (float)InputF2 + (InputTransitionBand / 2);
                    fcNormalized2 /= (InputFS);
                    for (int i = -1 * (int)N / 2; i <= (int)N / 2; i++)
                    {
                        if (i == 0)
                        {
                            OutputHn.Samples.Add(2 * (fcNormalized2 - fcNormalized1) * (0.54f + 0.46f));
                        }
                        else
                        {
                            Hn = (float)(((2 * fcNormalized2) * Math.Sin(i * 2 * Math.PI * fcNormalized2)) / (2 * i * Math.PI * fcNormalized2))
                              - 1 * (float)(((2 * fcNormalized1) * Math.Sin(i * 2 * Math.PI * fcNormalized1)) / (2 * i * Math.PI * fcNormalized1));
                            Wn = (float)(0.54 + 0.46 * Math.Cos((2 * i * Math.PI) / N));
                            OutputHn.Samples.Add(Hn * Wn);
                        }
                    }

                }
                else if (InputStopBandAttenuation <= 74)
                {
                    N = (float)(5.5 / deltaF);
                    N = (float)Math.Ceiling(N);
                    if (N % 2 == 0) N += 1;
                    float fcNormalized1 = (float)InputF1 - (InputTransitionBand / 2);
                    fcNormalized1 /= (InputFS);
                    float fcNormalized2 = (float)InputF2 + (InputTransitionBand / 2);
                    fcNormalized2 /= (InputFS);
                    for (int i = -1 * (int)N / 2; i <= (int)N / 2; i++)
                    {
                        if (i == 0)
                        {
                            OutputHn.Samples.Add((float)(2 * (fcNormalized2 - fcNormalized1) * (0.42 + 0.5+ 0.08)));
                        }
                        else
                        {
                            Hn = (float)(((2 * fcNormalized2) * Math.Sin(i * 2 * Math.PI * fcNormalized2)) / (2 * i * Math.PI * fcNormalized2))
                             -1*(float)(((2 * fcNormalized1) * Math.Sin(i * 2 * Math.PI * fcNormalized1)) / (2 * i * Math.PI * fcNormalized1));
                            Wn = (float)(0.42 + 0.5 * Math.Cos((2 * i * Math.PI) / (N - 1)) + 0.08f * Math.Cos((4 * i * Math.PI) / (N - 1)));
                            OutputHn.Samples.Add(Hn * Wn);
                        }
                        OutputHn.SamplesIndices.Add(i);
                    }

                }
            }
            else if (InputFilterType.Equals(FILTER_TYPES.BAND_STOP))
            {
                float deltaF = InputTransitionBand / InputFS;

                if (InputStopBandAttenuation <= 21)
                {
                    N = (float)(0.9 / deltaF);
                    N = (float)Math.Ceiling(N);
                    if (N % 2 == 0) N += 1;
                    float fcNormalized1 = (float)InputF1 - (InputTransitionBand / 2);
                    fcNormalized1 /= (InputFS);
                    float fcNormalized2 = (float)InputF2 + (InputTransitionBand / 2);
                    fcNormalized2 /= (InputFS);
                    for (int i = -1 * (int)N / 2; i <= (int)N / 2; i++)
                    {
                        if (i == 0)
                        {
                            OutputHn.Samples.Add(1 - 2 * (fcNormalized2 - fcNormalized1));
                        }
                        else
                        {
                            Hn = (float)(((2 * fcNormalized1) * Math.Sin(i * 2 * Math.PI * fcNormalized1)) / (2 * i * Math.PI * fcNormalized1))
                                - 1 * (float)(((2 * fcNormalized2) * Math.Sin(i * 2 * Math.PI * fcNormalized2)) / (2 * i * Math.PI * fcNormalized2));
                            OutputHn.Samples.Add(Hn);
                        }
                    }

                }
                else if (InputStopBandAttenuation <= 44)
                {
                    N = (float)(3.1 / deltaF);
                    N = (float)Math.Ceiling(N);
                    if (N % 2 == 0) N += 1;
                    float fcNormalized1 = (float)InputF1 - (InputTransitionBand / 2);
                    fcNormalized1 /= (InputFS);
                    float fcNormalized2 = (float)InputF2 + (InputTransitionBand / 2);
                    fcNormalized2 /= (InputFS);
                    for (int i = -1 * (int)N / 2; i <= (int)N / 2; i++)
                    {
                        if (i == 0)
                        {
                            OutputHn.Samples.Add((1 - 2 * (fcNormalized2 - fcNormalized1)) * (0.5f + 0.5f));
                        }
                        else
                        {
                            Hn = (float)(((2 * fcNormalized1) * Math.Sin(i * 2 * Math.PI * fcNormalized1)) / (2 * i * Math.PI * fcNormalized1))
                              - 1 * (float)(((2 * fcNormalized2) * Math.Sin(i * 2 * Math.PI * fcNormalized2)) / (2 * i * Math.PI * fcNormalized2));
                            Wn = (float)(0.5 + 0.5 * Math.Cos((2 * i * Math.PI) / N));
                            OutputHn.Samples.Add(Hn * Wn);
                        }
                    }

                }
                else if (InputStopBandAttenuation <= 53)
                {
                    N = (float)(3.3 / deltaF);
                    N = (float)Math.Ceiling(N);
                    if (N % 2 == 0) N += 1;
                    float fcNormalized1 = (float)InputF1 - (InputTransitionBand / 2);
                    fcNormalized1 /= (InputFS);
                    float fcNormalized2 = (float)InputF2 + (InputTransitionBand / 2);
                    fcNormalized2 /= (InputFS);
                    for (int i = -1 * (int)N / 2; i <= (int)N / 2; i++)
                    {
                        if (i == 0)
                        {
                            OutputHn.Samples.Add((1 - 2 * (fcNormalized2 - fcNormalized1)) * (0.54f + 0.46f));
                        }
                        else
                        {
                            Hn = (float)(((2 * fcNormalized1) * Math.Sin(i * 2 * Math.PI * fcNormalized1)) / (2 * i * Math.PI * fcNormalized1))
                              - 1 * (float)(((2 * fcNormalized2) * Math.Sin(i * 2 * Math.PI * fcNormalized2)) / (2 * i * Math.PI * fcNormalized2));
                            Wn = 0.54f + 0.46f * (float)Math.Cos(2 * i * Math.PI / N);
                            OutputHn.Samples.Add(Hn * Wn);
                        }
                    }

                }
                else if (InputStopBandAttenuation <= 74)
                {
                    N = (float)(5.5 / deltaF);
                    N = (float)Math.Ceiling(N);
                    if (N % 2 == 0) N += 1;
                    float fcNormalized1 = (float)InputF1 - (InputTransitionBand / 2);
                    fcNormalized1 /= (InputFS);
                    float fcNormalized2 = (float)InputF2 + (InputTransitionBand / 2);
                    fcNormalized2 /= (InputFS);
                    for (int i = -1 * (int)N / 2; i <= (int)N / 2; i++)
                    {
                        if (i == 0)
                        {
                            OutputHn.Samples.Add((float)((1 - 2 * (fcNormalized2 - fcNormalized1)) * (0.42 + 0.5 + 0.08)));
                        }
                        else
                        {
                            Hn = (float)(((2 * fcNormalized1) * Math.Sin(i * 2 * Math.PI * fcNormalized1)) / (2 * i * Math.PI * fcNormalized1))
                             - 1 * (float)(((2 * fcNormalized2) * Math.Sin(i * 2 * Math.PI * fcNormalized2)) / (2 * i * Math.PI * fcNormalized2));
                            Wn = (float)(0.42 + 0.5 * Math.Cos((2 * i * Math.PI) / (N - 1)) + 0.08f * Math.Cos((4 * i * Math.PI) / (N - 1)));
                            OutputHn.Samples.Add(Hn * Wn);
                        }
                        OutputHn.SamplesIndices.Add(i);
                        
                    }
                    

                }
            }
             List<float> output = new List<float>();
             output = function(InputTimeDomainSignal, OutputHn);
             OutputYn.Samples = output;
              for (int i = -1 * (int)N / 2; i < output.Count()-(int)N/2 ; i++)
              {
                OutputYn.SamplesIndices.Add(i);
              }

            //text file
            StreamWriter writeData = new StreamWriter("FIR data.txt");
            writeData.WriteLine("H(n) Cofficient");
            writeData.WriteLine(OutputHn.Samples.Count());
            for(int i = 0; i < OutputHn.Samples.Count(); i++)
            {
                writeData.WriteLine(i + " " + OutputHn.Samples[i]);
            }
            writeData.Close();

        }
    }
}
