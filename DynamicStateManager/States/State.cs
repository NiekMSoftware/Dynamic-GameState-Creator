namespace DynamicStateManager.States
{
    public abstract class State
    {
        /// <summary>
        /// The ID's of the States that you want.
        /// </summary>
        public enum ID
        {
            MAIN_MENU, // The Main Menu state's ID.
            GAME, // The Game state's ID.
            PAUSE, // The Pause state's ID.
            NONE
        }

        /// <summary>
        /// The context that each <see cref="State"/> has.
        /// This could be easily modified to include the data you need.
        /// </summary>
        public struct Context
        {
            /// <summary>
            /// Constructor of the Context, make sure to include the properties you want to
            /// have initialised within the arguments.
            /// </summary>
            /// <param name="_health">Temporary argument for Health.</param>
            public Context(int _health)
            {
                Health = _health;
            }

            // define properties when needed
            // for now we will just use health.
            public readonly int Health;
        }

        // The Stack to access for all the States.
        protected StateStack StateStack;

        // The Context to access for all the States.
        protected Context StateContext;

        /// <summary>
        /// Constructs and intialises the States with valid <see cref="StateStack"/> and <see cref="Context"/> components.
        /// </summary>
        /// <param name="_stateStack">The StateStack of the State.</param>
        /// <param name="_stateContext">The Context of the State.</param>
        protected State(StateStack _stateStack, Context _stateContext)
        {
            StateStack = _stateStack;
            StateContext = _stateContext;
        }

        /// <summary>
        /// Responsible for handling and updating any events.
        /// </summary>
        public abstract void Update();

        /// <summary>
        /// Responsible for drawing out the State accordingly.
        /// </summary>
        public abstract void Draw();

        /// <summary>
        /// Responsible for Processing any integer input.
        /// </summary>
        /// <param name="input">The input to process.</param>
        protected abstract void ProcessIntInput(int input);
    }
}
