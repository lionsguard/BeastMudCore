using System.Runtime.InteropServices;

namespace System.Security
{
    public static class SecureStringExtensions
    {
        public static string ToUnsecureString(this SecureString str)
        {
            if (str == null)
                return null;

            var unmanagedStr = IntPtr.Zero;
            try
            {
                unmanagedStr = SecureStringMarshal.SecureStringToGlobalAllocUnicode(str);
                return Marshal.PtrToStringUni(unmanagedStr);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedStr);
            }
        }
    }
}
