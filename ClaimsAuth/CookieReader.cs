using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MSDN.Samples.ClaimsAuth
{
    /// <summary>
    /// WinInet.dll wrapper
    /// </summary>
    internal static class CookieReader
    {
        /// <summary>
        /// Enables the retrieval of cookies that are marked as "HTTPOnly". 
        /// Do not use this flag if you expose a scriptable interface, 
        /// because this has security implications. It is imperative that 
        /// you use this flag only if you can guarantee that you will never 
        /// expose the cookie to third-party code by way of an 
        /// extensibility mechanism you provide. 
        /// Version:  Requires Internet Explorer 8.0 or later.
        /// </summary>
        private const int INTERNET_COOKIE_HTTPONLY = 0x00002000;

        /// <summary>
        /// A general purpose option that is used to suppress behaviors on a process-wide basis.
        /// The lpBuffer parameter of the function must be a pointer to a DWORD containing the specific behavior to suppress.
        /// </summary>
        private const int INTERNET_OPTION_SUPPRESS_BEHAVIOR = 81;

        /// <summary>
        /// Suppresses the persistence of cookies, even if the server has specified them as persistent.
        /// Version:  Requires Internet Explorer 8.0 or later.
        /// </summary>
        private const int INTERNET_SUPPRESS_COOKIE_PERSIST = 3;

        /// <summary>
        /// Disables the INTERNET_SUPPRESS_COOKIE_PERSIST suppression, re-enabling the persistence of cookies.
        /// Any previously suppressed cookies will not become persistent. 
        /// Version:  Requires Internet Explorer 8.0 or later.
        /// </summary>
        private const int INTERNET_SUPPRESS_COOKIE_PERSIST_RESET = 4;

        /// <summary>
        /// Flushes entries not in use from the password cache on the hard disk drive.
        /// Also resets the cache time used when the synchronization mode is once-per-session.
        /// No buffer is required for this option.
        /// This is used by InternetSetOption.
        /// </summary>
        private const int INTERNET_OPTION_END_BROWSER_SESSION = 42;

        /// <summary>
        /// Returns cookie contents as a string
        /// </summary>
        /// <param name="url">Url to get cookie</param>
        /// <returns>Returns Cookie contents as a string</returns>
        public static string GetCookie(string url)
        {

            int size = 1024;
            StringBuilder sb = new StringBuilder(size);
            if (!NativeMethods.InternetGetCookieEx(url, null, sb, ref size, INTERNET_COOKIE_HTTPONLY, IntPtr.Zero))
            {
                if (size < 0)
                {
                    return null;
                }
                sb = new StringBuilder(size);
                if (!NativeMethods.InternetGetCookieEx(url, null, sb, ref size, INTERNET_COOKIE_HTTPONLY, IntPtr.Zero))
                {
                    return null;
                }
            }
            return sb.ToString();
        }

        public static void ClearSession()
        {
            SetOption(INTERNET_OPTION_END_BROWSER_SESSION, null);
        }

        public static bool SupressCookiePersist()
        {
            return SetOption(INTERNET_OPTION_SUPPRESS_BEHAVIOR, INTERNET_SUPPRESS_COOKIE_PERSIST);
        }

        public static bool SupressCookiePersistReset()
        {
            return SetOption(INTERNET_OPTION_SUPPRESS_BEHAVIOR, INTERNET_SUPPRESS_COOKIE_PERSIST_RESET);
        }

        private static bool SetOption(int settingCode, int? option)
        {
            IntPtr optionPtr = IntPtr.Zero;
            int size = 0;
            if (option.HasValue)
            {
                size = sizeof(int);
                optionPtr = Marshal.AllocCoTaskMem(size);
                Marshal.WriteInt32(optionPtr, option.Value);
            }

            bool success = NativeMethods.InternetSetOption(0, settingCode, optionPtr, size);

            if (optionPtr != IntPtr.Zero)
            {
                Marshal.Release(optionPtr);
            }

            return success;
        }

        private static class NativeMethods
        {

            [DllImport("wininet.dll", EntryPoint = "InternetGetCookieEx", CharSet = CharSet.Unicode, SetLastError = true)]
            public static extern bool InternetGetCookieEx(
                string url,
                string cookieName,
                StringBuilder cookieData,
                ref int size,
                int flags,
                IntPtr pReserved);

            [DllImport("wininet.dll", CharSet = CharSet.Unicode, SetLastError = true)]
            public static extern bool InternetSetOption(
                int hInternet,
                int dwOption,
                IntPtr lpBuffer,
                int dwBufferLength
            );
        }
    }
}
