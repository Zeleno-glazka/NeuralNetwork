using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    public class Program
    {
        public static void Main(string[] args)
        {
            /*int vectorNumbers = 4;
            for(int i = 0; i < vectorNumbers; i++)
            {
                for(int j = i+1;j < vectorNumbers; j++)
                {
                    for(int m = j + 1; m < vectorNumbers; m++)
                    {
                     Console.WriteLine($"{i}{j}{m}");
                    }
                }
            }
            return;*/
            Console.WriteLine("Start reading from file");
            List<Anketa> anketas = Reader.ReadFromExcel("Kursovaya_1.xlsx");
            //List<Anketa> anketas = Reader.ReadFromCSV("Ankety.csv");
            Console.WriteLine($"Read is end. Readed {anketas.Count}");
            int vectorNumbers = 8;
            for (int firstVectorI = 0; firstVectorI < vectorNumbers; firstVectorI++)
            {
                for(int secondVectorI = firstVectorI+1; secondVectorI < vectorNumbers; secondVectorI++)
                {
                    for(int thirdVectorI = secondVectorI+1; thirdVectorI < vectorNumbers; thirdVectorI++)
                    {
                        Console.WriteLine("Start prepare data set");
                        var data = VectorHelper.GetVectorInfos(anketas, firstVectorI, secondVectorI, thirdVectorI);
                        var dataSet = PrepareDataSet(data);
                        Console.WriteLine("Data set is prepared");
                        Console.WriteLine("Creating Neural Network");
                        NeuronTopology topology = new NeuronTopology(48, 1, 8);
                        topology.LearningRate = 0.001;
                        NeuralNetwork neuralNetwork = new NeuralNetwork(topology);
                        Console.WriteLine("Neural Network is created");
                        Console.WriteLine("Start learning");
                        double error = neuralNetwork.Learn(dataSet, 100000);
                        Console.WriteLine("Learning is end");
                        List<List<double>> results = new List<List<double>>();
                        for (int j = 0; j < anketas.Count; j++)
                        {
                            results.Add(new List<double>() { j, anketas[j].IsSerious, neuralNetwork.FeedForward(dataSet[j].Item2).Output });
                        }
                        Console.WriteLine("Start write to Excel");
                        Writter.WriteToExcel("Result3v.xlsx", $"Вектор 1 = {data.First().Vector1Name}\nВектор 2 = {data.First().Vector2Name}\nВектор 3 = {data.First().Vector3Name}\nОшибка = {error}\n", new string[] { "#", "Expected", "Actual" }, results);
                        Console.WriteLine("Writed is end");
                    }
                }
            }
        }
        public static List<Tuple<double,double[]>> PrepareDataSet(List<VectorHelper.VectorInfos> vectors)
        {
            List<Tuple<double, double[]>> dataset = new List<Tuple<double, double[]>>();

            foreach(var vector in vectors)
            {
                List<double> answers = new List<double>();
                answers.AddRange(vector.Vector1);
                answers.AddRange(vector.Vector2);
                answers.AddRange(vector.Vector3);
                dataset.Add(new Tuple<double, double[]>(vector.IsSerious, answers.ToArray()));
            }
            return dataset;
        }
    }
}
