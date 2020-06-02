using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_Service.Models;

namespace Web_Service.Services
{
    public class DbRepository : DbContext
    {
        public DbRepository() { }
        public DbRepository(DbContextOptions<DbRepository> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<ErrorViewModel>(xx => xx.HasNoKey());
                
        }

        //Get all records from view
        public async Task<List<Models.Task>> SelectTasksAsync()
        {

            string sqlQuery = "Select * from v_task";

            var items = await Set<Models.Task>().FromSqlRaw(sqlQuery).ToListAsync().ConfigureAwait(false);

            return items;
        }

        //Query builder
        public async Task<List<Models.Task>> QueryBuilderAsync(string q)
        {

            string sqlQuery = $"select * from v_task where {q}";

            var items = await Set<Models.Task>().FromSqlRaw(sqlQuery).ToListAsync().ConfigureAwait(false);

            return items;
        }

        public DbSet<Web_Service.Models.Task> Task { get; set; }

        public DbSet<Web_Service.Models.Query> Query { get; set; }

    }
}
