using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibreHardwareMonitor.Hardware;

namespace AuraCpuSync
{
    class TempReader
    {
        private readonly Computer computer;
        private readonly string sensorName;

        public TempReader(string sensorName)
        {
            computer = new Computer
            {
                IsCpuEnabled = true,
                IsGpuEnabled = false,
                IsMemoryEnabled = false,
                IsMotherboardEnabled = false,
                IsControllerEnabled = false,
                IsNetworkEnabled = false,
                IsStorageEnabled = false
            };

            this.sensorName = sensorName;
            computer.Open();
        }

        public float? ReadTemp()
        {
            computer.Accept(new UpdateVisitor());

            foreach (ISensor sensor in computer.Hardware[0].Sensors)
            {
                // TODO: Add a config for this.
                if (sensor.Name == sensorName)
                {
                    return sensor.Value;
                }
            }

            return 0;
        }
    }

    public class UpdateVisitor : IVisitor
    {
        public void VisitComputer(IComputer computer)
        {
            computer.Traverse(this);
        }
        public void VisitHardware(IHardware hardware)
        {
            hardware.Update();
        }
        public void VisitSensor(ISensor sensor) { }
        public void VisitParameter(IParameter parameter) { }
    }
}
