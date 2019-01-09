using AutoMapper;
using System;
using System.Linq;
using System.Reflection;

namespace TVmaze
{
    public static class AutoMapperConfiguration
    {
        public static void Configure()
        {
            var assembliesToScan = AppDomain.CurrentDomain.GetAssemblies();
            var allTypes = assembliesToScan.Where(a => a.FullName.Contains("TVmaze")).SelectMany(a => a.ExportedTypes).ToArray();

            var profiles =
                allTypes
                    .Where(t => typeof(Profile).GetTypeInfo().IsAssignableFrom(t.GetTypeInfo()))
                    .Where(t => !t.GetTypeInfo().IsAbstract);

            Mapper.Initialize(cfg =>
            {
                foreach (var profile in profiles)
                {
                    cfg.AddProfile(profile);
                }
            });
        }
    }
}
