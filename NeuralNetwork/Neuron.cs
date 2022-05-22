using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    public class Neuron
    {
        public List<double> Weights { get; }
        public List<double> Inputs { get; }
        public NeuronType NeuronType { get; set; }
        public double Output { get;private set; }
        public double Delta { get; private set; }
        public Neuron(int inputCount, NeuronType type = NeuronType.Normal)
        {
            NeuronType = type;
            Weights = new List<double>();
            Inputs = new List<double>();
            InitWeightsRandom(inputCount);
        }
        private void InitWeightsRandom(int inputCount)
        {
            Random random = new Random();
            for (int i = 0; i < inputCount; i++)
            {
                if (NeuronType == NeuronType.Input)
                {
                    Weights.Add(1);
                }
                else
                {
                    Weights.Add(random.NextDouble());
                }
                Inputs.Add(0);
            }
        }
        public double FeedForward(List<double> inputs)
        {
            double sum = 0;
            for(int i = 0; i < inputs.Count; i++)
            {
                sum += Weights[i] * inputs[i];
                Inputs[i] = inputs[i];
            }
            if (NeuronType != NeuronType.Input) sum = Sigmoid(sum);
            Output = sum;
            return Output;
        }
        public void Learn(double error, double learning_rate)
        {
            if(NeuronType == NeuronType.Input)
            {
                return;
            }
            Delta = error * SigmoidDx(Output);
            for(int i = 0; i < Weights.Count; i++)
            {
                Weights[i] = Weights[i] - Inputs[i] * Delta * learning_rate;
            }
        }
        private double Sigmoid(double value)
        {
            return 1 / (1 + Math.Exp(-value));
        }
        private double SigmoidDx(double value)
        {
            double sigmoid = Sigmoid(value);
            return sigmoid * (1 - sigmoid);
        }
    }
}
