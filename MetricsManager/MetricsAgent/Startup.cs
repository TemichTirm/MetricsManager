using AutoMapper;
using Dapper;
using FluentMigrator.Runner;
using MetricsAgent.DTO;
using MetricsAgent.DTO.SQL_Settings;
using MetricsAgent.Jobs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;
using System.Data.SQLite;

namespace MetricsAgent
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private const string ConnectionString = @"Data Source = metrics.db; Version = 3;";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            //ConfigureSqlLiteConnection(services);
            //services.AddHostedService<QuartzHostedService>();
            services.AddSingleton(new SQLiteConnection(ConnectionString));
            services.AddSingleton<ISQLiteConfig, SQLiteConfig>();
            services.AddSingleton<ICpuMetricsRepository, CpuMetricsRepository>();
            services.AddSingleton<IDotNetMetricsRepository, DotNetMetricsRepository>();
            services.AddSingleton<IHddMetricsRepository, HddMetricsRepository>();
            services.AddSingleton<INetworkMetricsRepository, NetworkMetricsRepository>();
            services.AddSingleton<IRamMetricsRepository, RamMetricsRepository>();
            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            services.AddSingleton<CpuMetricJob>();
            services.AddSingleton(new JobSchedule(
                                  jobType: typeof(CpuMetricJob),
                                  cronExpression: "0/5 * * * * ?")); // запускать каждые 5 секунд
            services.AddSingleton<DotNetMetricJob>();
            services.AddSingleton(new JobSchedule(
                                  jobType: typeof(DotNetMetricJob),
                                  cronExpression: "0/5 * * * * ?"));
            services.AddSingleton<HddMetricJob>();
            services.AddSingleton(new JobSchedule(
                                  jobType: typeof(HddMetricJob),
                                  cronExpression: "0/5 * * * * ?"));
            services.AddSingleton<NetworkMetricJob>();
            services.AddSingleton(new JobSchedule(
                                  jobType: typeof(NetworkMetricJob),
                                  cronExpression: "0/5 * * * * ?"));
            services.AddSingleton<RamMetricJob>();
            services.AddSingleton(new JobSchedule(
                                  jobType: typeof(RamMetricJob),
                                  cronExpression: "0/5 * * * * ?")); 
            services.AddHostedService<QuartzHostedService>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MetricsAgent", Version = "v1" });
            });
            var mapperConfiguration = new MapperConfiguration(mp => mp.AddProfile(new MapperProfile()));
            var mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);
            services.AddFluentMigratorCore().ConfigureRunner(rb => rb
                                            // добавляем поддержку SQLite
                                            .AddSQLite()
                                            // устанавливаем строку подключения
                                            .WithGlobalConnectionString(ConnectionString)
                                            // подсказываем где искать классы с миграциями
                                            .ScanIn(typeof(Startup).Assembly).For.Migrations()).AddLogging(lb => lb
                                            .AddFluentMigratorConsole());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //private void ConfigureSqlLiteConnection(IServiceCollection services)
        //{
        //    string connectionString = @"Data Source = metrics.db; Version = 3; Pooling = True; Max Pool Size = 100; ";
        //    var connection = new SQLiteConnection(connectionString);
        //    connection.Open();
        //    PrepareSchema(connection);
        //    services.AddSingleton(connection);
        //}

        //private void PrepareSchema(SQLiteConnection connection)
        //{
        //    // Создаем таблицы в базе данных
        //    using (var createTableConnection = new SQLiteConnection(connection))
        //    {
        //        // Создаем таблицус метриками CPU
        //        createTableConnection.Execute("DROP TABLE IF EXISTS cpumetrics");
        //        createTableConnection.Execute("CREATE TABLE cpumetrics(id INTEGER PRIMARY KEY, value INT, time INT)");

        //        // Создаем таблицу с метриками .Net
        //        createTableConnection.Execute("DROP TABLE IF EXISTS dotnetmetrics");
        //        createTableConnection.Execute("CREATE TABLE dotnetmetrics(id INTEGER PRIMARY KEY, value INT, time INT)");

        //        // Создаем таблицу с метриками HDD
        //        createTableConnection.Execute("DROP TABLE IF EXISTS hddmetrics");
        //        createTableConnection.Execute("CREATE TABLE hddmetrics(id INTEGER PRIMARY KEY, value INT, time INT)");

        //        // Создаем таблицу с метриками Network
        //        createTableConnection.Execute("DROP TABLE IF EXISTS networkmetrics");
        //        createTableConnection.Execute("CREATE TABLE networkmetrics(id INTEGER PRIMARY KEY, value INT, time INT)");

        //        // Создаем таблицу с метриками RAM
        //        createTableConnection.Execute("DROP TABLE IF EXISTS rammetrics");
        //        createTableConnection.Execute("CREATE TABLE rammetrics(id INTEGER PRIMARY KEY, value INT, time INT)");
        //    }

        //    using (var fillTablesConnection = new SQLiteConnection(connection))
        //    {
        //        // Заполняем БД фейковыми данными для отработки дальнейших запросов
        //        for (int i = 1; i <= 10; i++)
        //        {

        //            // Для таблицы cpumetrics
        //            long newTime = new DateTimeOffset(new DateTime(2020, 05, i + 10), TimeSpan.FromHours(0)).ToUnixTimeSeconds();
        //            int newValue = i * 10;
        //            fillTablesConnection.Execute("INSERT INTO cpumetrics(value, time) VALUES(@value, @time)"
        //                                        , new { value = newValue, time = newTime });

        //            // Для таблицы dotnetmetrics
        //            newTime = new DateTimeOffset(new DateTime(2020, 06, i + 2), TimeSpan.FromHours(0)).ToUnixTimeSeconds();
        //            newValue = i * 2;
        //            fillTablesConnection.Execute("INSERT INTO dotnetmetrics(value, time) VALUES(@value, @time)"
        //                                        , new { value = newValue, time = newTime });

        //            // Для таблицы hddmetrics
        //            newTime = new DateTimeOffset(new DateTime(2020, 07, i + 5), TimeSpan.FromHours(0)).ToUnixTimeSeconds();
        //            newValue = i * 5;
        //            fillTablesConnection.Execute("INSERT INTO hddmetrics(value, time) VALUES(@value, @time)"
        //                                        , new { value = newValue, time = newTime });

        //            // Для таблицы networkmetrics
        //            newTime = new DateTimeOffset(new DateTime(2020, 08, i * 2), TimeSpan.FromHours(0)).ToUnixTimeSeconds();
        //            newValue = i + 5 * i;
        //            fillTablesConnection.Execute("INSERT INTO networkmetrics(value, time) VALUES(@value, @time)"
        //                                        , new { value = newValue, time = newTime });

        //            // Для таблицы rammetrics
        //            newTime = new DateTimeOffset(new DateTime(2020, 09, i * 3), TimeSpan.FromHours(0)).ToUnixTimeSeconds();
        //            newValue = i + 3 * i;
        //            fillTablesConnection.Execute("INSERT INTO rammetrics(value, time) VALUES(@value, @time)"
        //                                        , new { value = newValue, time = newTime });
        //        }
        //    }
        //}
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMigrationRunner migrationRunner)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MetricsAgent v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            // запускаем миграции
            migrationRunner.MigrateUp();
        }

    }
}
