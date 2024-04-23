namespace DynamicStateManager.Input_Manager;

public static class Input
{
    public static int GetIntInput(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);

            if (int.TryParse(Console.ReadLine(), out int input))
            {
                return input;
            }

            Console.WriteLine($"Invalid input, please enter a valid integer..");
        }
    }
}