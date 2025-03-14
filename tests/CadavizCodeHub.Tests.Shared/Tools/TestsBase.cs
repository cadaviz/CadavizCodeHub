using AutoFixture;
using AutoMapper;
using CadavizCodeHub.Tests.Shared.Fixtures;
using System;

namespace CadavizCodeHub.Tests.Shared.Tools
{
    //TODO: revisar nomenclatura
    public abstract class TestsBase
    {
        protected Fixture Fixture { get; }

        protected TestsBase()
        {
            Fixture = FixtureHelper.CreateFixture();
        }

        protected static IMapper CreateMapper<T>()
            where T : Profile, new()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<T>();
            });

            return mapperConfiguration.CreateMapper();
        }

        protected static IMapper CreateMappers(params Type[] profiles)
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                foreach (var profile in profiles)
                {
                    if (typeof(Profile).IsAssignableFrom(profile))
                    {
                        cfg.AddProfile((Profile)Activator.CreateInstance(profile)!);
                    }
                }
            });

            return mapperConfiguration.CreateMapper();
        }
    }
}
