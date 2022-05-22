using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    public class NeuronTopology
    {
        public int InputCount { get; }
        public int OutputCount { get; }
        public double LearningRate { get; set; }
        public List<int> HiddenLayers { get; }
        public NeuronTopology(int inputCount, int outputCount, params int[] hidden)
        {
            InputCount = inputCount;
            OutputCount = outputCount;
            LearningRate = 0.1;
            HiddenLayers = new List<int>();
            HiddenLayers.AddRange(hidden);
        }
    }
}
