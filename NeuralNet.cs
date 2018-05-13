using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace NeuroLib
{
    public class MLP
    {
        public int inputs { get; set; }
        public int outputs { get; set; }
        public int layers { get; set; }
        public int layerSize { get; set; }
        public string activation { get; set; }
        public float[,] inputWeights { get; set; }
        public float[,] outputWeights { get; set; }
        public float[][,] hiddenWeights { get; set; }
        public MLP(int Inputs, int Outputs, int Layers, int LayerSize, string Activation)
        {
            inputs = Inputs;
            outputs = Outputs;
            layers = Layers;
            layerSize = LayerSize;
            activation = Activation;

            inputWeights = Matrix.UniformRandomMatrix(inputs, layerSize, 1, -1);
            outputWeights = Matrix.UniformRandomMatrix(layerSize, outputs, 1, -1);

            hiddenWeights = new float[layers][,];
            for (int i = 0; i < layers; i++)
            {
                hiddenWeights[i] = Matrix.UniformRandomMatrix(layerSize, layerSize, 1, -1);
            }
        }
        public static float[][,] Evaluate(MLP brain, float[][,] input)
        {
            int iter = brain.layers;
            float[][,] output = new float[input.Length][,];
            for (int i = 0; i < input.Length; i++)
            {
                float[,] pre = new float[brain.layerSize, 1];
                pre = Matrix.Map(Matrix.Inner(brain.inputWeights, input[i]), brain.activation);
                float[,] hid = new float[brain.layerSize, 1];
                for (int k = 0; k < brain.layers; k++)
                {
                    hid = Matrix.Map(Matrix.Inner(brain.hiddenWeights[k], pre), brain.activation);
                }
                output[i] = new float[brain.outputs, 1];
                output[i] = Matrix.Map(Matrix.Inner(brain.outputWeights, hid), brain.activation);
            }

            return output;
        }
        public static void Mutate(MLP brain, float lo, float hi)
        {
            brain.inputWeights = Matrix.Add(brain.inputWeights, Matrix.UniformRandomMatrix(brain.inputs, brain.layerSize, 0.5f, -0.5f), 1);
            for (int k = 0; k < brain.layers; k++)
            {
                brain.hiddenWeights[k] = Matrix.Add(brain.hiddenWeights[k], Matrix.UniformRandomMatrix(brain.layerSize, brain.layerSize, 0.5f, -0.5f), 2);

            }
            brain.outputWeights = Matrix.Add(brain.outputWeights, Matrix.UniformRandomMatrix(brain.layerSize, brain.outputs, 0.5f, -0.5f), 1);


            return;
        }
        //Train function still in development----------------------------
        public static void Train(MLP brain, float[][,] x, float[][,] y, int generations, float rate)
        {
            for (int i = 0; i < generations; i++)
            {
                float[][,] Y = MLP.Evaluate(brain, x);
                
                for (int j = 0; j < generations; j++)
                {
                    float step = -rate * Matrix.Error(Y[j], y[j], "dMeanSquare");
                    brain.outputWeights = Matrix.Increment(brain.outputWeights,Matrix.Map(Y[j], "dLogistic"),step);



                }

            }

            return;
        }
        //------------------------------------------------------------------
        public static MLP Breed(MLP brain1, MLP brain2, string type)
        {
            MLP frenkenstein = new MLP(brain1.inputs, brain1.outputs, brain1.layers, brain1.layerSize, brain1.activation);
            if (brain1.inputs != brain2.inputs || brain1.outputs != brain2.outputs || brain1.layerSize != brain2.layerSize || brain1.layers != brain2.layers || brain1.activation != brain2.activation)
            {
                Console.WriteLine("oops");
            }
            else
            { 
                switch (type)
                {
                    case "Swap":
                        frenkenstein.inputWeights = brain1.inputWeights;
                        for (int i = 0; i < frenkenstein.layers; i++)
                        {
                            if (i % 2 == 0)
                            {
                                frenkenstein.hiddenWeights[i] = brain2.hiddenWeights[i];
                            }
                            else
                            {
                                frenkenstein.hiddenWeights[i] = brain1.hiddenWeights[i];
                            }
                        }

                        frenkenstein.outputWeights = brain2.outputWeights;

                        break;

                    case "Average":
                        frenkenstein.inputWeights = Matrix.Add(brain1.inputWeights, brain2.inputWeights, 0.5f);
                        for (int i = 0; i < frenkenstein.layers; i++)
                        {
                            frenkenstein.hiddenWeights[i] = Matrix.Add(brain1.hiddenWeights[i], brain2.hiddenWeights[i], 0.5f);
                        }

                        frenkenstein.outputWeights = Matrix.Add(brain1.outputWeights, brain2.outputWeights, 0.5f);

                        break;

                    case "Conjoin":

                        int bI1 = (int)Decimal.Floor((brain1.inputs) / 2);
                        int bI2 = (int)Decimal.Floor((brain2.inputs) / 2);
                        int bH1 = (int)Decimal.Floor((brain1.layerSize) / 2);
                        int bH2 = (int)Decimal.Floor((brain2.layerSize) / 2);

                        frenkenstein.inputWeights = Matrix.Concatenate(Matrix.Slice(brain1.inputWeights,0,bI1,0, brain1.layerSize),Matrix.Slice(brain2.inputWeights, bI1, brain2.inputs, 0, brain1.layerSize),0);
                        for (int i = 0; i < frenkenstein.layers; i++)
                        {
                            frenkenstein.hiddenWeights[i] = Matrix.Concatenate(Matrix.Slice(brain1.hiddenWeights[i], 0, bH1, 0, brain1.layerSize), Matrix.Slice(brain2.hiddenWeights[i], bH1, brain1.layerSize, 0, brain1.layerSize), 0);
                        }

                        frenkenstein.outputWeights = Matrix.Concatenate(Matrix.Slice(brain1.outputWeights, 0, bH1, 0, brain1.layerSize), Matrix.Slice(brain2.outputWeights, bH1, brain1.layerSize, 0, brain1.layerSize), 0);

                        break;
                }

                
            }
            return frenkenstein;
        }

    }
}
