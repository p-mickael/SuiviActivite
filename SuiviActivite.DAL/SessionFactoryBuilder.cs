using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using SuiviActivite.DAL.AutoMappingOverrides;
using SuiviActivite.DAL.Conventions;
using SuiviActivite.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuiviActivite.DAL
{
    public static class SessionFactoryBuilder
    {
        public static ISessionFactory BuildSessionFactory()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            return Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008.ConnectionString(connectionString))
                .Mappings(m => m.AutoMappings.Add(AutoMap.AssemblyOf<User>(new AutoMapConfiguration())
                    .Conventions.Add(new AutoMapConvention(), new AutoMapForeignKeyConvention())
                    .UseOverridesFromAssemblyOf<UserOverride>()))
                .BuildSessionFactory();
        }
    }
}
