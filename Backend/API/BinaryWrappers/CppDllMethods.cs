using System.Runtime.InteropServices;

namespace API.BinaryWrappers
{
	public class CppDllMethods
	{
		public static class FiniteFieldMethods
		{
			[DllImport("wrapper.so")]
			public static extern unsafe byte* finite_field(byte* expression, byte* n, byte* errorStr);
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