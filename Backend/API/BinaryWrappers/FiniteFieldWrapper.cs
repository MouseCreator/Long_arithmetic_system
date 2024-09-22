using System.Runtime.InteropServices;
using System.Text;

namespace API.BinaryWrappers;


public class FiniteFieldWrapper
{
    public static unsafe string Add(string numberA, string numberB, string mod, string errStr)
    {
        try
        {
            byte[] aBytes = Encoding.ASCII.GetBytes(numberA);
            byte[] bBytes = Encoding.ASCII.GetBytes(numberB);
            byte[] modBytes = Encoding.ASCII.GetBytes(mod);
            byte[] errorStrBytes = Encoding.ASCII.GetBytes(errStr);

            fixed (byte* aPtr = aBytes)
            fixed (byte* bPtr = bBytes)
            fixed (byte* modPtr = modBytes)
            fixed (byte* errStrPtr = errorStrBytes)
            {
                byte* resultPtr = CppDllMethods.FiniteFieldMethods.addition(aPtr, bPtr, modPtr, errStrPtr);

                int resultLength = 0;
                while (resultPtr[resultLength] != 0)
                    resultLength++;

                byte[] resultBytes = new byte[resultLength];
                Marshal.Copy((IntPtr)resultPtr, resultBytes, 0, resultLength);
                return Encoding.ASCII.GetString(resultBytes);
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred while adding numbers: {ex.Message}");
        }
    }

    public static unsafe string Subtract(string numberA, string numberB, string mod, string errStr)
    {
        try
        {
            byte[] aBytes = Encoding.ASCII.GetBytes(numberA);
            byte[] bBytes = Encoding.ASCII.GetBytes(numberB);
            byte[] modBytes = Encoding.ASCII.GetBytes(mod);
            byte[] errorStrBytes = Encoding.ASCII.GetBytes(errStr);

            fixed (byte* aPtr = aBytes)
            fixed (byte* bPtr = bBytes)
            fixed (byte* modPtr = modBytes)
            fixed (byte* errStrPtr = errorStrBytes)
            {
                byte* resultPtr = CppDllMethods.FiniteFieldMethods.subtraction(aPtr, bPtr, modPtr, errStrPtr);

                int resultLength = 0;
                while (resultPtr[resultLength] != 0)
                    resultLength++;

                byte[] resultBytes = new byte[resultLength];
                Marshal.Copy((IntPtr)resultPtr, resultBytes, 0, resultLength);
                return Encoding.ASCII.GetString(resultBytes);
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred while subtracting numbers: {ex.Message}");
        }
    }

    public static unsafe string Multiply(string numberA, string numberB, string mod, string errStr)
    {
        try
        {
            byte[] aBytes = Encoding.ASCII.GetBytes(numberA);
            byte[] bBytes = Encoding.ASCII.GetBytes(numberB);
            byte[] modBytes = Encoding.ASCII.GetBytes(mod);
            byte[] errorStrBytes = Encoding.ASCII.GetBytes(errStr);

            fixed (byte* aPtr = aBytes)
            fixed (byte* bPtr = bBytes)
            fixed (byte* modPtr = modBytes)
            fixed (byte* errStrPtr = errorStrBytes)
            {
                byte* resultPtr = CppDllMethods.FiniteFieldMethods.multiplication(aPtr, bPtr, modPtr, errStrPtr);

                int resultLength = 0;
                while (resultPtr[resultLength] != 0)
                    resultLength++;

                byte[] resultBytes = new byte[resultLength];
                Marshal.Copy((IntPtr)resultPtr, resultBytes, 0, resultLength);
                return Encoding.ASCII.GetString(resultBytes);
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred while multiplying numbers: {ex.Message}");
        }
    }

    public static unsafe string Divide(string numberA, string numberB, string mod, string errStr)
    {
        try
        {
            byte[] aBytes = Encoding.ASCII.GetBytes(numberA);
            byte[] bBytes = Encoding.ASCII.GetBytes(numberB);
            byte[] modBytes = Encoding.ASCII.GetBytes(mod);
            byte[] errorStrBytes = Encoding.ASCII.GetBytes(errStr);

            fixed (byte* aPtr = aBytes)
            fixed (byte* bPtr = bBytes)
            fixed (byte* modPtr = modBytes)
            fixed (byte* errStrPtr = errorStrBytes)
            {
                byte* resultPtr = CppDllMethods.FiniteFieldMethods.division(aPtr, bPtr, modPtr, errStrPtr);

                int resultLength = 0;
                while (resultPtr[resultLength] != 0)
                    resultLength++;

                byte[] resultBytes = new byte[resultLength];
                Marshal.Copy((IntPtr)resultPtr, resultBytes, 0, resultLength);
                return Encoding.ASCII.GetString(resultBytes);
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred while dividing numbers: {ex.Message}");
        }
    }
    
    public static unsafe string FastPow(string numberA, string degree, string mod, string errStr)
        {
            try
            {
                byte[] aBytes = Encoding.ASCII.GetBytes(numberA);
                byte[] degreeBytes = Encoding.ASCII.GetBytes(degree);
                byte[] modBytes = Encoding.ASCII.GetBytes(mod);
                byte[] errorStrBytes = Encoding.ASCII.GetBytes(errStr);

                fixed (byte* aPtr = aBytes)
                fixed (byte* degreePtr = degreeBytes)
                fixed (byte* modPtr = modBytes)
                fixed (byte* errStrPtr = errorStrBytes)
                {
                    byte* resultPtr = CppDllMethods.FiniteFieldMethods.fastPow(aPtr, degreePtr, modPtr, errStrPtr);

                    int resultLength = 0;
                    while (resultPtr[resultLength] != 0)
                        resultLength++;

                    byte[] resultBytes = new byte[resultLength];
                    Marshal.Copy((IntPtr)resultPtr, resultBytes, 0, resultLength);
                    return Encoding.ASCII.GetString(resultBytes);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred during fast power calculation: {ex.Message}");
            }
        }

        public static unsafe string Inverse(string number, string mod, string errStr)
        {
            try
            {
                byte[] numBytes = Encoding.ASCII.GetBytes(number);
                byte[] modBytes = Encoding.ASCII.GetBytes(mod);
                byte[] errorStrBytes = Encoding.ASCII.GetBytes(errStr);

                fixed (byte* numPtr = numBytes)
                fixed (byte* modPtr = modBytes)
                fixed (byte* errStrPtr = errorStrBytes)
                {
                    byte* resultPtr = CppDllMethods.FiniteFieldMethods.inverse(numPtr, modPtr, errStrPtr);

                    int resultLength = 0;
                    while (resultPtr[resultLength] != 0)
                        resultLength++;

                    byte[] resultBytes = new byte[resultLength];
                    Marshal.Copy((IntPtr)resultPtr, resultBytes, 0, resultLength);
                    return Encoding.ASCII.GetString(resultBytes);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while calculating inverse: {ex.Message}");
            }
        }

        public static unsafe string FactorizePolard(ref int size, string number, string mod, string errStr)
        {
            try
            {
                byte[] numBytes = Encoding.ASCII.GetBytes(number);
                byte[] modBytes = Encoding.ASCII.GetBytes(mod);
                byte[] errorStrBytes = Encoding.ASCII.GetBytes(errStr);

                fixed (byte* numPtr = numBytes)
                fixed (byte* modPtr = modBytes)
                fixed (byte* errStrPtr = errorStrBytes)
                {
                    byte* resultPtr = CppDllMethods.FiniteFieldMethods.factorizePolard(ref size, numPtr, modPtr, errStrPtr);

                    int resultLength = 0;
                    while (resultPtr[resultLength] != 0)
                        resultLength++;

                    byte[] resultBytes = new byte[resultLength];
                    Marshal.Copy((IntPtr)resultPtr, resultBytes, 0, resultLength);
                    return Encoding.ASCII.GetString(resultBytes);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred during Pollard's factorization: {ex.Message}");
            }
        }

        public static unsafe string FactorizeSimple(ref int size, string number, string mod, string errStr)
        {
            try
            {
                byte[] numBytes = Encoding.ASCII.GetBytes(number);
                byte[] modBytes = Encoding.ASCII.GetBytes(mod);
                byte[] errorStrBytes = Encoding.ASCII.GetBytes(errStr);

                fixed (byte* numPtr = numBytes)
                fixed (byte* modPtr = modBytes)
                fixed (byte* errStrPtr = errorStrBytes)
                {
                    byte* resultPtr = CppDllMethods.FiniteFieldMethods.factorizeSimple(ref size, numPtr, modPtr, errStrPtr);

                    int resultLength = 0;
                    while (resultPtr[resultLength] != 0)
                        resultLength++;

                    byte[] resultBytes = new byte[resultLength];
                    Marshal.Copy((IntPtr)resultPtr, resultBytes, 0, resultLength);
                    return Encoding.ASCII.GetString(resultBytes);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred during simple factorization: {ex.Message}");
            }
        }

        public static unsafe string DiscreteSqrt(ref int size, string number, string mod, string errStr)
        {
            try
            {
                byte[] numBytes = Encoding.ASCII.GetBytes(number);
                byte[] modBytes = Encoding.ASCII.GetBytes(mod);
                byte[] errorStrBytes = Encoding.ASCII.GetBytes(errStr);

                fixed (byte* numPtr = numBytes)
                fixed (byte* modPtr = modBytes)
                fixed (byte* errStrPtr = errorStrBytes)
                {
                    byte* resultPtr = CppDllMethods.FiniteFieldMethods.discreteSqrt(ref size, numPtr, modPtr, errStrPtr);

                    int resultLength = 0;
                    while (resultPtr[resultLength] != 0)
                        resultLength++;

                    byte[] resultBytes = new byte[resultLength];
                    Marshal.Copy((IntPtr)resultPtr, resultBytes, 0, resultLength);
                    return Encoding.ASCII.GetString(resultBytes);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred during discrete square root calculation: {ex.Message}");
            }
        }

        public static unsafe string DiscreteLog(string number, string baseValue, string mod, string errStr)
        {
            try
            {
                byte[] numBytes = Encoding.ASCII.GetBytes(number);
                byte[] baseBytes = Encoding.ASCII.GetBytes(baseValue);
                byte[] modBytes = Encoding.ASCII.GetBytes(mod);
                byte[] errorStrBytes = Encoding.ASCII.GetBytes(errStr);

                fixed (byte* numPtr = numBytes)
                fixed (byte* basePtr = baseBytes)
                fixed (byte* modPtr = modBytes)
                fixed (byte* errStrPtr = errorStrBytes)
                {
                    byte* resultPtr = CppDllMethods.FiniteFieldMethods.discreteLog(numPtr, basePtr, modPtr, errStrPtr);

                    int resultLength = 0;
                    while (resultPtr[resultLength] != 0)
                        resultLength++;

                    byte[] resultBytes = new byte[resultLength];
                    Marshal.Copy((IntPtr)resultPtr, resultBytes, 0, resultLength);
                    return Encoding.ASCII.GetString(resultBytes);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred during discrete logarithm calculation: {ex.Message}");
            }
        }

        public static unsafe bool IsGenerator(string number, string mod, string errStr)
        {
            try
            {
                byte[] numBytes = Encoding.ASCII.GetBytes(number);
                byte[] modBytes = Encoding.ASCII.GetBytes(mod);
                byte[] errorStrBytes = Encoding.ASCII.GetBytes(errStr);

                fixed (byte* numPtr = numBytes)
                fixed (byte* modPtr = modBytes)
                fixed (byte* errStrPtr = errorStrBytes)
                {
                    return CppDllMethods.FiniteFieldMethods.isGenerator(numPtr, modPtr, errStrPtr);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while checking if the number is a generator: {ex.Message}");
            }
        }

        public static unsafe string CarmichaelFunction(string number, string mod, string errStr)
        {
            try
            {
                byte[] numBytes = Encoding.ASCII.GetBytes(number);
                byte[] modBytes = Encoding.ASCII.GetBytes(mod);
                byte[] errorStrBytes = Encoding.ASCII.GetBytes(errStr);

                fixed (byte* numPtr = numBytes)
                fixed (byte* modPtr = modBytes)
                fixed (byte* errStrPtr = errorStrBytes)
                {
                    byte* resultPtr = CppDllMethods.FiniteFieldMethods.CarmichaelFunction(numPtr, modPtr, errStrPtr);

                    int resultLength = 0;
                    while (resultPtr[resultLength] != 0)
                        resultLength++;

                    byte[] resultBytes = new byte[resultLength];
                    Marshal.Copy((IntPtr)resultPtr, resultBytes, 0, resultLength);
                    return Encoding.ASCII.GetString(resultBytes);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while calculating the Carmichael function: {ex.Message}");
            }
        }

        public static unsafe bool IsPrime(string number, string mod, string iterations, string errStr)
        {
            try
            {
                byte[] numBytes = Encoding.ASCII.GetBytes(number);
                byte[] modBytes = Encoding.ASCII.GetBytes(mod);
                byte[] iterationsBytes = Encoding.ASCII.GetBytes(iterations);
                byte[] errorStrBytes = Encoding.ASCII.GetBytes(errStr);

                fixed (byte* numPtr = numBytes)
                fixed (byte* modPtr = modBytes)
                fixed (byte* iterationsPtr = iterationsBytes)
                fixed (byte* errStrPtr = errorStrBytes)
                {
                    return CppDllMethods.FiniteFieldMethods.isPrime(numPtr, modPtr, iterationsPtr, errStrPtr);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while checking primality: {ex.Message}");
            }
        }

        public static unsafe string EulerFunction(string number, string mod, string errStr)
        {
            try
            {
                byte[] numBytes = Encoding.ASCII.GetBytes(number);
                byte[] modBytes = Encoding.ASCII.GetBytes(mod);
                byte[] errorStrBytes = Encoding.ASCII.GetBytes(errStr);

                fixed (byte* numPtr = numBytes)
                fixed (byte* modPtr = modBytes)
                fixed (byte* errStrPtr = errorStrBytes)
                {
                    byte* resultPtr = CppDllMethods.FiniteFieldMethods.EulerFunction(numPtr, modPtr, errStrPtr);

                    int resultLength = 0;
                    while (resultPtr[resultLength] != 0)
                        resultLength++;

                    byte[] resultBytes = new byte[resultLength];
                    Marshal.Copy((IntPtr)resultPtr, resultBytes, 0, resultLength);
                    return Encoding.ASCII.GetString(resultBytes);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while calculating the Euler function: {ex.Message}");
            }
        }
}
