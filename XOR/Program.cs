using Encog.Engine.Network.Activation;
using Encog.ML.Data;
using Encog.ML.Data.Basic;
using Encog.ML.Train;
using Encog.Neural.Networks;
using Encog.Neural.Networks.Layers;
using Encog.Neural.Networks.Training.Propagation.Resilient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XOR
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting XOR learning");

            var network = ConstructNetwork();
            var ds = InitializeIO();
            var trainer = GetTrainer(network, ds);

            do
            {
                trainer.Iteration();
                Console.WriteLine(String.Format("Iteration: {0}, Error: {1}", 
                    trainer.IterationNumber, trainer.Error));
            }
            while (trainer.Error > .01);

            Console.WriteLine();

            foreach(var pair in ds)
            {
                var output = network.Compute(pair.Input);
                Console.WriteLine(String.Format("{0}, actual={1}, ideal={2}",
                    pair.Input, output, pair.Ideal));
            }

            Console.ReadKey();
        }

        private static IMLTrain GetTrainer(BasicNetwork network, IMLDataSet data)
        {
            return new ResilientPropagation(network, data);
        }

        private static BasicNetwork ConstructNetwork()
        {
            var network = new BasicNetwork();
            network.AddLayer(new BasicLayer(null, true, 2));
            network.AddLayer(new BasicLayer(new ActivationSigmoid(), true, 2));
            network.AddLayer(new BasicLayer(new ActivationSigmoid(), false, 1));
            network.Structure.FinalizeStructure();
            network.Reset();

            return network;
        }

        private static IMLDataSet InitializeIO()
        {
            var inputs = new double[][]
            {
                new[] { 0.0, 0.0 },
                new[] { 1.0, 0.0 },
                new[] { 0.0, 1.0 },
                new[] { 1.0, 1.0 }
            };

            var outputs = new double[][]
            {
                new[] { 0.0 },
                new[] { 1.0 },
                new[] { 1.0 },
                new[] { 0.0 }
            };

            return new BasicMLDataSet(inputs, outputs);
        }
    }
}
