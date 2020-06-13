using System;
using System.IO;
using System.Reflection;
using log4net;
using log4net.Config;

namespace Extract
{
    class Program
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);

        static void Main()
        {
            try
            {
                var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
                XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
                CelsiusTransactionExport.Run();
            }
            catch (Exception e)
            {
                Log.Error("Unhandled exception.");
                Log.Error(e);
                throw;
            }
        }
    }
}