using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordAssistCLI
{
    /// <summary>
    /// Used to process a user's input and respond meaningfully 
    /// </summary>
    public class Process
    {
        /// <summary>
        /// Refers to an accpeted string input used when validating user input
        /// </summary>
        /// <remarks>
        /// Is private because any validation should be through <see cref="Validate"/>
        /// </remarks>
        private string Input { get; }
        /// <summary>
        /// Contains a description of what  <see cref="Response"/> to achieve
        /// </summary>
        /// <remarks>
        /// Contains <see cref="Input"/> as part of the description
        /// <para>
        /// Is (intended) to be used to list to the user a list of Processes
        /// </para>
        /// </remarks>
        public string Description { get; }
        /// <summary>
        /// A response to a user's action
        /// </summary>
        /// <remarks>
        /// Is only meant to be called when <see cref="Validate"/> returns true
        /// </remarks>
        public Action Response { get; }
        /// <summary>
        /// The method in which a user's validation is handled
        /// </summary>
        /// <remarks>
        /// Validation can be handled either by an equality operator, or <see cref="String.StartsWith(string)"/>
        /// </remarks>
        /// <returns>
        /// True if the inputted <c>string</c> matches to <see cref="Input"/> according to the specified method
        /// </returns>
        public Predicate<string> Validate { get; }
        /// <summary>
        /// Creates an empty object with non null values
        /// </summary>
        public Process()
        {
            this.Input = "";
            this.Description = "";
            this.Response = () => { };
            this.Validate = (string input) => input == this.Input;
        }
        /// <summary>
        ///     Creates <c>Process</c> object with specified params and a default validation
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         The validation used by <see cref="Validate"/> is an equality operator.
        ///     </para>
        ///     <para>
        ///         Use <see cref="Process(string, string, Action, bool)"/> to use the other
        ///         validation method.
        ///     </para>
        /// </remarks>
        /// <param name="input">A specified input.</param>
        /// <param name="description"> A description matching <see cref="Response"/>.</param>
        /// <param name="reponse">The response to a user's input</param>
        public Process(string input, string description, Action reponse)
            :this(input, description, reponse, true) {}
        /// <summary>
        ///     Creates <c>Process</c> object with specified params
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         If <c>validation</c> is true, <see cref="Validate"/> uses equality operator
        ///     </para>
        ///     <para>
        ///         If <c>validation</c> is false, <see cref="Validate"/> uses <see cref="String.StartsWith(string)"/>
        ///     </para>
        /// </remarks>
        /// <param name="input">A specified input.</param>
        /// <param name="description"> A description matching <see cref="Response"/>.</param>
        /// <param name="reponse">The response to a user's input</param>
        /// <param name="validation">A bool used to decide the method in which validation is carried out</param>
        public Process(string input, string description, Action reponse, bool validation)
        {
            this.Input = input;
            this.Description = $"{input} - {description}";
            this.Response = reponse;
            this.Validate = validation ? 
                (string userInput) => this.Input == userInput : 
                (string userInput) => userInput.StartsWith(this.Input);
        }

    }
}
