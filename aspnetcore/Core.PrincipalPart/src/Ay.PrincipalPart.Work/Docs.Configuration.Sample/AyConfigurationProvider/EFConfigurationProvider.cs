using Docs.Configuration.Sample.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docs.Configuration.Sample.AyConfigurationProvider
{
    public class EFConfigurationProvider: ConfigurationProvider
    {
        Action<DbContextOptionsBuilder> OptionsAction{ get; }


        public override void Load()
        {
            var builder = new DbContextOptionsBuilder<EFConfigurationContext>();

            OptionsAction(builder);

            //TODO：选项的理解
            using(var dbContext=new EFConfigurationContext(builder.Options)){
                
            }




            base.Load();
        }
    }
}
