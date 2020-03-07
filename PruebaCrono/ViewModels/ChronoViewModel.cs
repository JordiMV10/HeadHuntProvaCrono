using PruebaCrono.Lib;
using System;
using System.Diagnostics;
using System.Windows.Input;
using System.Windows.Threading;

namespace PruebaCrono.ViewModel
{
    public class ChronoViewModel : ViewModelBase
    {
        readonly DispatcherTimer dispatcherTimer = new DispatcherTimer();
        readonly Stopwatch stopwatch = new Stopwatch();

        public ChronoViewModel()
        {
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();

            StartChronoCommand = new RouteCommand(StartChrono);
            StopChronoCommand = new RouteCommand(StopChrono);
            PauseChronoCommand = new RouteCommand(PauseChrono);
        }


        public ICommand StartChronoCommand { get; set; }
        public ICommand StopChronoCommand { get; set; }
        public ICommand PauseChronoCommand { get; set; }


        private string _hourVM;
        public string HourVM
        {
            get { return _hourVM; }
            set
            {
                _hourVM = value;
                OnPropertyChanged();
            }
        }

        private string _minuteVM;
        public string MinuteVM
        {
            get { return _minuteVM; }
            set
            {
                _minuteVM = value;
                OnPropertyChanged();
            }
        }

        private string _secondVM;
        public string SecondVM
        {
            get { return _secondVM; }
            set
            {
                _secondVM = value;
                OnPropertyChanged();
            }
        }


        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            TimeSpan timeSpan = new TimeSpan(0, 0, (int)stopwatch.Elapsed.TotalSeconds);

            HourVM = timeSpan.Hours.ToString();
            MinuteVM = timeSpan.Minutes.ToString();
            SecondVM = timeSpan.Seconds.ToString();

            if (HourVM.Length < 2)
            {
                HourVM = "0" + timeSpan.Hours.ToString();
            }
            else
            {
                HourVM = timeSpan.Hours.ToString();
            }

            if (MinuteVM.Length < 2)
            {
                MinuteVM = "0" + timeSpan.Minutes.ToString();
            }
            else
            {
                MinuteVM = timeSpan.Minutes.ToString();
            }

            if (SecondVM.Length < 2)
            {
                SecondVM = "0" + timeSpan.Seconds.ToString();
            }
            else
            {
                SecondVM = timeSpan.Seconds.ToString();
            }

        }



        public void StartChrono()
        {
            stopwatch.Start();
        }

        public void StopChrono()
        {
            stopwatch.Reset();
            HourVM = "00";
            MinuteVM = "00";
            SecondVM = "00";
        }

        public void PauseChrono()
        {
            stopwatch.Stop();
        }
    }
}
