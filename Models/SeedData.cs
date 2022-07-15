using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Platform.Models
{
	public class SeedData
	{
		private CalculationContext context;
		private ILogger<SeedData> logger;

		private static Dictionary<int, long> data = new Dictionary<int, long>()
		{
			{1,1 },{2,3},{3,6},{4,10},{5,15},
			{6,21 },{7,28},{8,36},{9,45},{10,55}
		};

        public SeedData(CalculationContext ctx, ILogger<SeedData> log)
        {
			context = ctx;
			logger = log;
        }

		public void SeedDataBase()
        {
			context.Database.Migrate();
			if(context.Calculations.Count() == 0)
            {
				logger.LogInformation("preparing to seed database");
				context.Calculations!.AddRange(data.Select(ctx => new Calculation()
				{
					Count = ctx.Key,
					Result = ctx.Value
				}));
				context.SaveChanges();
				logger.LogInformation("Database seed");
            }
            else
            {
				logger.LogInformation("Database not seed");
            }
        }
	}
}

