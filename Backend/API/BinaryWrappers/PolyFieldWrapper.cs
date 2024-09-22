using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using API.BinaryWrappers;

public static class PolyFieldWrapper
{
    public static unsafe string Add(string coefModule, string powModule, string firstPoly, string secondPoly)
    {
        try
        {
            NormalizeInput(ref coefModule, ref powModule, ref firstPoly, ref secondPoly);

            int size1 = 0, size2 = 0, polyModSize = 0;
            byte[] str1 = Encoding.ASCII.GetBytes(firstPoly);
            byte[] str2 = Encoding.ASCII.GetBytes(secondPoly);
            byte[] modBytes = Encoding.ASCII.GetBytes(coefModule);
            byte[] polyModBytes = Encoding.ASCII.GetBytes(powModule);
            byte[] errorStrBytes = Encoding.ASCII.GetBytes("");

            fixed (byte* modPtr = modBytes)
            fixed (byte* polyModPtr = polyModBytes)
            fixed (byte* errStrPtr = errorStrBytes)
            fixed (byte* str1Ptr = str1)
            fixed (byte* str2Ptr = str2)
            {
                var parsedPoly1 = CppDllMethods.PolyRingMethods.polyParse(ref size1, str1Ptr);
                var parsedPoly2 = CppDllMethods.PolyRingMethods.polyParse(ref size2, str2Ptr);
                var polyModparsed = CppDllMethods.PolyRingMethods.polyParse(ref polyModSize, polyModPtr);

                int size = 0;
                byte* resultPtr = CppDllMethods.PolyFieldMethods.polyFieldAddition(ref size, size1, parsedPoly1, size2, parsedPoly2, polyModSize, polyModparsed, modPtr, errStrPtr);

                byte[] resultBytes = new byte[size];
                Marshal.Copy((IntPtr)resultPtr, resultBytes, 0, size);
                return Encoding.ASCII.GetString(resultBytes);
            }
        }
        catch (Exception ex)
        {
            return $"Error in addition: {ex.Message}";
        }
    }

    public static unsafe string Subtract(string coefModule, string powModule, string firstPoly, string secondPoly)
    {
        try
        {
            NormalizeInput(ref coefModule, ref powModule, ref firstPoly, ref secondPoly);

            int size1 = 0, size2 = 0, polyModSize = 0;
            byte[] str1 = Encoding.ASCII.GetBytes(firstPoly);
            byte[] str2 = Encoding.ASCII.GetBytes(secondPoly);
            byte[] modBytes = Encoding.ASCII.GetBytes(coefModule);
            byte[] polyModBytes = Encoding.ASCII.GetBytes(powModule);
            byte[] errorStrBytes = Encoding.ASCII.GetBytes("");

            fixed (byte* modPtr = modBytes)
            fixed (byte* polyModPtr = polyModBytes)
            fixed (byte* errStrPtr = errorStrBytes)
            fixed (byte* str1Ptr = str1)
            fixed (byte* str2Ptr = str2)
            {
                var parsedPoly1 = CppDllMethods.PolyRingMethods.polyParse(ref size1, str1Ptr);
                var parsedPoly2 = CppDllMethods.PolyRingMethods.polyParse(ref size2, str2Ptr);
                var polyModparsed = CppDllMethods.PolyRingMethods.polyParse(ref polyModSize, polyModPtr);

                int size = 0;
                byte* resultPtr = CppDllMethods.PolyFieldMethods.polyFieldSubtraction(ref size, size1, parsedPoly1, size2, parsedPoly2, polyModSize, polyModparsed, modPtr, errStrPtr);

                byte[] resultBytes = new byte[size];
                Marshal.Copy((IntPtr)resultPtr, resultBytes, 0, size);
                return Encoding.ASCII.GetString(resultBytes);
            }
        }
        catch (Exception ex)
        {
            return $"Error in subtraction: {ex.Message}";
        }
    }

    public static unsafe string Multiply(string coefModule, string powModule, string firstPoly, string secondPoly)
    {
        try
        {
            NormalizeInput(ref coefModule, ref powModule, ref firstPoly, ref secondPoly);

            int size1 = 0, size2 = 0, polyModSize = 0;
            byte[] str1 = Encoding.ASCII.GetBytes(firstPoly);
            byte[] str2 = Encoding.ASCII.GetBytes(secondPoly);
            byte[] modBytes = Encoding.ASCII.GetBytes(coefModule);
            byte[] polyModBytes = Encoding.ASCII.GetBytes(powModule);
            byte[] errorStrBytes = Encoding.ASCII.GetBytes("");

            fixed (byte* modPtr = modBytes)
            fixed (byte* polyModPtr = polyModBytes)
            fixed (byte* errStrPtr = errorStrBytes)
            fixed (byte* str1Ptr = str1)
            fixed (byte* str2Ptr = str2)
            {
                var parsedPoly1 = CppDllMethods.PolyRingMethods.polyParse(ref size1, str1Ptr);
                var parsedPoly2 = CppDllMethods.PolyRingMethods.polyParse(ref size2, str2Ptr);
                var polyModparsed = CppDllMethods.PolyRingMethods.polyParse(ref polyModSize, polyModPtr);

                int size = 0;
                byte* resultPtr = CppDllMethods.PolyFieldMethods.polyFieldMultiplication(ref size, size1, parsedPoly1, size2, parsedPoly2, polyModSize, polyModparsed, modPtr, errStrPtr);

                byte[] resultBytes = new byte[size];
                Marshal.Copy((IntPtr)resultPtr, resultBytes, 0, size);
                return Encoding.ASCII.GetString(resultBytes);
            }
        }
        catch (Exception ex)
        {
            return $"Error in multiplication: {ex.Message}";
        }
    }

    public static unsafe string Inverse(string coefModule, string powModule, string poly)
    {
        try
        {
            string _ = "";
            NormalizeInput(ref coefModule, ref powModule, ref poly, ref _);

            int size1 = 0, polyModSize = 0;
            byte[] str1 = Encoding.ASCII.GetBytes(poly);
            byte[] modBytes = Encoding.ASCII.GetBytes(coefModule);
            byte[] polyModBytes = Encoding.ASCII.GetBytes(powModule);
            byte[] errorStrBytes = Encoding.ASCII.GetBytes("");

            fixed (byte* modPtr = modBytes)
            fixed (byte* polyModPtr = polyModBytes)
            fixed (byte* errStrPtr = errorStrBytes)
            fixed (byte* str1Ptr = str1)
            {
                var parsedPoly1 = CppDllMethods.PolyRingMethods.polyParse(ref size1, str1Ptr);
                var polyModparsed = CppDllMethods.PolyRingMethods.polyParse(ref polyModSize, polyModPtr);

                int size = 0;
                byte* resultPtr = CppDllMethods.PolyFieldMethods.polyFieldInversion(ref size, size1, parsedPoly1, polyModSize, polyModparsed, modPtr, errStrPtr);

                byte[] resultBytes = new byte[size];
                Marshal.Copy((IntPtr)resultPtr, resultBytes, 0, size);
                return Encoding.ASCII.GetString(resultBytes);
            }
        }
        catch (Exception ex)
        {
            return $"Error in inversion: {ex.Message}";
        }
    }

    public static unsafe string Divide(string coefModule, string powModule, string numerator, string denominator)
    {
        try
        {
            NormalizeInput(ref coefModule, ref powModule, ref numerator, ref denominator);

            int size1 = 0, size2 = 0, polyModSize = 0;
            byte[] str1 = Encoding.ASCII.GetBytes(numerator);
            byte[] str2 = Encoding.ASCII.GetBytes(denominator);
            byte[] modBytes = Encoding.ASCII.GetBytes(coefModule);
            byte[] polyModBytes = Encoding.ASCII.GetBytes(powModule);
            byte[] errorStrBytes = Encoding.ASCII.GetBytes("");

            fixed (byte* modPtr = modBytes)
            fixed (byte* polyModPtr = polyModBytes)
            fixed (byte* errStrPtr = errorStrBytes)
            fixed (byte* str1Ptr = str1)
            fixed (byte* str2Ptr = str2)
            {
                var parsedPoly1 = CppDllMethods.PolyRingMethods.polyParse(ref size1, str1Ptr);
                var parsedPoly2 = CppDllMethods.PolyRingMethods.polyParse(ref size2, str2Ptr);
                var polyModparsed = CppDllMethods.PolyRingMethods.polyParse(ref polyModSize, polyModPtr);

                int size = 0;
                byte* resultPtr = CppDllMethods.PolyFieldMethods.polyFieldDivision(ref size, size1, parsedPoly1, size2, parsedPoly2, polyModSize, polyModparsed, modPtr, errStrPtr);

                byte[] resultBytes = new byte[size];
                Marshal.Copy((IntPtr)resultPtr, resultBytes, 0, size);
                return Encoding.ASCII.GetString(resultBytes);
            }
        }
        catch (Exception ex)
        {
            return $"Error in division: {ex.Message}";
        }
    }

    private static void NormalizeInput(ref string coefModule, ref string powModule, ref string poly1, ref string poly2)
    {
        coefModule = Regex.Replace(coefModule, "[^0-9]", "");
        powModule = Regex.Replace(powModule, "[^0-9x+\\-*^]", "");
        poly1 = Regex.Replace(poly1, "[^0-9x+\\-*^]", "");
        poly2 = Regex.Replace(poly2, "[^0-9x+\\-*^]", "");

        if (string.IsNullOrWhiteSpace(coefModule)) coefModule = "1";
        if (string.IsNullOrWhiteSpace(powModule)) powModule = "x+3";
        if (string.IsNullOrWhiteSpace(poly1)) poly1 = "x+1";
        if (string.IsNullOrWhiteSpace(poly2)) poly2 = "x+2";
    }
}
