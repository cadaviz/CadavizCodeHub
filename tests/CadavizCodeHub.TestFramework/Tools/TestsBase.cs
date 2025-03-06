﻿using AutoFixture;
using AutoMapper;
using CadavizCodeHub.TestFramework.Fixtures;
using System;

namespace CadavizCodeHub.TestFramework.Tools
{
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
