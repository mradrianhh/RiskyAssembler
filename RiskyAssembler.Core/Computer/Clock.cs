using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiskyAssembler.Core.Computer
{
    public delegate void TickHandler();

    public class Clock : IClock
    {
        private double _frequency;
        public double Frequency
        {
            get => _frequency;
            set
            {
                _frequency = value;
                CalculateTickTime(value);
            }
        }

        private double _tickTime;
        public double TickTime
        {
            get => _tickTime;
            set
            {
                _tickTime = value;
                CalculateFrequency(value);
            }
        }

        public event TickHandler Ticked;

        private bool _ticking = true;

        public Clock(int frequency)
        {
            Frequency = frequency;
        }

        public void Tick()
        {
            while (_ticking)
            {
                System.Threading.Thread.Sleep((int)_tickTime);
                OnTicked();
            }
        }

        private void OnTicked()
        {
            Ticked?.Invoke();
        }

        public void Start()
        {
            _ticking = true;
            Tick();
        }

        public void Start(int frequency)
        {
            Frequency = frequency;
            _ticking = true;
            Tick();
        }

        public void Stop()
        {
            _ticking = false;
        }

        private void CalculateFrequency(double tickTime)
        {
            _frequency = 1 / (tickTime / 1000);
        }

        private void CalculateTickTime(double frequency)
        {
            _tickTime = (1 / frequency) * 1000;
        }

    }
}
