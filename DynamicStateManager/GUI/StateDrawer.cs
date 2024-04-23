using DynamicStateManager.States;

namespace DynamicStateManager.GUI
{
    public static class StateDrawer
    {
        public static void DrawState(State state)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"=== {state}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void DrawOptions(string[] options)
        {
            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine($"{i + 1}) {options[i]}");
            }
        }
    }
}

