using log4net;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Tweet_Api.Logger
{
    public class LogFilterAttribute : ExceptionFilterAttribute
    {
        private ILog logger;

        public LogFilterAttribute()
        {
            logger = LogManager.GetLogger(typeof(LogFilterAttribute));
        }

        public override void OnException(ExceptionContext Context)
        {
            logger.Error(Context.Exception.Message + " - " + Context.Exception.StackTrace);
        }
    }
}