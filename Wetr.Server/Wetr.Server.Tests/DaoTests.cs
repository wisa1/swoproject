using System.Collections.Generic;
using Wetr.Server.Common;
using Wetr.Server.DAL.IDAO;
using Wetr.Server.DTO;
using Xunit;
using Wetr.Server.DAL.DAO;
using System.Transactions;
using System.Linq;
using System.Data.SqlClient;
using Wetr.Server.DAL.DTO;
using System;

namespace Wetr.Server.Tests
{
    public class DaoTests
    {
        private IConnectionFactory connFac;
        public DaoTests()
        {
            connFac = ConnectionFactory.CreateFromConfiguration("WeatherTracer");
        }

        [Fact]
        public void TestConnection()
        {
            Assert.True(connFac != null);
        }

        [Fact]
        public async void TestUserDAO()
        {
            IUserDAO userDAO = new UserDAO(connFac);
            //get IEnumerable of all users initially
            IEnumerable<User> allUsers = await userDAO.FindAllAsync();
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                //Create and insert new User
                User testUser = new User() { LastName = "SomeTest", FirstName = "User" };
                bool inserted = (await userDAO.InsertAsync(testUser)) == 1;

                //check if insertion was successful
                Assert.True(inserted);

                //check new overall record count
                IEnumerable<User> allUsersNew = await userDAO.FindAllAsync();
                Assert.Equal<int>(allUsers.Count() + 1, allUsersNew.Count());
                User newUser = allUsersNew.Last<User>();

                //Test that the inserted user actually has the given properties
                Assert.Equal(newUser.FirstName, testUser.FirstName);
                Assert.Equal(newUser.LastName, testUser.LastName);

                //Test for bad Inserts
                await Assert.ThrowsAsync<SqlException>(async () => await userDAO.InsertAsync(new User() { FirstName = "NoLastName" }));
                await Assert.ThrowsAsync<SqlException>(async () => await userDAO.InsertAsync(new User() { LastName = "NoFirstName" }));
            }
        }

        [Fact]
        public async void TestUnitOfMeasureDAO()
        {
            IUnitOfMeasureDAO uomDAO = new UnitOfMeasureDAO(connFac);
            //get IEnumerable of all users initially
            IEnumerable<UnitOfMeasure> allUom = await uomDAO.FindAllAsync();
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                //Create and insert new UnitOfMeasure
                UnitOfMeasure testUnitOfMeasure = new UnitOfMeasure() { Abbreviation = "SomeTest", Description = "UnitOfMeasure" };
                bool inserted = (await uomDAO.InsertAsync(testUnitOfMeasure)) == 1;

                //check if insertion was successful
                Assert.True(inserted);

                //check new overall record count
                IEnumerable<UnitOfMeasure> allUnitOfMeasuresNew = await uomDAO.FindAllAsync();
                Assert.Equal<int>(allUom.Count() + 1, allUnitOfMeasuresNew.Count());
                UnitOfMeasure newUnitOfMeasure = allUnitOfMeasuresNew.Last<UnitOfMeasure>();

                //Test that the inserted UnitOfMeasure actually has the given properties
                Assert.Equal(newUnitOfMeasure.Abbreviation, testUnitOfMeasure.Abbreviation);
                Assert.Equal(newUnitOfMeasure.Description, testUnitOfMeasure.Description);
            }
        }

        [Fact]
        public async void TestProvinceDAO()
        {
            IProvinceDAO provinceDAO = new ProvinceDAO(connFac);
            //get IEnumerable of all users initially
            IEnumerable<Province> allProvinces = await provinceDAO.FindAllAsync();
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                //Create and insert new Province
                Province testProvince = new Province() { Name = "SomeProvince"};
                bool inserted = (await provinceDAO.InsertAsync(testProvince)) == 1;

                //check if insertion was successful
                Assert.True(inserted);

                //check new overall record count
                IEnumerable<Province> allProvincesNew = await provinceDAO.FindAllAsync();
                Assert.Equal<int>(allProvinces.Count() + 1, allProvincesNew.Count());
                Province newProvince = allProvincesNew.Last<Province>();

                //Test that the inserted Province actually has the given properties
                Assert.Equal(newProvince.Name, testProvince.Name);
                
            }
        }

        [Fact]
        public async void TestMeasurementTypeDAO()
        {
            IMeasurementTypeDAO measurementTypeDAO = new MeasurementTypeDAO(connFac);
            //get IEnumerable of all users initially
            IEnumerable<MeasurementType> allMeasurementTypes = await measurementTypeDAO.FindAllAsync();
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                //Create and insert new MeasurementType
                MeasurementType testMeasurementType = new MeasurementType() { Description = "SomeMeasurementType" };
                bool inserted = (await measurementTypeDAO.InsertAsync(testMeasurementType)) == 1;

                //check if insertion was successful
                Assert.True(inserted);

                //check new overall record count
                IEnumerable<MeasurementType> allMeasurementTypesNew = await measurementTypeDAO.FindAllAsync();
                Assert.Equal<int>(allMeasurementTypes.Count() + 1, allMeasurementTypesNew.Count());
                MeasurementType newMeasurementType = allMeasurementTypesNew.Last<MeasurementType>();

                //Test that the inserted MeasurementType actually has the given properties
                Assert.Equal(newMeasurementType.Description, testMeasurementType.Description);

            }
        }

        [Fact]
        public async void TestMeasurementDeviceDAO()
        {
            IMeasurementDeviceDAO measurementDeviceDAO = new MeasurementDeviceDAO(connFac);
            //get IEnumerable of all users initially
            IEnumerable<MeasurementDevice> allMeasurementDevices = await measurementDeviceDAO.FindAllAsync();
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                //Create and insert new MeasurementDevice
                MeasurementDevice testMeasurementDevice = new MeasurementDevice() { 
                    Address = "SomeAddress 1",
                    CommunityID = 1,
                    DeviceName = "MyDevice",
                    Latitude = 15.345234,
                    Longitude = 12.123123
                };

                bool inserted = (await measurementDeviceDAO.InsertAsync(testMeasurementDevice)) == 1;

                //check if insertion was successful
                Assert.True(inserted);

                //check new overall record count
                IEnumerable<MeasurementDevice> allMeasurementDevicesNew = await measurementDeviceDAO.FindAllAsync();
                Assert.Equal<int>(allMeasurementDevices.Count() + 1, allMeasurementDevicesNew.Count());
                MeasurementDevice newMeasurementDevice = allMeasurementDevicesNew.Last<MeasurementDevice>();

                //Test that the inserted MeasurementDevice actually has the given properties
                Assert.Equal(newMeasurementDevice.Address, testMeasurementDevice.Address);
                Assert.Equal(newMeasurementDevice.CommunityID, testMeasurementDevice.CommunityID);
                Assert.Equal(newMeasurementDevice.DeviceName, testMeasurementDevice.DeviceName);
                Assert.Equal(newMeasurementDevice.Longitude, testMeasurementDevice.Longitude);
                Assert.Equal(newMeasurementDevice.Latitude, testMeasurementDevice.Latitude);
            }
        }

        [Fact]
        public async void TestMeasurementDAO()
        {
            IMeasurementDAO measurementDAO = new MeasurementDAO(connFac);
            //get IEnumerable of all users initially
            IEnumerable<Measurement> allMeasurements = await measurementDAO.FindAllAsync();
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                //Create and insert new Measurement
                Measurement testMeasurement = new Measurement() {
                    DeviceID = 1,
                    Timestamp = DateTime.Now,
                    TypeID = 1,
                    UnitOfMeasureID = 1,
                    Value = 19.5
                };
                bool inserted = (await measurementDAO.InsertAsync(testMeasurement)) == 1;

                //check if insertion was successful
                Assert.True(inserted);

                //check new overall record count
                IEnumerable<Measurement> allMeasurementsNew = await measurementDAO.FindAllAsync();
                Assert.Equal<int>(allMeasurements.Count() + 1, allMeasurementsNew.Count());
                Measurement newMeasurement = allMeasurementsNew.Last<Measurement>();

                //Test that the inserted Measurement actually has the given properties
                Assert.Equal(newMeasurement.DeviceID, testMeasurement.DeviceID);
                Assert.Equal(newMeasurement.TypeID, testMeasurement.TypeID);
                Assert.Equal(newMeasurement.UnitOfMeasureID, testMeasurement.UnitOfMeasureID);
                Assert.Equal(newMeasurement.Value, testMeasurement.Value);

            }
        }

        [Fact]
        public async void TestDistrictDAO()
        {
            IProvinceDAO provinceDAO = new ProvinceDAO(connFac);
            //get IEnumerable of all users initially
            IEnumerable<Province> allProvinces = await provinceDAO.FindAllAsync();
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                //Create and insert new Province
                Province testProvince = new Province() { Name = "SomeDistrictName" };
                bool inserted = (await provinceDAO.InsertAsync(testProvince)) == 1;

                //check if insertion was successful
                Assert.True(inserted);

                //check new overall record count
                IEnumerable<Province> allProvincesNew = await provinceDAO.FindAllAsync();
                Assert.Equal<int>(allProvinces.Count() + 1, allProvincesNew.Count());
                Province newProvince = allProvincesNew.Last<Province>();

                //Test that the inserted Province actually has the given properties
                Assert.Equal(newProvince.Name, testProvince.Name);

            }
        }

        [Fact]
        public async void TestCommunityDAO()
        {
            ICommunityDAO communityDAO = new CommunityDAO(connFac);
            //get IEnumerable of all users initially
            IEnumerable<Community> allCommunitys = await communityDAO.FindAllAsync();
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                //Create and insert new Community
                Community testCommunity = new Community() {
                    Name = "SomeCommunityName",
                    DistrictID = 10,
                    PostalCode = 2110};
                bool inserted = (await communityDAO.InsertAsync(testCommunity)) == 1;

                //check if insertion was successful
                Assert.True(inserted);

                //check new overall record count
                IEnumerable<Community> allCommunitysNew = await communityDAO.FindAllAsync();
                Assert.Equal<int>(allCommunitys.Count() + 1, allCommunitysNew.Count());
                Community newCommunity = allCommunitysNew.Last<Community>();

                //Test that the inserted Community actually has the given properties
                Assert.Equal(newCommunity.Name, testCommunity.Name);
                Assert.Equal(newCommunity.PostalCode, testCommunity.PostalCode);
                Assert.Equal(newCommunity.DistrictID, testCommunity.DistrictID);
            }
        }
    }
}
