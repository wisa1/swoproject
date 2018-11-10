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
    }
}
