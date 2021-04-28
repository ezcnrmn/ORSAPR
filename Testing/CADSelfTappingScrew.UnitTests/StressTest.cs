using NUnit.Framework;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using CADSelfTappingScrew;
using KompasWrapper;

namespace CADSelfTappingScrew.UnitTests
{
    /// <summary>
    /// Класс для нагрузочного тестирования.
    /// </summary>
    [TestFixture]
    public class StressTest
    {
        [TestCase(TestName = "Нагрузочный тест потребления памяти и времени построения")]
        public void Start()
        {
            var writer = new StreamWriter($@"{AppDomain.CurrentDomain.BaseDirectory}\StressTest.txt");

            var count = 200;

            var processes = Process.GetProcessesByName("KOMPAS");
            var process = processes.First();

            var ramCounter = new PerformanceCounter("Process", "Working Set - Private", process.ProcessName);
            var cpuCounter = new PerformanceCounter("Process", "% Processor Time", process.ProcessName);
            var stopwatch = new Stopwatch();

            for (int i = 0; i < count; i++)
            {
                stopwatch.Start();

                cpuCounter.NextValue();
                var selfTappingScrewParameters = new SelfTappingScrewParameters();
                selfTappingScrewParameters.HeadDiameter = SelfTappingScrewParameters.DefaultValues[ParametersName.HeadDiameter];
                selfTappingScrewParameters.HeadHight = SelfTappingScrewParameters.DefaultValues[ParametersName.HeadHight];
                selfTappingScrewParameters.RodLength = SelfTappingScrewParameters.DefaultValues[ParametersName.RodLength];
                selfTappingScrewParameters.ThreadLength = SelfTappingScrewParameters.DefaultValues[ParametersName.ThreadLength];
                selfTappingScrewParameters.ThreadDiameter = SelfTappingScrewParameters.DefaultValues[ParametersName.ThreadDiameter];
                selfTappingScrewParameters.ThreadStep = SelfTappingScrewParameters.DefaultValues[ParametersName.ThreadStep];
                selfTappingScrewParameters.RodDiameter = SelfTappingScrewParameters.DefaultValues[ParametersName.RodDiameter];
                selfTappingScrewParameters.InternalThreadDiameter = SelfTappingScrewParameters.DefaultValues[ParametersName.InternalThreadDiameter];
                selfTappingScrewParameters.Washer = false;

                var kompas3DWrapper = new Kompas3DWrapper();
                kompas3DWrapper.OpenKompas();

                SelfTappingScrewBuilder selfTappingScrewBuilder = new SelfTappingScrewBuilder();
                selfTappingScrewBuilder.BuildSelfTappingScrew(kompas3DWrapper, selfTappingScrewParameters);

                stopwatch.Stop();

                var ram = ramCounter.NextValue();
                var cpu = cpuCounter.NextValue();

                writer.Write($"{i}. ");
                writer.Write($"RAM: {Math.Round(ram / 1024 / 1024)} MB");
                writer.Write($"\tCPU: {cpu} %");
                writer.Write($"\ttime: {stopwatch.Elapsed}");
                writer.Write(Environment.NewLine);
                writer.Flush();

                stopwatch.Reset();
            }
        }
    }
}