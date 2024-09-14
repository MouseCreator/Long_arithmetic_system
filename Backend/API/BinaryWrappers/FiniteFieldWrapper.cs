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
}