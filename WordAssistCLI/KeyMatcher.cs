using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordAssistCLI
{
    public class KeyMatcher
    {
        private List<ProcessMatcher> Matchers { get; }
        private Process Unrecognized { get; }
        public KeyMatcher()
        {
            this.Matchers = new List<ProcessMatcher>(); 
            this.Unrecognized = new Process();
        }
        public KeyMatcher(string unrecognizedResponse)
        {
            this.Matchers = new List<ProcessMatcher>();
            this.Unrecognized = new Process("","", () => Console.WriteLine(unrecognizedResponse));
        }
        public Process GetProcess(string input)
        { 
            foreach (ProcessMatcher matcher in this.Matchers)
            {
                if (input.StartsWith(matcher.KeyWord))
                {
                    return matcher.GetProcess(input);
                }
            }
            return Unrecognized;
        }
        public ProcessMatcher? GetProcessMatcher(string keyWord)
        {
            foreach (ProcessMatcher matcher in this.Matchers)
            {
                if (keyWord == matcher.KeyWord)
                {
                    return matcher;
                }
            }
            return null;
        }
        public void AddProcessMatcher(ProcessMatcher processMatcher)
        {
            this.Matchers.Add(processMatcher);
        }
        public void SetNewProcessMatcher(string keyWord, ProcessMatcher newProcessMatcher)
        {
            for(int i= 0; i < this.Matchers.Count; i++)
            {
                if(this.Matchers[i].KeyWord == keyWord)
                {
                    this.Matchers[i] = newProcessMatcher;
                }
            }
        }
    }
}
