using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace API.BinaryWrappers;

public static class PolyRingWrapper
{
    public static unsafe string Add(string firstInput, string secondInput, string coefModule, ref string errStr)
    {
        try
        {
            NormalizeInputString(ref firstInput, ref secondInput, ref coefModule);

            int size1 = 0, size2 = 0;
            byte[] str1 = Encoding.ASCII.GetBytes(firstInput);
            byte[] str2 = Encoding.ASCII.GetBytes(secondInput);
            byte[] modBytes = Encoding.ASCII.GetBytes(coefModule);
            byte[] errorStrBytes = Encoding.ASCII.GetBytes(errStr);

            fixed (byte* modPtr = modBytes)
            fixed (byte* errStrPtr = errorStrBytes)
            fixed (byte* str1Ptr = str1)
            fixed (byte* str2Ptr = str2)
            {
                var parsedPoly1 = CppDllMethods.PolyRingMethods.polyParse(ref size1, str1Ptr);
                var parsedPoly2 = CppDllMethods.PolyRingMethods.polyParse(ref size2, str2Ptr);

                int size = 0;
                byte* resultPtr = CppDllMethods.PolyRingMethods.polyAddition(ref size, size1, parsedPoly1, size2, parsedPoly2, modPtr, errStrPtr);

                byte[] resultBytes = new byte[size];
                Marshal.Copy((IntPtr)resultPtr, resultBytes, 0, size);
                return Encoding.ASCII.GetString(resultBytes);
            }
        }
        catch (Exception ex)
        {
            return $"An error occurred during addition: {ex.Message}";
        }
    }

    public static unsafe string Subtract(string firstInput, string secondInput, string coefModule, ref string errStr)
    {
        try
        {
            NormalizeInputString(ref firstInput, ref secondInput, ref coefModule);

            int size1 = 0, size2 = 0;
            byte[] str1 = Encoding.ASCII.GetBytes(firstInput);
            byte[] str2 = Encoding.ASCII.GetBytes(secondInput);
            byte[] modBytes = Encoding.ASCII.GetBytes(coefModule);
            byte[] errorStrBytes = Encoding.ASCII.GetBytes(errStr);

            fixed (byte* modPtr = modBytes)
            fixed (byte* errStrPtr = errorStrBytes)
            fixed (byte* str1Ptr = str1)
            fixed (byte* str2Ptr = str2)
            {
                var parsedPoly1 = CppDllMethods.PolyRingMethods.polyParse(ref size1, str1Ptr);
                var parsedPoly2 = CppDllMethods.PolyRingMethods.polyParse(ref size2, str2Ptr);

                int size = 0;
                byte* resultPtr = CppDllMethods.PolyRingMethods.polySubtraction(ref size, size1, parsedPoly1, size2, parsedPoly2, modPtr, errStrPtr);

                byte[] resultBytes = new byte[size];
                Marshal.Copy((IntPtr)resultPtr, resultBytes, 0, size);
                return Encoding.ASCII.GetString(resultBytes);
            }
        }
        catch (Exception ex)
        {
            return $"An error occurred during subtraction: {ex.Message}";
        }
    }

    public static unsafe string Multiply(string firstInput, string secondInput, string coefModule, ref string errStr)
    {
        try
        {
            NormalizeInputString(ref firstInput, ref secondInput, ref coefModule);

            int size1 = 0, size2 = 0;
            byte[] str1 = Encoding.ASCII.GetBytes(firstInput);
            byte[] str2 = Encoding.ASCII.GetBytes(secondInput);
            byte[] modBytes = Encoding.ASCII.GetBytes(coefModule);
            byte[] errorStrBytes = Encoding.ASCII.GetBytes(errStr);

            fixed (byte* modPtr = modBytes)
            fixed (byte* errStrPtr = errorStrBytes)
            fixed (byte* str1Ptr = str1)
            fixed (byte* str2Ptr = str2)
            {
                var parsedPoly1 = CppDllMethods.PolyRingMethods.polyParse(ref size1, str1Ptr);
                var parsedPoly2 = CppDllMethods.PolyRingMethods.polyParse(ref size2, str2Ptr);

                int size = 0;
                byte* resultPtr = CppDllMethods.PolyRingMethods.polyMultiplication(ref size, size1, parsedPoly1, size2, parsedPoly2, modPtr, errStrPtr);

                byte[] resultBytes = new byte[size];
                Marshal.Copy((IntPtr)resultPtr, resultBytes, 0, size);
                return Encoding.ASCII.GetString(resultBytes);
            }
        }
        catch (Exception ex)
        {
            return $"An error occurred during multiplication: {ex.Message}";
        }
    }

    public static unsafe string Divide(string firstInput, string secondInput, string coefModule, ref string errStr)
    {
        try
        {
            NormalizeInputString(ref firstInput, ref secondInput, ref coefModule);

            int size1 = 0, size2 = 0;
            byte[] str1 = Encoding.ASCII.GetBytes(firstInput);
            byte[] str2 = Encoding.ASCII.GetBytes(secondInput);
            byte[] modBytes = Encoding.ASCII.GetBytes(coefModule);
            byte[] errorStrBytes = Encoding.ASCII.GetBytes(errStr);

            fixed (byte* modPtr = modBytes)
            fixed (byte* errStrPtr = errorStrBytes)
            fixed (byte* str1Ptr = str1)
            fixed (byte* str2Ptr = str2)
            {
                var parsedPoly1 = CppDllMethods.PolyRingMethods.polyParse(ref size1, str1Ptr);
                var parsedPoly2 = CppDllMethods.PolyRingMethods.polyParse(ref size2, str2Ptr);

                int size = 0;
                byte* resultPtr = CppDllMethods.PolyRingMethods.polyDivision(ref size, size1, parsedPoly1, size2, parsedPoly2, modPtr, errStrPtr);

                byte[] resultBytes = new byte[size];
                Marshal.Copy((IntPtr)resultPtr, resultBytes, 0, size);
                return Encoding.ASCII.GetString(resultBytes);
            }
        }
        catch (Exception ex)
        {
            return $"An error occurred during division: {ex.Message}";
        }
    }

    public static unsafe string GCD(string firstInput, string secondInput, string coefModule, ref string errStr)
    {
        try
        {
            NormalizeInputString(ref firstInput, ref secondInput, ref coefModule);

            int size1 = 0, size2 = 0;
            byte[] str1 = Encoding.ASCII.GetBytes(firstInput);
            byte[] str2 = Encoding.ASCII.GetBytes(secondInput);
            byte[] modBytes = Encoding.ASCII.GetBytes(coefModule);
            byte[] errorStrBytes = Encoding.ASCII.GetBytes(errStr);

            fixed (byte* modPtr = modBytes)
            fixed (byte* errStrPtr = errorStrBytes)
            fixed (byte* str1Ptr = str1)
            fixed (byte* str2Ptr = str2)
            {
                var parsedPoly1 = CppDllMethods.PolyRingMethods.polyParse(ref size1, str1Ptr);
                var parsedPoly2 = CppDllMethods.PolyRingMethods.polyParse(ref size2, str2Ptr);

                int size = 0;
                byte* resultPtr = CppDllMethods.PolyRingMethods.polyGCD(ref size, size1, parsedPoly1, size2, parsedPoly2, modPtr, errStrPtr);

                byte[] resultBytes = new byte[size];
                Marshal.Copy((IntPtr)resultPtr, resultBytes, 0, size);
                return Encoding.ASCII.GetString(resultBytes);
            }
        }
        catch (Exception ex)
        {
            return $"An error occurred while calculating GCD: {ex.Message}";
        }
    }

    private static void NormalizeInputString(ref string firstInput, ref string secondInput, ref string coefModule)
    {
        coefModule = Regex.Replace(coefModule, "[^0-9]", "");
        firstInput = Regex.Replace(firstInput, "[^0-9x+\\-*^]", "");
        secondInput = Regex.Replace(secondInput, "[^0-9x+\\-*^]", "");

        if (string.IsNullOrWhiteSpace(coefModule)) coefModule = "1";
        if (string.IsNullOrWhiteSpace(firstInput)) firstInput = "x+1";
        if (string.IsNullOrWhiteSpace(secondInput)) secondInput = "x+2";
    }
}
