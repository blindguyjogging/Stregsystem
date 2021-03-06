﻿using System;
using System.IO;

namespace Stregsystem
{
    public static class Logger
    {
        public static string basefolder = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        public static string transactionfile = Path.Combine(basefolder, "Logs\\TransactionLogs.txt");
        public static string userfile = Path.Combine(basefolder, "Logs\\UserLogs.txt");
        public static string productfile = Path.Combine(basefolder, "Logs\\ProductLogs.txt");

        public static void GenericLogger(string log, string path)
        {
            FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
            fs.Seek(0, SeekOrigin.End);
            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.WriteLine(log);
            }
        }
        public static void TransactionLog(string log)
        {
            GenericLogger(log, transactionfile);

        }

        public static void UserLog(string log)
        {
            GenericLogger(log, userfile);
        }

        public static void ProductLog(string log)
        {
            GenericLogger(log, productfile);

        }
    }
}
