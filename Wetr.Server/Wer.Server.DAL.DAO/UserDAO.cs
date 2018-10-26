﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wetr.Server.Common;
using Wetr.Server.DAL.IDAO;
using Wetr.Server.DTO;
using static Wetr.Server.Common.ADOTemplate;

namespace Wer.Server.DAL.DAO
{
    public class UserDAO : IUserDAO
    {
        private ADOTemplate template;
        private RowMapper<User> userMapper = record =>
        {
            return new User()
            {
                ID = (int)record["ID"],
                FirstName = (string)record["FirstName"],
                LastName = (string)record["LastName"]
            };
        };

        public UserDAO(IConnectionFactory connectionFactory)
        {
            this.template = new ADOTemplate(connectionFactory);
        }
        public IEnumerable<User> FindAll()
         => template.Query<User>("SELECT * FROM User", userMapper);

        public User FindById(int id)
         => template.Query<User>("Select * FROM User WHERE ID = @ID", 
                                 userMapper, 
                                 new SqlParameter[] { new SqlParameter("ID", id) }
                                ).SingleOrDefault();
    }
}