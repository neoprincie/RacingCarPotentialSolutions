using FluentAssertions;
using NSubstitute;
using Xunit;

namespace TDDMicroExercises.TirePressureMonitoringSystem.Tests
{
    public class AlarmTest
    {
        [Fact]
        public void AlarmOn_GivenNoInput_ShouldNotBeOn()
        {
            var mockSensor = Substitute.For<ISensor>();
            var alarm = new Alarm(mockSensor);
            
            alarm.AlarmOn.Should().BeFalse();
        }

        [Theory]
        [InlineData(22)] // lower bound high pressure
        [InlineData(23)]
        public void Check_GivenHighPressure_AlarmShouldBeOn(int psiPressureValue)
        {
            var mockSensor = Substitute.For<ISensor>();
            var alarm = new Alarm(mockSensor);

            mockSensor.PopNextPressurePsiValue().Returns(psiPressureValue);
            
            alarm.Check();

            alarm.AlarmOn.Should().BeTrue();
        }

        [Theory]
        [InlineData(16)] // upper bound low pressure
        [InlineData(1)]
        public void Check_GivenLowPressure_AlarmShouldBeOn(int psiPressureValue)
        {
            var mockSensor = Substitute.For<ISensor>();
            var alarm = new Alarm(mockSensor);
            
            mockSensor.PopNextPressurePsiValue().Returns(psiPressureValue);
            
            alarm.Check();

            alarm.AlarmOn.Should().BeTrue();
        }

        [Theory]
        [InlineData(17)] // at low pressure bound
        [InlineData(19)]
        [InlineData(21)] // at high pressure bound
        public void Check_GivenAcceptablePressure_AlarmShouldBeOff(int psiPressureValue)
        {
            var mockSensor = Substitute.For<ISensor>();
            var alarm = new Alarm(mockSensor);
            
            mockSensor.PopNextPressurePsiValue().Returns(psiPressureValue);
            
            alarm.Check();

            alarm.AlarmOn.Should().BeFalse();
        }
    }
}