using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    public class NeuralNetwork
    {
        public NeuronTopology Topology { get; }
        public List<NeuralLayer> Layers { get; }
        public NeuralNetwork(NeuronTopology topology)
        {
            Topology = topology;
            Layers = new List<NeuralLayer>();
            CreateInputLayer();
            CreateHiddenLayers();
            CreateOutputLayer();
        }
        public Neuron FeedForward(params double[] inputSignals)
        {
            SendSignalToInputLayer(inputSignals);
            SendSignalToLayers(inputSignals);
            if(Topology.OutputCount == 1)
            {
                return Layers.Last().Neurons[0];
            }
            else
            {
                return Layers.Last().Neurons.OrderByDescending(x => x.Output).First();
            }
        }
        public double Learn(List<Tuple<double, double[]>> dataset, int epoch)
        {
            double error = 0.0;
            for(int i = 0; i < epoch; i++)
            {
                foreach(var data in dataset)
                {
                    error += Backpropagation(data.Item1, data.Item2);
                }
            }
            return error / epoch;
        }
        private double Backpropagation(double expected, params double[] inputs)
        {
            var actual = FeedForward(inputs).Output;
            var diff = actual - expected;
            foreach(var neuron in Layers.Last().Neurons)
            {
                neuron.Learn(diff, Topology.LearningRate);
            }
            for(int i = Layers.Count-2; i>=0; i--)
            {
                var layer = Layers[i];
                var prevLayer = Layers[i + 1];
                for(int j = 0; j < layer.NeuronCount; j++)
                {
                    var neuron = layer.Neurons[j];
                    for(int k = 0; k < prevLayer.NeuronCount; k++)
                    {
                        var prevNeuron = prevLayer.Neurons[k];
                        var error = prevNeuron.Weights[j] * prevNeuron.Delta;
                        neuron.Learn(error, Topology.LearningRate);
                    }
                }
            }
            return diff * diff;
        }
        private void SendSignalToInputLayer(params double[] inputSignals)
        {
            for (int i = 0; i < inputSignals.Length; i++)
            {
                var signal = new List<double> { inputSignals[i] };
                var neuron = Layers[0].Neurons[i];

                neuron.FeedForward(signal);
            }
        }
        private void SendSignalToLayers(params double[] inputSignals)
        {
            for(int i = 1; i < Layers.Count; i++)
            {
                var layer = Layers[i];
                var prevSignals = Layers[i - 1].GetSignals();
                foreach(var neuron in layer.Neurons)
                {
                    neuron.FeedForward(prevSignals);
                }
            }
        }
        private void CreateInputLayer()
        {
            List<Neuron> neurons = new List<Neuron>();
            for(int i = 0; i < Topology.InputCount; i++)
            {
                neurons.Add(new Neuron(1,NeuronType.Input));
            }
            Layers.Add(new NeuralLayer(neurons));
        }
        private void CreateHiddenLayers()
        {
            
            for (int i = 0; i < Topology.HiddenLayers.Count; i++)
            {
                List<Neuron> neurons = new List<Neuron>();
                for (int j = 0; j < Topology.HiddenLayers[i]; j++)
                {
                    neurons.Add(new Neuron(Layers.Last().NeuronCount, NeuronType.Output));
                }
                Layers.Add(new NeuralLayer(neurons));
            }
            
        }

        private void CreateOutputLayer()
        {
            List<Neuron> neurons = new List<Neuron>();
            for (int i = 0; i < Topology.OutputCount; i++)
            {
                neurons.Add(new Neuron(Layers.Last().NeuronCount, NeuronType.Output));
            }
            Layers.Add(new NeuralLayer(neurons));
        }

        
    }
}
