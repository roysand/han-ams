using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public enum ObisCodeId
    {
        PowerUsed = 1,
        MeterId,
        MeterType,
        ClockAndDate,
        HourlyActiveImportEnergy,
        HourlyActiveExportEnergy,
        HourlyReactiveImportEnergy,
        HourlyReactiveExportEnergy,
        UL1PhaseVoltage,
        UL2PhaseVoltage,
        UL3PhaseVoltage,
        IL1Current,
        IL2Current,
        IL3Current
    }
    public class Detail
    {
        public Guid Id { get; set; }
        public Guid MeasurementId { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Location { get; set; }
        public string Name { get; set; }
        public ObisCodeId ObisCodeId { get; set; }
        public string ObisCode { get; set; }
        public string Unit { get; set; }
        public string ValueStr { get; set; }
        public decimal ValueNum { get; set; }

        public Detail()
        {
            Id = Guid.NewGuid();
            ObisCodeId = ObisCodeId.PowerUsed;
        }
    }
}
