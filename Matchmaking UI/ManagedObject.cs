using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;


namespace Matchmaking_UI
{
    public static class ManagedWrapper
    {
        //callback functions

        //need to make sure that the parameters match, double check at source.cpp line 24
        //Current order:
        //UpdatePlayers, SyncCounter
        [DllImport("Matchmaking Player Generator.dll")]
        public static extern void AssignCallbacks(Delegate UpdatePlayers, Delegate SyncCounter);

        //player related
        [DllImport("Matchmaking Player Generator.dll")]
        public static extern void MakePlayer(int addPlayers);

        [DllImport("Matchmaking Player Generator.dll")]
        public static extern IntPtr GetDistro();

        //counter
        [DllImport("Matchmaking Player Generator.dll")]
        public static extern IntPtr SyncCounter();
    }

    public static class ManagedObject
    {
        //player related
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

        //counter
        public static int[] SyncCounter()
        {
            IntPtr ptr = ManagedWrapper.SyncCounter();
            int[] result = new int[2];
            Marshal.Copy(ptr, result, 0, 2);

            return result;
        }
    }
}

