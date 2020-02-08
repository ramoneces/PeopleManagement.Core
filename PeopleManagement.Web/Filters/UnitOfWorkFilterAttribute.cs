using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PeopleManagement.Data;
using PeopleManagement.Data.Context;

namespace PeopleManagement.Web.Filters
{
    public class UnitOfWorkFilterAttribute : ActionFilterAttribute
    {
        private readonly ApplicationDbContext _dbContext;

        public UnitOfWorkFilterAttribute(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _dbContext.Database.BeginTransaction();
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception == null)
            {
                try
                {
                    _dbContext.SaveChanges();
                    _dbContext.Database.CommitTransaction();
                }
                catch (Exception ex)
                {
                    context.Exception = ex;
                    context.Result = null;
                }
            }
        }

    }
}
