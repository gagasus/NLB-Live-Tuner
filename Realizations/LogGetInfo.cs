using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ServersDistribution.Interfaces;

namespace ServersDistribution.Realizations
{
    class LogGetInfo : ILogGetInfo
    {
        private List<string> ResultsFromRegex(string pattern,string text)
        {
            Regex regex = new Regex(pattern);
            Match match = regex.Match(text);
            List<string> results = new List<string>();
            while (match.Success)
            {
                results.Add(match.Value);
                match = match.NextMatch();
            }
            return results;
        }
        private List<double> ConvertNumbers(List<string> list)
        {
            string tmp;
            List<double> results = new List<double>();
            for(int i = 0; i < list.Count; i++)
            {
                tmp = list[i].Replace(".", ",");
                double num = -1;
                try
                {
                    num = Convert.ToDouble(tmp);
                    results.Add(num);
                }
                catch (FormatException ex) { }
                catch (OverflowException ex) { }
            }
            return results;
        }
        public List<double> LogGetTime(string log)
        {
            string pattern = @"(?<=(-\s)|(,\s))\d+\.\d+[^.,]";
            List<string> regexResults = ResultsFromRegex(pattern, log);
            return ConvertNumbers(regexResults);
        }
        public List<string> LogGetServerAddresses(string log)
        {
            string pattern = @"(?:(?:-,\s))?(\d+\.\d+\.\d+\.\d+[:]\d+)";
            return ResultsFromRegex(pattern, log);
        }
        public int GetServerNumber(string address)
        {
            string pattern = @"(?<=\.)[0-9]*?(?=:)";
            List<string> results = ResultsFromRegex(pattern, address);
            return int.Parse(results[0]);
        }
    }
}
