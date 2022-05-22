using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    public static class VectorHelper
    {
        public class VectorInfos
        {
            public double IsSerious;
            public string Vector1Name;
            public string Vector2Name;
            public string Vector3Name;
            public double[] Vector1;
            public double[] Vector2;
            public double[] Vector3;
        }
        public static List<VectorInfos> GetVectorInfos(List<Anketa> anketas, int firstVectorI, int secondVectorI, int thirdVectorI)
        {
            List<VectorInfos> vectorInfos = new List<VectorInfos>();
            foreach(var anketa in anketas)
            {
                VectorInfos info = new VectorInfos();

                info.IsSerious = anketa.IsSerious;

                var vector1 = GetVectorByNumber(anketa, firstVectorI);
                var vector2 = GetVectorByNumber(anketa, secondVectorI);
                var vector3 = GetVectorByNumber(anketa, thirdVectorI);

                info.Vector1Name = vector1.Item1;
                info.Vector1 = vector1.Item2;

                info.Vector2Name = vector2.Item1;
                info.Vector2 = vector2.Item2;

                info.Vector3Name = vector3.Item1;
                info.Vector3 = vector3.Item2;

                vectorInfos.Add(info);
            }
            return vectorInfos;
        }
        private static Tuple<string,double[]> GetVectorByNumber(Anketa anketa, int number)
        {
            switch (number)
            {
                case 0: return new Tuple<string, double[]>("Авторитарный",anketa.Authoritarian);
                case 1: return new Tuple<string, double[]>("Эгоистичный",anketa.Selfish);
                case 2: return new Tuple<string, double[]>("Агресивный",anketa.Aggresive);
                case 3: return new Tuple<string, double[]>("Подозрительный",anketa.Suspicious);
                case 4: return new Tuple<string, double[]>("Подчиняемый",anketa.Subordinate);
                case 5: return new Tuple<string, double[]>("Зависимый",anketa.Dependent);
                case 6: return new Tuple<string, double[]>("Дружелюбный",anketa.Friendly);
                case 7: return new Tuple<string, double[]>("Альтруистический",anketa.Altruistic);
                default: return null;
            }
        }
    }
}
