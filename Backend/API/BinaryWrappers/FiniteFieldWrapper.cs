using System.Runtime.InteropServices;
using System.Text;

namespace API.BinaryWrappers;


public class FiniteFieldWrapper
{
    public static unsafe string ExecuteFiniteField(string expr, string? number, ref string errStr)
    {
        try
        {
            byte[] exprBytes = Encoding.ASCII.GetBytes(expr);
            byte[] nBytes = Encoding.ASCII.GetBytes(number ?? "");
            byte[] errorStrBytes = Encoding.ASCII.GetBytes(errStr);

            fixed (byte* expression = exprBytes)
            fixed (byte* n = nBytes)
            fixed (byte* errStrPtr = errorStrBytes)
            {
                byte* resultPtr = CppDllMethods.FiniteFieldMethods.finite_field(expression, n, errStrPtr);

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
}
