using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;
using System.Web;

namespace Utilities
{
    public static class Logger
    {
        public static bool UseConsole = false;

        private static void write(string message)
        {
            try
            {
                string filename = HttpRuntime.AppDomainAppPath + "Logs/" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + ".txt";

                if (!File.Exists(filename))
                {
                    using (StreamWriter file = File.CreateText(filename))
                    {
                        file.WriteLine(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " " + message);
                    }
                }
                else
                {
                    using (StreamWriter file = new StreamWriter(filename, true))
                    {
                        file.WriteLine(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " " + message);
                    }
                }
            }
            catch (Exception ex)
            {
                string error = "Could not write log";
            }
        }

        public static void info(String message, int eventId)
        {
            if (UseConsole)
                Console.WriteLine("INFO: " + eventId + " - " + message);

            write("INFO: " + eventId + " - " + message);
        }

        public static void warning(String message, int eventId)
        {
            if (UseConsole)
                Console.WriteLine("WARNING: " + eventId + " - " + message);

            write("WARNING: " + eventId + " - " + message);
        }

        public static void error(String message, int eventId)
        {
            if (UseConsole)
                Console.WriteLine("ERROR: " + eventId + " - " + message);

            write("ERROR: " + eventId + " - " + message);
        }

        public static void error(Exception exception, int eventId)
        {
            if (UseConsole)
                Console.WriteLine("ERROR: " + eventId + " - " + exception.Message);

            String message = "Error:\n";
            message += "Message: " + exception.Message + "\n\n";
            message += "String: " + exception.ToString() + "\n\n";
            message += "StackTrace: " + exception.StackTrace + "\n\n";

            write("ERROR: " + eventId + " - " + message);
        }
    }
}
