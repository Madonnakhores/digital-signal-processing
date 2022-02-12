﻿using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPAlgorithms.Algorithms
{
    public class DC_Component : Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }
        

        public override void Run()
        {
            List<float> x = new List<float>();
            Signal newSignal = new Signal(x, false);
            OutputSignal = newSignal;
            float sum = 0;
            for (int i = 0; i < InputSignal.Samples.Count(); i++)
            {
                sum += InputSignal.Samples[i];
            }
            float mean = sum / InputSignal.Samples.Count();
            for(int i = 0; i < InputSignal.Samples.Count(); i++)
            {
                OutputSignal.Samples.Add(InputSignal.Samples[i] - mean);
            }
        }
    }
}