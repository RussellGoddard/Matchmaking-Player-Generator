using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;

namespace Matchmaking_UI
{
    public class PlayerDistribution : INotifyPropertyChanged
    {

        //INotifyProperty events
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        static PlayerDistribution _instance;
        public static object _lockThis = new object();

        private PlayerDistribution() { }

        public static PlayerDistribution GetInstance()
        {
            lock (_lockThis)
            {
                if (_instance == null) _instance = new PlayerDistribution(); 
            }

            return _instance;
        }

        private int[] playerDistroInt = new int[35];
        public int[] PlayerDistroInt { get { return playerDistroInt; } }

        private string[] playerDistroString = new string[35];
        public string[] PlayerDistroString { get { return playerDistroString; } }

        private string playerDistroStringHeader = String.Format("{0,-14} {1,-13} {2, -10:00.00}\n", "Division:", "# of Players", "%");
        public string PlayerDistroStringHeader { get { return playerDistroStringHeader; } }

        public string[] distroStrings = { "Bronze5", "Bronze4", "Bronze3", "Bronze2", "Bronze1", "Silver5", "Silver4", "Silver3", "Silver2", "Silver1", "Gold5", "Gold4", "Gold3", "Gold2", "Gold1", "Platinum5", "Platinum4", "Platinum3", "Platinum2", "Platinum1", "Diamond5", "Diamond4", "Diamond3", "Diamond2", "Diamond1", "Masters", "Challenger", "Bronze Total", "Silver Total", "Gold Total", "Platinum Total", "Diamond Total", "Masters", "Challenger", "Total" };

        public void UpdateDistro()
        {
            int[] newDistro = ManagedObject.cw_GetPlayerDistro();
            double perc = 0D;
                
            for (int i = 0; i < newDistro.Length - 1; ++i)
            {
               
                perc = (double)newDistro[i] / (double)newDistro[34] * 100;
                playerDistroString[i] = String.Format("{0,-14} {1,-13} {2, -10:00.00}\n", distroStrings[i], newDistro[i], perc);
            }

            playerDistroString[34] = String.Format("{0,-14} {1,-13} {2, -10:00.00}\n", distroStrings[34], newDistro[34], 100);

            OnPropertyChanged("PlayerDistroString");
            OnPropertyChanged("PlayerTotal");
        }
    }
}
