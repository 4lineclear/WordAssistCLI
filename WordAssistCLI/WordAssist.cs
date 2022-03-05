using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordAssistCLI
{
    public class WordAssist
    {
        public string Input { get; private set; }
        private Process CurrentProcess { get; set; }
        private KeyMatcher Matcher { get; }
        private HangmanAssist Hangman { get; }
        public WordAssist()
        {
            this.Input = "";
            this.Matcher = new KeyMatcher("Input Unrecognized");
            this.CurrentProcess = new Process("", "", StartingMessage);
            Hangman = new HangmanAssist(this);

            ProcessMatcher actionMatcher = new ProcessMatcher("/", "Action Unrecognized");
            ProcessMatcher hangmanMatcher = new ProcessMatcher("/", "Hangman Action Unrecognized");
            ProcessMatcher commandMatcher = new ProcessMatcher("!", "Command Unrecognized");

            TemplateProcess[] hangmanActions =
            {
                new TemplateProcess("back","returns to main menu", () =>
                {
                    Matcher.SetNewProcessMatcher("/", actionMatcher);
                    Console.Clear();
                    Console.WriteLine("Returned to main menu");
                }, true),
                new TemplateProcess("setlen","sets the length of the word",Hangman.SetLen, false),
                new TemplateProcess("setcl","sets a correct letter, or sets all correct letters to a new value",Hangman.SetCL, false),
                new TemplateProcess("setil","sets a incorrect letter, or sets all incorrect letters to a new value",Hangman.SetIL, false),
                new TemplateProcess("addcl","adds a correct letter",Hangman.AddCL, false),
                new TemplateProcess("addil","adds a incorrect letter",Hangman.AddIL, false),
                new TemplateProcess("rmcl","removes a correct letter",Hangman.RMCL, false),
                new TemplateProcess("rmil","removes a incorrect letter",Hangman.RMIL, false),
                new TemplateProcess("listcl","lists correct letters",Hangman.ListCL, true),
                new TemplateProcess("listil","lists incorrect letters",Hangman.ListIL, true),
                new TemplateProcess("listag","lists all guesses",Hangman.ListAG, true),
                new TemplateProcess("listbg","lists best guesses, optianally all of them or a cetain number of them",Hangman.ListBG, false),
                new TemplateProcess("numg","outputs number of guesses",Hangman.NumG, false),
                new TemplateProcess("clearcl","clears correct letters",Hangman.ClearCl, true),
                new TemplateProcess("clearil","clears incorrect letters",Hangman.ClearIL, true),
                new TemplateProcess("clearall","clears all values",Hangman.ClearAll, true),
                new TemplateProcess("listall","lists set values",Hangman.ListAll, true)
            };
            foreach(TemplateProcess process in hangmanActions)
            {
                hangmanMatcher.AddProcess(process);
            }

            actionMatcher.AddProcess("hangman", "starts hangman", () => 
            {
                Matcher.SetNewProcessMatcher("/", hangmanMatcher);
                Console.Clear();
                Console.WriteLine("Hangman started");
            });


            commandMatcher.AddProcess("clear", "clears the console", () => Console.Clear());
            commandMatcher.AddProcess("commands", "prints list of commands", () => Console.WriteLine($"Commands:\n{commandMatcher.GetProcessInputs}"));
            commandMatcher.AddProcess("actions", "prints list of usable actions", () => Console.WriteLine($"Actions:\n{Matcher.GetProcessMatcher("/").GetProcessInputs}"));
            commandMatcher.AddProcess("quit", "ends word assist", ()=>{ });

            this.Matcher.AddProcessMatcher(actionMatcher);
            this.Matcher.AddProcessMatcher(commandMatcher);
        }
        public void StartAssist()
        {
            while (Input != "!quit")
            {
                this.CurrentProcess.Response();
                this.Input = Console.ReadLine().Trim().ToLower();
                this.CurrentProcess = Matcher.GetProcess(Input);
            }
            Console.WriteLine("goodbye");
        }
        public void StartingMessage()
        {
            Console.WriteLine(  "Welcome To Word Assist! The very best word game tool around\n" +
                                "===========================\n" +
                                "   Press Enter to Start    \n" +
                                "===========================");
            while (Console.ReadKey().Key != ConsoleKey.Enter)
            {
                Console.WriteLine();
            }
            Console.Clear();
        }
    }
}
