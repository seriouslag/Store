using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Store.Data
{
    public class StoreDbInitializer : DropCreateDatabaseAlways<StoreDbContext>
    {
        protected override void Seed(StoreDbContext context)
        {
            base.Seed(context);
        }
    }
}