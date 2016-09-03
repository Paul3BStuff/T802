using System;

namespace FormulaRunner
{
    /// <summary>
    /// Implement this class to accept messages from the Formula Runner
    /// and output to a device.
    /// </summary>
    interface IStatusDisplay
    {
        /// <summary>
        /// Set the line where the output will be displayed.
        /// </summary>
        /// <param name="line">The line number to use.</param>
        void SetLine(int line);
        /// <summary>
        /// Sets the total rows to be displayed.
        /// </summary>
        /// <param name="total">The amount.</param>
        void SetTotal(int total);
        /// <summary>
        /// Instructs the class to update the count to the provided value.
        /// </summary>
        /// <param name="value">The value to be used.</param>
        void UpdateCount(int value);
        /// <summary>
        /// The text to be displayed.
        /// </summary>
        void SetText(string text);
    }

    /// <summary>
    /// Displays messages from Formula Runner on the console.
    /// </summary>
    class ConsoleStatusDisplay : IStatusDisplay
    {
        private int _total;
        private int _line;

        public void SetLine(int line)
        {
            _line = line;
        }

        public void SetText(string text)
        {
            Console.SetCursorPosition(0, _line);
            Console.WriteLine(text);
        }

        public void SetTotal(int total)
        {
            _total = total;
        }

        public void UpdateCount(int value)
        {
            Console.SetCursorPosition(0, _line + 1);
            Console.WriteLine($"{value:000000} of {_total:000000}");
        }
    }
}
