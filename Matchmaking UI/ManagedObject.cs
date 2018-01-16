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
        public static extern void MakePlayer(int addPlayers);


        [DllImport("Matchmaking Player Generator.dll")]
        public static extern IntPtr GetDistro();
    }

    class ManagedObject
    {
        public static void GetMakePlayer(int addPlayers)
        {
            ManagedWrapper.MakePlayer(addPlayers);
        }


        public static string[] distroStrings = { "Bronze5", "Bronze4", "Bronze3", "Bronze2", "Bronze1", "Silver5", "Silver4", "Silver3", "Silver2", "Silver1", "Gold5", "Gold4", "Gold3", "Gold2", "Gold1", "Platinum5", "Platinum4", "Platinum3", "Platinum2", "Platinum1", "Diamond5", "Diamond4", "Diamond3", "Diamond2", "Diamond1", "Masters", "Challenger", "Bronze Total", "Silver Total", "Gold Total", "Platinum Total", "Diamond Total", "Masters", "Challenger", "Total" };
        public static int[] GetPlayerDistro()
        {
            IntPtr ptr = ManagedWrapper.GetDistro();
            int[] result = new int[35];
            Marshal.Copy(ptr, result, 0, 35);

            return result;
        }
    }
}

