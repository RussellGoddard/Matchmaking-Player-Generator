using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;


namespace Matchmaking_UI
{
    class ManagedWrapper
    {
        [DllImport("Matchmaking Player Generator.dll")]
        public static extern IntPtr PlayerDistro();


        [DllImport("Matchmaking Player Generator.dll")]
        public static extern void DeleteArray(IntPtr ptr);
    }

    class ManagedObject
    {
        public static int[] GetPlayerDistro()
        {
            IntPtr ptr = ManagedWrapper.PlayerDistro();
            int[] result = new int[8];
            Marshal.Copy(ptr, result, 0, 8);

            ManagedWrapper.DeleteArray(ptr);

            return result;
        }
    }
}

