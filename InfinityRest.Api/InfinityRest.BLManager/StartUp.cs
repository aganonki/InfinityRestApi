using AutoMapper;
using InfinityRest.BLManager.Entities;
using InfinityRest.BLManager.Interfaces;
using InfinityRest.BLManager.Services;
using InfinityRest.Data.Data;
using InfinityRest.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InfinityRest.BLManager
{
    public static class Startup
    {
        public static void Configuration(IConfiguration config)
        {
            Mapper.Initialize(CreateMappings);
            Mapper.AssertConfigurationIsValid();
        }

        private static void CreateMappings(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Run, RunEntity>();
            cfg.CreateMap<Task, TaskEntity>()
                    .ForMember(x => x.TaskType, m => m.MapFrom(a => a.TaskTypeId))
                    .ForMember(x => x.ProcessStateId, m => m.MapFrom(a => a.ProcessStateId));

            cfg.CreateMap<TaskEntity, Task>()
                    .ForMember(x => x.TaskTypeId, m => m.MapFrom(y => y.TaskType))
                    .ForMember(x => x.ProcessStateId, m => m.MapFrom(a => a.ProcessStateId))
                    .ForMember(a => a.ProcessStateLink, opts => opts.Ignore())
                    .ForMember(a => a.TaskTypeLink, opts => opts.Ignore())
                    .ForMember(a => a.RunId, opts => opts.Ignore()); 

            cfg.CreateMap<TaskSettings, TaskSettingsEntity>()
                    
                    .ForMember(x => x.RunType, m => m.MapFrom(a => a.TypeId))
                    .ReverseMap()
                    .ForMember(a => a.RunType, opts => opts.Ignore());

            
            cfg.CreateMap<TaskRunType, TaskRunTypeEntity>();
            cfg.CreateMissingTypeMaps = true;
        }

        public static void ConfigureServices(IServiceCollection services, IConfiguration config)
        {

            services.AddDbContext<InfinityDB>(options => options.UseSqlServer(config.GetSection("Connection").Value));
            services.BuildServiceProvider().GetService<InfinityDB>().Database.Migrate();
            services.AddScoped<UnitOfWork>();
            services.AddScoped<IService<TaskEntity>>((ctx) =>
            {
                var svc = ctx.GetService<UnitOfWork>();
                return new GenericService<IRepository<Task>,TaskEntity,Task>(svc, svc.TaskRepository);
            });

            services.AddScoped<IService<RunEntity>>((ctx) =>
            {
                var svc = ctx.GetService<UnitOfWork>();
                return new GenericService<IRepository<Run>, RunEntity, Run>(svc, svc.RunRepository);
            });
        }
    }
}
