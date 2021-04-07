using AutoMapper;
using MetricsAgent.DTO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            ConfigureSqlLiteConnection(services);
            services.AddScoped<ICpuMetricsRepository, CpuMetricsRepository>();
            services.AddScoped<IDotNetMetricsRepository, DotNetMetricsRepository>();
            services.AddScoped<IHddMetricsRepository, HddMetricsRepository>();
            services.AddScoped<INetworkMetricsRepository, NetworkMetricsRepository>();
            services.AddScoped<IRamMetricsRepository, RamMetricsRepository>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MetricsAgent", Version = "v1" });
            });
            var mapperConfiguration = new MapperConfiguration(mp => mp.AddProfile(new MapperProfile()));
            var mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        private void ConfigureSqlLiteConnection(IServiceCollection services)
        {
            string connectionString = @"Data Source = metrics.db; Version = 3; Pooling = True; Max Pool Size = 100; ";
            var connection = new SQLiteConnection(connectionString);
            connection.Open();
            PrepareSchema(connection);
            services.AddSingleton(connection);
        }

        private void PrepareSchema(SQLiteConnection connection)
        {
            // Создаем таблицус метриками CPU
            using (var command = new SQLiteCommand(connection))
            {
                // задаем новый текст команды для выполнения
                // удаляем таблицу с метриками если она существует в базе данных
                command.CommandText = "DROP TABLE IF EXISTS cpumetrics";
                // отправляем запрос в базу данных
                command.ExecuteNonQuery();
                command.CommandText = @"CREATE TABLE cpumetrics(id INTEGER PRIMARY KEY,
                    value INT, time INT)";
                command.ExecuteNonQuery();
            }

            // Создаем таблицу с метриками .Net
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = "DROP TABLE IF EXISTS dotnetmetrics";
                command.ExecuteNonQuery();
                command.CommandText = @"CREATE TABLE dotnetmetrics(id INTEGER PRIMARY KEY,
                    value INT, time INT)";
                command.ExecuteNonQuery();
            }
            // Создаем таблицу с метриками HDD
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = "DROP TABLE IF EXISTS hddmetrics";
                command.ExecuteNonQuery();
                command.CommandText = @"CREATE TABLE hddmetrics(id INTEGER PRIMARY KEY,
                    value INT, time INT)";
                command.ExecuteNonQuery();
            }
            // Создаем таблицу с метриками Network
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = "DROP TABLE IF EXISTS networkmetrics";
                command.ExecuteNonQuery();
                command.CommandText = @"CREATE TABLE networkmetrics(id INTEGER PRIMARY KEY,
                    value INT, time INT)";
                command.ExecuteNonQuery();
            }
            // Создаем таблицу с метриками RAM
            using (var command = new SQLiteCommand(connection))
            {
                command.CommandText = "DROP TABLE IF EXISTS rammetrics";
                command.ExecuteNonQuery();
                command.CommandText = @"CREATE TABLE rammetrics(id INTEGER PRIMARY KEY,
                    value INT, time INT)";
                command.ExecuteNonQuery();
            }
            // Заполняем БД фейковыми данными для отработки дальнейших запросов
            for (int i = 1; i <= 10; i++)
            {
                using var cmd = new SQLiteCommand(connection);
                {
                    // Для таблицы cpumetrics
                    long time = new DateTimeOffset(new DateTime(2020, 05, (i+10)), TimeSpan.FromHours(0)).ToUnixTimeSeconds();
                    int value = i * 10;
                    cmd.CommandText = "INSERT INTO cpumetrics(value, time) VALUES(@value, @time)";
                    cmd.Parameters.AddWithValue("@value", value);
                    cmd.Parameters.AddWithValue("@time", time);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();

                    // Для таблицы dotnetmetrics
                    time = new DateTimeOffset(new(2020, 06, i + 2)).ToUnixTimeSeconds();
                    value = i * 2;
                    cmd.CommandText = "INSERT INTO dotnetmetrics(value, time) VALUES(@value, @time)";
                    cmd.Parameters.AddWithValue("@value", value);
                    cmd.Parameters.AddWithValue("@time", time);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();

                    // Для таблицы hddmetrics
                    time = new DateTimeOffset(new(2020, 07, i + 5)).ToUnixTimeSeconds();
                    value = i * 5;
                    cmd.CommandText = "INSERT INTO hddmetrics(value, time) VALUES(@value, @time)";
                    cmd.Parameters.AddWithValue("@value", value);
                    cmd.Parameters.AddWithValue("@time", time);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();

                    // Для таблицы networkmetrics
                    time = new DateTimeOffset(new(2020, 08, i * 2)).ToUnixTimeSeconds();
                    value = i + 5 * i;
                    cmd.CommandText = "INSERT INTO networkmetrics(value, time) VALUES(@value, @time)";
                    cmd.Parameters.AddWithValue("@value", value);
                    cmd.Parameters.AddWithValue("@time", time);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();

                    // Для таблицы rammetrics
                    time = new DateTimeOffset(new(2020, 09, i * 3)).ToUnixTimeSeconds();
                    value = i + 3 * i;
                    cmd.CommandText = "INSERT INTO rammetrics(value, time) VALUES(@value, @time)";
                    cmd.Parameters.AddWithValue("@value", value);
                    cmd.Parameters.AddWithValue("@time", time);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
        }

    }
}
