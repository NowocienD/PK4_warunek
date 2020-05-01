﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ColorToGrayScale
{
    public class LogerService : ILogerService
    {
        private const string FilePath = "log.txt";

        public void Debug(string message)
        {
            WriteToLog(String.Format("Debug |{0}| {1}{2}", System.DateTime.Now.ToString(), message, Environment.NewLine));
        }

        public void Info(string message)
        {
            WriteToLog(String.Format("Info |{0}| {1}{2}", System.DateTime.Now.ToString(), message, Environment.NewLine));
        }

        public void Warning(string message)
        {
            WriteToLog(String.Format("Warning |{0}| {1}{2}", System.DateTime.Now.ToString(), message, Environment.NewLine));
        }

        public void Error(string message)
        {
            WriteToLog(String.Format("Error |{0}| {1}{2}", System.DateTime.Now.ToString(), message, Environment.NewLine));
        }

        public string ReadLog(string regexPattern)
        {
            string[] lines = ReadFromFile();

            Regex regex = new Regex(regexPattern);

            var regexMatch = lines.Where<string>(x => regex.IsMatch(x));
            return string.Join(Environment.NewLine, regexMatch);
        }

        private void WriteToLog(string message)
        {
            File.AppendAllText(FilePath, message);
        }

        private string[] ReadFromFile()
        {
            if (!File.Exists(FilePath))
            {
                return new string[0];
            }
            return File.ReadAllLines(FilePath);
        }
    }
}
