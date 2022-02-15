using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordAssistCLI
{
    public class Process
    {
        public string Input { get; }
        public string Description { get; }
        public Action Reponse { get; }
        public Process()
        {
            this.Input = "";
            this.Description = "";
            this.Reponse = () => { };
        }

        public Process(string input, string description, Action reponse)
        {
            this.Input = input;
            this.Description = description;
            this.Reponse = reponse;
        }

    }
}
