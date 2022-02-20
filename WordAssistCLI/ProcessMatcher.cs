using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordAssistCLI
{
    public class ProcessMatcher
    {
        public string KeyWord { get; }
        public Process Unrecognized { get; }
        private List<Process> Processes { get; set; }
        public ProcessMatcher()
        {
            this.KeyWord = "";
            this.Unrecognized = new Process("", "", () => { });
            this.Processes = new List<Process>();
        }
        public ProcessMatcher(string keyWord, string unrecognizedResponse)
        {
            this.KeyWord = keyWord;
            this.Unrecognized = new Process("", "", () => 
            { 
                Console.WriteLine(unrecognizedResponse);
                Console.Beep();
            });
            this.Processes = new List<Process>();
        }
        public Process GetProcess(string input)
        {
            foreach (Process process in Processes)
            {
                if (process.Validate(input))
                {
                    return process;
                }
            }
            return Unrecognized;
        }
        public string GetProcessInputs
        {
            get
            {
                string temp = "";
                foreach (Process process in Processes)
                {
                    temp += process.Description + "\n";
                }
                return temp;
            }
        }
        public void AddProcess(string input, string description, Action response)
        {
            this.Processes.Add(new Process(KeyWord + input, description, response));
        }
        public void AddProcess(string input, string description, Action response, bool validation)
        {
            this.Processes.Add( new Process(KeyWord + input, description, response, validation) );
        }
        public void SetNewProcesses(List<Process> newProcesses)
        {
            this.Processes = newProcesses;
        }
        public void RemoveProcess(string input)
        {
            foreach (Process process in Processes)
            {
                if (process.Validate(KeyWord + input))
                {
                    Processes.Remove(process);
                    return;
                }
            }
        }


    }
}
