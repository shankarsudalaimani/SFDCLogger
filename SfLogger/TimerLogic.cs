using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Timers;
namespace SfLogger
{
    class TimerLogic
    {
        public static void SetTimer()
        {

            foreach (MainWindow window in System.Windows.Application.Current.Windows)
            {

                if ((window).TimerBtn.Content.ToString() == "Start Timer")
                {
                    (window).TimerBtn.Content = "Stop Timer";
                    (window).TimerBtn.Background = Brushes.LightGreen;

                    (window).refreshTimer.Interval = new TimeSpan(0, 0, int.Parse((window).refTime.Text));
                    (window).refreshTimer.Start();
                }
                else
                {
                    (window).TimerBtn.Content = "Start Timer"; var bc = new BrushConverter();
                    (window).TimerBtn.Background = (Brush)bc.ConvertFrom("#FF292929");
                    (window).refreshTimer.Stop();

                }
            }

        }

    }
}
