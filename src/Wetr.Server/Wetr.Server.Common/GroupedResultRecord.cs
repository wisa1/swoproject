using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wetr.Server.DAL.DTO;
using static Wetr.Server.Common.ADOTemplate;
using static Wetr.Server.Common.Constants;


namespace Wetr.Server.Common
{
    public class GroupedResultRecord
    {
        public static RowMapper<GroupedResultRecord> groupResultMapper = record =>
        {
            GroupedResultRecord result = new GroupedResultRecord();
            for (int i = 0; i < record.FieldCount; i++)
            {
                switch (record.GetName(i))
                {
                    case "Timestamp":
                        result.DateTimeStart = DateTime.Parse((record["Timestamp"]).ToString());
                        break;
                    case "DateTimeStart":
                        result.DateTimeStart = DateTime.Parse((record["DateTimeStart"]).ToString());
                        break;
                    case "Value":
                        result.Value = (double)record["Value"];
                        break;
                    case "TypeID":
                        result.MeasurementTypeID = (int)record["TypeID"];
                        break;
                    case "DeviceID":
                        result.DeviceID = (int)record["DeviceID"];
                        break;
                    case "Unit of Measure ID":
                        result.UnitOfMeasureID = (int)record["Unit of Measure ID"];
                        break;
                }   
            }
            return result;
        };

        public DateTime DateTimeStart { set; get; }
        public PeriodType PeriodType { get; set; }

        public int MeasurementTypeID { get; set; }
        public int DeviceID { set; get; }
        public int UnitOfMeasureID { set; get; }
        public double Value { get; set; }
    }
}
