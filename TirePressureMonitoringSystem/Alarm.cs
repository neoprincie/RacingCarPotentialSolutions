using System;

namespace TDDMicroExercises.TirePressureMonitoringSystem
{
    public class Alarm
    {
        private const double LowPressureThreshold = 17;
        private const double HighPressureThreshold = 21;

        private readonly ISensor _sensor;

        [Obsolete("Replace with IoC container")]
        public Alarm() : this(new Sensor())
        {
        }

        public Alarm(ISensor sensor)
        {
            _sensor = sensor;
        }

        public void Check()
        {
            var psiPressureValue = PopNextPressurePsiValue();

            if (psiPressureValue is < LowPressureThreshold or > HighPressureThreshold)
            {
                AlarmOn = true;
            }
        }

        private double PopNextPressurePsiValue()
        {
            return _sensor.PopNextPressurePsiValue();
        }

        public bool AlarmOn { get; private set; }
    }
}
