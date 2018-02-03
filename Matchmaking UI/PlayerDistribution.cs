using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Matchmaking_UI
{
    public class PlayerDistribution : INotifyPropertyChanged
    {
        private int[] int_Div = new int[27];
        public int[] Int_Div { get { return int_Div; } }

        private int[] int_Tier = new int[7];
        public int[] Int_Tier { get { return int_Tier; } }

        private int int_Total;
        public int Int_Total { get { return int_Total; } }

        private string[] outputString = new string[35];
        public string[] OutputString { get { return outputString; } }

        private string outputStringHeader = String.Format("{0,-14} {1,-13} {2, -10:00.00}\n", "Division:", "# of Players", "%");
        public string OutputStringHeader { get { return outputStringHeader; } }

        public string[] divTierStrings = new string[35] { "Bronze5", "Bronze4", "Bronze3", "Bronze2", "Bronze1", "Silver5", "Silver4", "Silver3", "Silver2", "Silver1", "Gold5", "Gold4", "Gold3", "Gold2", "Gold1", "Platinum5", "Platinum4", "Platinum3", "Platinum2", "Platinum1", "Diamond5", "Diamond4", "Diamond3", "Diamond2", "Diamond1", "Masters", "Challenger", "Bronze Total", "Silver Total", "Gold Total", "Platinum Total", "Diamond Total", "Masters", "Challenger", "Total" };

        private SeriesCollection chart_Tier = new SeriesCollection { };
        public SeriesCollection Chart_Tier
        {
            get { return chart_Tier; }
            set
            {
                chart_Tier = value;
                OnPropertyChanged("Chart_Tier");
            } }

        private SeriesCollection chart_Div = new SeriesCollection { };
        public SeriesCollection Chart_Div
        {
            get { return chart_Div; }
            set
            {
                chart_Div = value;
                OnPropertyChanged("Chart_Div");
            } }

        public void UpdateDistro()
        {
            int[] newDistro = ManagedObject.cw_GetPlayerDistro();
            int index = 0; //for updating playerDistroInt

            //update playerDistroInt_Tier, _Div, Total
            for (int i = 0; i < int_Div.Length; ++i)
            {
                int_Div[i] = newDistro[index];
                ++index;
            }
            for (int i = 0; i < int_Tier.Length; ++i)
            {
                int_Tier[i] = newDistro[index];
                ++index;
            }
            int_Total = newDistro[index];

            //update playerDistroString
            double perc = 0D;   
            for (int i = 0; i < newDistro.Length - 1; ++i)
            {
                perc = (double)newDistro[i] / (double)newDistro[34] * 100;
                outputString[i] = String.Format("{0,-14} {1,-13} {2, -10:00.00}\n", divTierStrings[i], newDistro[i], perc);
            }

            outputString[34] = String.Format("{0,-14} {1,-13} {2, -10:00.00}\n", divTierStrings[34], newDistro[34], 100);

            //call OnPropertyChanged events
            OnPropertyChanged("OutputString");
            OnPropertyChanged("Int_Tier");
            OnPropertyChanged("Int_Div");
            OnPropertyChanged("Int_Total");
        }

        //INotifyProperty events
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        //singleton implementation
        static PlayerDistribution instance;
        public static object lockThis = new object();

        private PlayerDistribution() {  }

        public static PlayerDistribution GetInstance()
        {
            lock (lockThis)
            {
                if (instance == null) instance = new PlayerDistribution();
            }

            return instance;
        }
    }
}
