namespace DynamicStateManager.States
{
    /// <summary>
    /// Represents a stack-based manager for handling states in a dynamic system.
    /// </summary>
    public class StateStack
    {
        /// <summary>
        /// Defines actions that can be performed on the state stack.
        /// </summary>
        public enum Action
        {
            // Push in a new state.
            PUSH,
            // Pop the current State.
            POP,
            // Clear out all the States.
            CLEAR
        }

        // Structure to represent pending changes to the state stack.
        private struct PendingChange
        {
            public Action Action;
            public State.ID StateId;

            public PendingChange(Action _action, State.ID _stateId)
            {
                Action = _action;
                StateId = _stateId;
            }
        }

        // Dictionary to store state factories.
        private readonly Dictionary<State.ID, Func<State?>> factories = new();

        // Stack to maintain the state stack.
        public readonly Stack<State?> stateStack = new();

        // List to track pending state changes.
        private readonly List<PendingChange> pendingList = new();

        // Context object among other states.
        private readonly State.Context context;

        /// <summary>
        /// Initializes a new instance of the <see cref="StateStack"/> class with the specified context.
        /// </summary>
        /// <param name="_context">The context shared among states.</param>
        public StateStack(State.Context _context)
        {
            context = _context;
        }

        /// <summary>
        /// Registers a state type with the state stack.
        /// </summary>
        /// <typeparam name="T">The type of the state to register.</typeparam>
        /// <param name="stateId">The ID of the state to register.</param>
        public void RegisterState<T>(State.ID stateId) where T : State
        {
            factories[stateId] = () => Activator.CreateInstance(typeof(T), this, context) as T;
        }

        /// <summary>
        /// Updates all states in the stack.
        /// </summary>
        public void Update()
        {
            foreach (var state in stateStack)
            {
                state?.Update();
            }
            
            ApplyPendingChanges();
        }

        /// <summary>
        /// Draws all states in the stack.
        /// </summary>
        public void Draw()
        {
            foreach (var state in stateStack)
            {
                state?.Draw();
            }
        }

        /// <summary>
        /// Pushes a state onto the stack.
        /// </summary>
        /// <param name="_stateId">The ID of the state to push.</param>
        public void PushState(State.ID _stateId)
        {
            pendingList.Add(new PendingChange(Action.PUSH, _stateId));
        }

        /// <summary>
        /// Pops the current state from the stack.
        /// </summary>
        public void PopState()
        {
            // we define none as it will pop automatically regardless of state.
            pendingList.Add(new PendingChange(Action.POP, State.ID.NONE));
        }

        /// <summary>
        /// Clears all states from the stack.
        /// </summary>
        public void ClearStates()
        {
            // we define none as it will clear automatically regardless of state.
            pendingList.Add(new PendingChange(Action.CLEAR, State.ID.NONE));
        }

        /// <summary>
        /// Applies pending changes to the state stack.
        /// </summary>
        public void ApplyPendingChanges()
        {
            // iterate through the pending list
            foreach (var change in pendingList)
            {
                // handle action
                switch (change.Action)
                {
                    case Action.PUSH:
                        stateStack.Push(CreateState(change.StateId));
                        break;

                    case Action.POP:
                        stateStack.Pop();
                        break;

                    case Action.CLEAR:
                        stateStack.Clear();
                        break;
                }
            }

            // clear list so it won't repeat
            pendingList.Clear();
        }

        /// <summary>
        /// Creates a new state instance based on the provided state ID.
        /// </summary>
        /// <param name="_stateId">The ID of the state to create.</param>
        /// <returns>A new instance of the specified state.</returns>
        /// <exception cref="ArgumentException">Thrown when the state ID is unknown.</exception>
        private State? CreateState(State.ID _stateId)
        {
            if (factories.TryGetValue(_stateId, out var factory))
            {
                return factory.Invoke();
            }

            throw new ArgumentException($"Unknown state ID: {_stateId}");
        }
    }
}