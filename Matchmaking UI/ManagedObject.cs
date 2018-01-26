using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;


namespace Matchmaking_UI
{
    public static class ManagedObject
    {
        private static class ManagedWrapper
        {
            //callback functions

            //need to make sure that the parameters match, double check at source.cpp line 24
            //Current order:
            //UpdatePlayers, SyncCounter
            [DllImport("Matchmaking Player Generator.dll")]
            public static extern void cpp_AssignCallbacks(Delegate UpdatePlayers, Delegate SyncCounter);

            //player related
            [DllImport("Matchmaking Player Generator.dll")]
            public static extern void cpp_MakePlayer(int addPlayers);

            [DllImport("Matchmaking Player Generator.dll")]
            public static extern IntPtr cpp_GetDistro();

            //counter
            [DllImport("Matchmaking Player Generator.dll")]
            public static extern IntPtr cpp_SyncCounter();

            [DllImport("Matchmaking Player Generator.dll")]
            public static extern void cpp_CreateCounter(int set, int update);

            [DllImport("Matchmaking Player Generator.dll")]
            public static extern int cpp_GetCount();
        }

        //function at the start that is called by C# and passes all the callback functions
        public static void cw_AssignCallbacks(Delegate UpdatePlayers, Delegate SyncCounter)
        {
            ManagedWrapper.cpp_AssignCallbacks(UpdatePlayers, SyncCounter);
        }

        //player related
        public static void cw_GetMakePlayer(int addPlayers)
        {
            ManagedWrapper.cpp_MakePlayer(addPlayers);
        }

        public static int[] cw_GetPlayerDistro()
        {
            IntPtr ptr = ManagedWrapper.cpp_GetDistro();
            int[] result = new int[35];
            Marshal.Copy(ptr, result, 0, 35);

            return result;
        }

        //counter related
        public static int[] cw_SyncCount()
        {
            IntPtr ptr = ManagedWrapper.cpp_SyncCounter();
            int[] result = new int[2];
            Marshal.Copy(ptr, result, 0, 2);

            return result;
        }

        //create counter in c++
        public static void cw_CreateCounter(int set, int update)
        {
            ManagedWrapper.cpp_CreateCounter(set, update);
        }

        //get count from c++ counter
        public static int cw_GetCount()
        {
            return ManagedWrapper.cpp_GetCount();
        }
    }
}

