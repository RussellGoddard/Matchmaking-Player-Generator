using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Runtime.CompilerServices;

using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace Matchmaking_UI
{
    public class PlayerDistribution : INotifyPropertyChanged
    {
        private static int[] playerDistroInt_Div = new int[27];
        public static int[] PlayerDistroInt_Div { get { return playerDistroInt_Div; } }

        private static int[] playerDistroInt_Tier = new int[7];
        public static int[] PlayerDistroInt_Tier { get { return playerDistroInt_Tier; } }

        private static int playerDistroInt_Total;
        public static int PlayerDistroInt_Total { get { return playerDistroInt_Total; } }

        private string[] playerDistroString = new string[35];
        public string[] PlayerDistroString { get { return playerDistroString; } }

        private string playerDistroStringHeader = String.Format("{0,-14} {1,-13} {2, -10:00.00}\n", "Division:", "# of Players", "%");
        public string PlayerDistroStringHeader { get { return playerDistroStringHeader; } }

        public string[] titleDistroStrings = new string[35] { "Bronze5", "Bronze4", "Bronze3", "Bronze2", "Bronze1", "Silver5", "Silver4", "Silver3", "Silver2", "Silver1", "Gold5", "Gold4", "Gold3", "Gold2", "Gold1", "Platinum5", "Platinum4", "Platinum3", "Platinum2", "Platinum1", "Diamond5", "Diamond4", "Diamond3", "Diamond2", "Diamond1", "Masters", "Challenger", "Bronze Total", "Silver Total", "Gold Total", "Platinum Total", "Diamond Total", "Masters", "Challenger", "Total" };

        private SeriesCollection playerDistroChart_Tier = new SeriesCollection
        {
            //bronze total
            new PieSeries
            {
                Title = "Bronze",
                Values = new ChartValues<int> { PlayerDistroInt_Tier[0] },
                DataLabels = true
            },
            //silver total
            new PieSeries
            {
                Title = "Silver",
                Values = new ChartValues<int> { PlayerDistroInt_Tier[1] },
                DataLabels = true
            },
            //gold total
            new PieSeries
            {
                Title = "Gold",
                Values = new ChartValues<int> { PlayerDistroInt_Tier[2] },
                DataLabels = true
            },
            //plat total
            new PieSeries
            {
                Title = "Platinum",
                Values = new ChartValues<int> { PlayerDistroInt_Tier[3] },
                DataLabels = true
            },
            //diamond total
            new PieSeries
            {
                Title = "Diamond",
                Values = new ChartValues<int> { PlayerDistroInt_Tier[4] },
                DataLabels = true
            },
            //master total
            new PieSeries
            {
                Title = "Masters",
                Values = new ChartValues<int> { PlayerDistroInt_Tier[5] },
                DataLabels = true
            },
            //challenger total
            new PieSeries
            {
                Title = "Challenger",
                Values = new ChartValues<int> { PlayerDistroInt_Tier[6] },
                DataLabels = true
            }
        };
        public SeriesCollection PlayerDistroChart_Tier { get { return playerDistroChart_Tier; } }

        private SeriesCollection playerDistroChart_Div = new SeriesCollection
        {
            //bronze 5
            new PieSeries
            {
                Title = "Bronze 5",
                Values = new ChartValues<int> { PlayerDistroInt_Div[0] },
                DataLabels = true
            },
            //bronze 4
            new PieSeries
            {
                Title = "Bronze 4",
                Values = new ChartValues<int> { PlayerDistroInt_Div[1] },
                DataLabels = true
            },
            //bronze 3
            new PieSeries
            {
                Title = "Bronze 3",
                Values = new ChartValues<int> { PlayerDistroInt_Div[2] },
                DataLabels = true
            },
            //bronze 2
            new PieSeries
            {
                Title = "Bronze 2",
                Values = new ChartValues<int> { PlayerDistroInt_Div[3] },
                DataLabels = true
            },
            //bronze 1
            new PieSeries
            {
                Title = "Bronze 1",
                Values = new ChartValues<int> { PlayerDistroInt_Div[4] },
                DataLabels = true
            },            
            //silver 5
            new PieSeries
            {
                Title = "Silver 5",
                Values = new ChartValues<int> { PlayerDistroInt_Div[5] },
                DataLabels = true
            }, 
            //silver 4
            new PieSeries
            {
                Title = "Silver 4",
                Values = new ChartValues<int> { PlayerDistroInt_Div[6] },
                DataLabels = true
            }, 
            //silver 3
            new PieSeries
            {
                Title = "Silver 3",
                Values = new ChartValues<int> { PlayerDistroInt_Div[7] },
                DataLabels = true
            }, 
            //silver 2
            new PieSeries
            {
                Title = "Silver 2",
                Values = new ChartValues<int> { PlayerDistroInt_Div[8] },
                DataLabels = true
            }, 
            //silver 1
            new PieSeries
            {
                Title = "Silver 1",
                Values = new ChartValues<int> { PlayerDistroInt_Div[9] },
                DataLabels = true
            }, 
            //gold 5
            new PieSeries
            {
                Title = "Gold 5",
                Values = new ChartValues<int> { PlayerDistroInt_Div[10] },
                DataLabels = true
            }, 
            //gold 4
            new PieSeries
            {
                Title = "Gold 4",
                Values = new ChartValues<int> { PlayerDistroInt_Div[11] },
                DataLabels = true
            }, 
            //gold 3
            new PieSeries
            {
                Title = "Gold 3",
                Values = new ChartValues<int> { PlayerDistroInt_Div[12] },
                DataLabels = true
            }, 
            //gold 2
            new PieSeries
            {
                Title = "Gold 2",
                Values = new ChartValues<int> { PlayerDistroInt_Div[13] },
                DataLabels = true
            }, 
            //gold 1
            new PieSeries
            {
                Title = "Gold 1",
                Values = new ChartValues<int> { PlayerDistroInt_Div[14] },
                DataLabels = true
            }, 
            //plat 5
            new PieSeries
            {
                Title = "Plat 5",
                Values = new ChartValues<int> { PlayerDistroInt_Div[15] },
                DataLabels = true
            }, 
            //plat 4
            new PieSeries
            {
                Title = "Plat 4",
                Values = new ChartValues<int> { PlayerDistroInt_Div[16] },
                DataLabels = true
            }, 
            //plat 3
            new PieSeries
            {
                Title = "Plat 3",
                Values = new ChartValues<int> { PlayerDistroInt_Div[17] },
                DataLabels = true
            }, 
            //plat 2
            new PieSeries
            {
                Title = "Plat 2",
                Values = new ChartValues<int> { PlayerDistroInt_Div[18] },
                DataLabels = true
            }, 
            //plat 1
            new PieSeries
            {
                Title = "Plat 1",
                Values = new ChartValues<int> { PlayerDistroInt_Div[19] },
                DataLabels = true
            }, 
            //diamond 5
            new PieSeries
            {
                Title = "Diamond 5",
                Values = new ChartValues<int> { PlayerDistroInt_Div[20] },
                DataLabels = true
            }, 
            //diamond 4
            new PieSeries
            {
                Title = "Diamond 4",
                Values = new ChartValues<int> { PlayerDistroInt_Div[21] },
                DataLabels = true
            }, 
            //diamond 3
            new PieSeries
            {
                Title = "Diamond 3",
                Values = new ChartValues<int> { PlayerDistroInt_Div[22] },
                DataLabels = true
            }, 
            //diamond 2
            new PieSeries
            {
                Title = "Diamond 2",
                Values = new ChartValues<int> { PlayerDistroInt_Div[23] },
                DataLabels = true
            }, 
            //diamond 1
            new PieSeries
            {
                Title = "Diamond 1",
                Values = new ChartValues<int> { PlayerDistroInt_Div[24] },
                DataLabels = true
            }, 
            //masters
            new PieSeries
            {
                Title = "Masters",
                Values = new ChartValues<int> { PlayerDistroInt_Div[25] },
                DataLabels = true
            },
            //challenger
            new PieSeries 
            {
                Title = "Challenger",
                Values = new ChartValues<int> { PlayerDistroInt_Div[26] },
                DataLabels = true
            }
        };
        public SeriesCollection PlayerDistroChart_Div { get { return playerDistroChart_Div; } }



        public void UpdateDistro()
        {
            int[] newDistro = ManagedObject.cw_GetPlayerDistro();
            int index = 0; //for updating playerDistroInt

            //update playerDistroInt_Tier, _Div, Total
            //update playerDistroChart_Tier, _Div
            for (int i = 0; i < playerDistroInt_Div.Length; ++i)
            {
                playerDistroInt_Div[i] = newDistro[index];

                ++index;
            }
            for (int i = 0; i < playerDistroInt_Tier.Length; ++i)
            {
                playerDistroInt_Tier[i] = newDistro[index];

                ++index;
            }
            playerDistroInt_Total = newDistro[index];

            //update playerDistroString
            double perc = 0D;   
            for (int i = 0; i < newDistro.Length - 1; ++i)
            {
               
                perc = (double)newDistro[i] / (double)newDistro[34] * 100;
                playerDistroString[i] = String.Format("{0,-14} {1,-13} {2, -10:00.00}\n", titleDistroStrings[i], newDistro[i], perc);
            }

            playerDistroString[34] = String.Format("{0,-14} {1,-13} {2, -10:00.00}\n", titleDistroStrings[34], newDistro[34], 100);

            //call OnPropertyChanged events
            OnPropertyChanged("PlayerDistroString");
            OnPropertyChanged("PlayerDistroInt_Tier");
            OnPropertyChanged("PlayerDistroInt_Div");
            OnPropertyChanged("PlayerDistroInt_Total");
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
