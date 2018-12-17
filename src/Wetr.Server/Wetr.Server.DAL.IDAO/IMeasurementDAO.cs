using System.Collections.Generic;
using System.Threading.Tasks;
using Wetr.Server.Common;
using Wetr.Server.DAL.DTO;
using static Wetr.Server.Common.Constants;

namespace Wetr.Server.DAL.IDAO
{
    public interface IMeasurementDAO
    {
        Task<IEnumerable<Measurement>> FindAllAsync();
        Task<Measurement> FindByIDAsync(int id);
        Task<int> InsertAsync(Measurement measurement);
        Task<IEnumerable<GroupedResultRecord>> GetAggregatedDataForDevice(MeasurementDevice measurementDevice, AggregationType aggregationType, MeasurementType measurementType, PeriodType periodType);
        Task<IEnumerable<GroupedResultRecord>> GetCumulatedDataForDevice(MeasurementDevice measurementDevice, AggregationType aggregationType, MeasurementType measurementType, PeriodType periodType);

        //region stuff?
        //Task<IEnumerable<GroupedResultRecord>> GetCumulatedDataForDevice(MeasurementDevice measurementDevice, AggregationType aggregationType, MeasurementType measurementType, PeriodType periodType = PeriodType.None);
        //Task<IEnumerable<GroupedResultRecord>> GetCumulatedDataForDevice(MeasurementDevice measurementDevice, AggregationType aggregationType, MeasurementType measurementType, PeriodType periodType = PeriodType.None);
    }
}
