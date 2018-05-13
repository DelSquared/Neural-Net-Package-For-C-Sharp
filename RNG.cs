using System.Collections;
using System.Collections.Generic;
using System;



namespace NeuroLib
{
    
    public static class RNG
	{
        static Random random = new Random();

        public static int GenerateFromDistribution (int[] distributionValuesPercentages){

            
            int value = (int)(100*random.NextDouble());
			int result = 0;

			for (int i = 0; i < distributionValuesPercentages.Length; i++) {

				if (value <= distributionValuesPercentages[i]){
					result=i;
					break;
				}
				else {
					value -= distributionValuesPercentages[i];
				}

			}

			return result;
		}

		//----------------------------------------------------------------------------------------------

		public static float GenerateFromUniform (float hi, float lo){
            
            float output = (float)((hi-lo)*random.NextDouble ()+lo);

			return output;
		}

		//----------------------------------------------------------------------------------------------

		public static float GenerateFromGaussian (float m, float s){
            if (s <= 0)
            {
                s = 0.025f;
            }
			return (float)Maths.LogisticInverse(0.01f*(GenerateFromUniform(1,98)-m)/(2*s));
		}

		//----------------------------------------------------------------------------------------------

		public static int BinoCoeff (int n, int k){

			int result = 0;

			if (n <= 30) {

				int nFac=2;
				int kFac=2;
				int nMinuskFac=2;

				for (int i = 3; i <= n; i++) {
					nFac *= i;
				}
				for (int i = 3; i <= k; i++) {
					kFac *= i;
				}
				for (int i = 3; i <= n-k; i++) {
					nMinuskFac *= i;
				}

				result = nFac / (kFac * nMinuskFac);
			}
			if (n < 30) {


				result = (int)(Math.Sqrt (n/(2*Math.PI*k*(n-k))) * Math.Pow (n, n) * Math.Pow (k, k) * Math.Pow (n-k, n-k));
			}


			return result;

		}

		//----------------------------------------------------------------------------------------------

		public static float SterlingFac (int n){

			float nFac = (float)(Math.Sqrt (2 * Math.PI * n) * Math.Pow (n / Math.E, n));

			return nFac;
		}

		//----------------------------------------------------------------------------------------------


		public static int ExpectedTries (int p){

			int exp = 1 / p;

			return exp;
		}



		public static int DevTries (float p){

			int dev = (int) Math.Round(Math.Sqrt((1f-p) / (p*p)), MidpointRounding.AwayFromZero);

			return dev;
		}


		//----------------------------------------------------------------------------------------------
	}
}