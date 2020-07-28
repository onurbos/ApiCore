using ApiCore.Context;
using ApiCore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using ApiCore.Helpers;

namespace ApiCore.Repositories
{
    public abstract class RepositoryBase : IRepositoryBase, IDisposable
    {
        private readonly AppDBContext _dbContext;
        private readonly IConfiguration _configuration;

        public int? UserId { get; set; }

        public CurrentUser CurrentUser { get; set; }

        public RepositoryBase(AppDBContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public AppDBContext DbContext
        {
            get
            {
                var dbConnection = _dbContext.Database.GetDbConnection();
                var connectionString = _configuration.GetConnectionString("DBConnection");

                if (dbConnection.State != System.Data.ConnectionState.Open)
                    dbConnection.ConnectionString = connectionString;

                _dbContext.UserId = CurrentUser?.UserId;

                return _dbContext;
            }
        }

        public void Dispose()
        {
        }
    }
}
