﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wetr.Server.Common;
using Wetr.Server.DAL.DTO;
using Wetr.Server.DAL.IDAO;
using static Wetr.Server.Common.ADOTemplate;

namespace Wetr.Server.DAL.DAO
{
    public class MeasurementDeviceDAO : IMeasurementDeviceDAO
    {
        private ADOTemplate template;
        private RowMapper<MeasurementDevice> measurementDeviceMapper = record =>
        {
            return new MeasurementDevice()
            {
                ID = (int)record["ID"],
                Address = (string)record["Address"],
                CommunityID = (int)record["CommunityID"],
                DeviceName = (string)record["DeviceName"],
                Latitude = (float)record["Latitude"],
                Longitude = (float)record["Longitude"]

            };
        };

        public MeasurementDeviceDAO(IConnectionFactory connectionFactory)
        {
            this.template = new ADOTemplate(connectionFactory);
        }
        public async Task<IEnumerable<MeasurementDevice>> FindAllAsync()
          => await template.QueryAsync<MeasurementDevice>("SELECT * FROM [Measurement Device]", measurementDeviceMapper);

        public async Task<MeasurementDevice> FindByIDAsync(int id)
          => (await template.QueryAsync<MeasurementDevice>("Select * FROM [Measurement Device] WHERE ID = @ID",
                                 measurementDeviceMapper,
                                 new SqlParameter[] { new SqlParameter("@ID", id) }
                                )).SingleOrDefault();

        public async Task<int> InsertAsync(MeasurementDevice measurementDevice)
          => (await template.ExecuteAsync("INSERT INTO [Measurement Device] (CommunityID, [Device Name], Address, Longitude, Latitude) " +
                                          "VALUES(@CommunityID, @DeviceName, @Address, @Longitude, @Latitude)",
                                    new SqlParameter[] { new SqlParameter("@Description", measurementDevice.CommunityID) ,
                                                         new SqlParameter("@DeviceName", measurementDevice.DeviceName),
                                                         new SqlParameter("@Address", measurementDevice.Address),
                                                         new SqlParameter("@Longitude", measurementDevice.Longitude),
                                                         new SqlParameter("@Latitude", measurementDevice.Latitude)}));
    }

}

