using System.Runtime.InteropServices;

namespace API.BinaryWrappers
{
	public class CppDllMethods
	{
		public static class FiniteFieldMethods
		{
			[DllImport("global-wrapper")]
			public static extern unsafe byte* addition(byte* a, byte* b, byte* mod, byte* errStr);

			[DllImport("global-wrapper")]
			public static extern unsafe byte* subtraction(byte* a, byte* b, byte* mod, byte* errStr);

			[DllImport("global-wrapper")]
			public static extern unsafe byte* multiplication(byte* a, byte* b, byte* mod, byte* errStr);

			[DllImport("global-wrapper")]
			public static extern unsafe byte* division(byte* a, byte* b, byte* mod, byte* errStr);

			[DllImport("global-wrapper")]
			public static extern unsafe byte* fastPow(byte* a, byte* degree, byte* mod, byte* errStr);

			[DllImport("global-wrapper")]
			public static extern unsafe byte* inverse(byte* num, byte* mod, byte* errStr);

			[DllImport("global-wrapper")]
			public static extern unsafe byte* factorizePolard(ref int size, byte* num, byte* mod, byte* errorStr);

			[DllImport("global-wrapper")]
			public static extern unsafe byte* factorizeSimple(ref int size, byte* num, byte* mod, byte* errorStr);

			[DllImport("global-wrapper")]
			public static extern unsafe byte* discreteSqrt(ref int size, byte* num, byte* mod, byte* errStr);

			[DllImport("global-wrapper")]
			public static extern unsafe byte* discreteLog(byte* num, byte* basa, byte* mod, byte* errStr);


			[DllImport("global-wrapper")]
			public static extern unsafe bool isGenerator(byte* num, byte* mod, byte* errStr);

			[DllImport("global-wrapper")]
			public static extern unsafe byte* CarmichaelFunction(byte* num, byte* mod, byte* errStr);

			[DllImport("global-wrapper")]
			public static extern unsafe bool isPrime(byte* num, byte* mod, byte* iterations, byte* errStr);

			[DllImport("global-wrapper")]
			public static extern unsafe byte* EulerFunction(byte* num, byte* mod, byte* errStr);
		}

		public static class PolyRingMethods
		{
			[DllImport("global-wrapper")]
			public static extern unsafe byte* polyAddition(ref int returnSize, int polySize1, byte** polyStr1,
				int polySize2, byte** polyStr2, byte* numModStr, byte* errorStr);

			[DllImport("global-wrapper")]
			public static extern unsafe byte* polySubtraction(ref int returnSize, int polySize1, byte** polyStr1,
				int polySize2, byte** polyStr2, byte* numModStr, byte* errorStr);

			[DllImport("global-wrapper")]
			public static extern unsafe byte* polyMultiplication(ref int returnSize, int polySize1, byte** polyStr1,
				int polySize2, byte** polyStr2, byte* numModStr, byte* errorStr);

			[DllImport("global-wrapper")]
			public static extern unsafe byte* polyDivision(ref int returnSize, int polySize1, byte** polyStr1,
				int polySize2, byte** polyStr2, byte* numModStr, byte* errorStr);

			[DllImport("global-wrapper")]
			public static extern unsafe byte* polyRest(ref int returnSize, int polySize1, byte** polyStr1,
				int polySize2, byte** polyStr2, byte* numModStr, byte* errorStr);

			[DllImport("global-wrapper")]
			public static extern unsafe byte* polyDerivative(ref int returnSize, int polySize1, byte** polyStr1,
				byte* numModStr, byte* errorStr);

			[DllImport("global-wrapper")]
			public static extern unsafe byte* polyGCD(ref int returnSize, int polySize1, byte** polyStr1, int polySize2,
				byte** polyStr2, byte* numModStr, byte* errorStr);

			[DllImport("global-wrapper")]
			public static extern unsafe byte* polyEvaluate(ref int returnSize, int polySize1, byte** polyStr1,
				byte* numModStr, byte* errorStr);

			[DllImport("global-wrapper")]
			public static extern unsafe byte* getCyclotomic(ref int returnSize, byte* orderStr, byte* numModStr,
				byte* errorStr);

			[DllImport("global-wrapper")]
			public static extern unsafe byte** polyParse(ref int returnSize, byte* inputPolyString);
		}

		public static class PolyFieldMethods
		{
			[DllImport("global-wrapper")]
			public static extern unsafe byte* polyFieldAddition(ref int returnSize, int polySize1, byte** polyStr1,
				int polySize2, byte** polyStr2, int polyModSize, byte** powModStr, byte* numModStr, byte* errorStr);

			[DllImport("global-wrapper")]
			public static extern unsafe byte* polyFieldSubtraction(ref int returnSize, int polySize1, byte** polyStr1,
				int polySize2, byte** polyStr2, int polyModSize, byte** powModStr, byte* numModStr, byte* errorStr);

			[DllImport("global-wrapper")]
			public static extern unsafe byte* polyFieldMultiplication(ref int returnSize, int polySize1,
				byte** polyStr1, int polySize2, byte** polyStr2, int polyModSize, byte** powModStr, byte* numModStr,
				byte* errorStr);

			[DllImport("global-wrapper")]
			public static extern unsafe byte* polyFieldDivision(ref int returnSize, int polySize1, byte** polyStr1,
				int polySize2, byte** polyStr2, int polyModSize, byte** powModStr, byte* numModStr, byte* errorStr);

			[DllImport("global-wrapper")]
			public static extern unsafe byte* polyFieldInversion(ref int returnSize, int polySize1, byte** polyStr1,
				int polyModSize, byte** powModStr, byte* numModStr, byte* errorStr);

			[DllImport("global-wrapper")]
			public static extern unsafe byte** polyParse1(ref int returnSize, byte* inputPolyString);
		}
	}
}