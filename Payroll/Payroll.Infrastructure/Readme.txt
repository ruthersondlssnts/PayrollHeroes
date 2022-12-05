 protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseLazyLoadingProxies();
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("PayrollDBEntities"));
                //optionsBuilder.UseSqlServer(Startup.Configuration.Get("Data:DefaultConnection:ConnectionString"));
            }
        }



Scaffold-DbContext "Server=.\SQLEXPRESS;Database=PayrollDB;user id=sa;password=masterkey" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -UseDatabaseNames

FORCE DELETE DATA
DELETE FROM tablename
DBCC CHECKIDENT ('tablename',RESEED, 0)

