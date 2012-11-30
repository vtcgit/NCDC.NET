using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace NCDCWebService
{
    public static class NCDCUtilities
    {
        //public static object FetchingLock = new object();
        public static string CurrentToken { get; set; }

        private static string[] tokens = new string[] { "PuWQzHObWoYuiPdHLOKZfLCVGQIiwmoM", "gUfsVhpVfvwOIdwvyWeuMGLHoKcpixnM", "bXyiRnDskvkrLQLNUssKUSTjGrnxLown" };
        public static string GetUnlockedToken()
        {
            while (true)
            {
                foreach(var token in tokens){
                    if (Monitor.TryEnter(token, 0))
                        return token;
                }
                Thread.Sleep(400);
            }
        }

        #region Utility Methods
        internal static byte[] ReadStream(Stream responseStream)
        {
            byte[] data = new byte[32768];

            byte[] buffer = new byte[32768];
            using (MemoryStream ms = new MemoryStream())
            {
                bool exit = false;
                while (!exit)
                {
                    int read = responseStream.Read(buffer, 0, buffer.Length);
                    if (read <= 0)
                    {
                        data = ms.ToArray();
                        exit = true;
                    }
                    else
                    {
                        ms.Write(buffer, 0, read);
                    }
                }
            }

            return data;
        }
        #endregion
    }
}
