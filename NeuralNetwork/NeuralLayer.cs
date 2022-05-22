using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    public class NeuralLayer
    {
        public List<Neuron> Neurons { get; set; }
        public int NeuronCount => Neurons?.Count ?? 0;
        public NeuralLayer(List<Neuron> neurons)
        {
            Neurons = neurons;
        }

        public List<double> GetSignals()
        {
            List<double> signals = new List<double>();
            foreach (Neuron neuron in Neurons)
            {
                signals.Add(neuron.Output);
            }
            return signals;
        }
    }
}
