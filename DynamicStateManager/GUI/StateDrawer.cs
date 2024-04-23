using DynamicStateManager.States;

namespace DynamicStateManager.GUI
{
    public static class StateDrawer
    {
        /// <summary>
        /// Draws out the State to the console like a title.
        /// </summary>
        /// <param name="state">The state that will be drawn.</param>
        public static void DrawState(State state)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"=== {state}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Draws out the options that the state has.
        /// </summary>
        /// <param name="options">The array of options to display.</param>
        public static void DrawOptions(string[] options)
        {
            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine($"{i + 1}) {options[i]}");
            }
        }
    }
}

