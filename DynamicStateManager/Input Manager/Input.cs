namespace DynamicStateManager.Input_Manager;

public static class Input
{
    /// <summary>
    /// Receives and returns integer input for the user verify their input.
    /// </summary>
    /// <param name="_prompt">The prompt to display, typically a '>' would be used to display the prompt.</param>
    /// <returns>A valid integer value.</returns>
    public static int GetIntInput(string _prompt)
    {
        while (true)
        {
            Console.Write(_prompt);

            if (int.TryParse(Console.ReadLine(), out int input))
            {
                return input;
            }

            Console.WriteLine($"Invalid input, please enter a valid integer..");
        }
    }
}