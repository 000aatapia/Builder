using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Services
{
    public static class LoggingService
    {
        public static void LogInfo(string message)
        {
            Console.WriteLine($"[INFO] {DateTime.Now:HH:mm:ss} - {message}");
        }

        public static void LogError(string message)
        {
            Console.WriteLine($"[ERROR] {DateTime.Now:HH:mm:ss} - {message}");
        }
    }
}
