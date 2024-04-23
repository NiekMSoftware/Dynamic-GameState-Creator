namespace DynamicStateManager.States
{
    public class StateStack
    {
        public enum Action
        {
            Push, // Push in a new state.
            Pop, // Pop the current State.
            Clear // Clear out all the States.
        }

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

        private readonly Dictionary<State.ID, Func<State?>> factories = new();
        public readonly Stack<State?> stateStack = new();
        private readonly List<PendingChange> pendingList = new();
        private readonly State.Context context;

        public StateStack(State.Context _context)
        {
            context = _context;
        }

        public void RegisterState<T>(State.ID stateId) where T : State
        {
            factories[stateId] = () => Activator.CreateInstance(typeof(T), this, context) as T;
        }

        public void Update()
        {
            foreach (var state in stateStack)
            {
                state?.Update();
            }
            
            ApplyPendingChanges();
        }

        public void Draw()
        {
            foreach (var state in stateStack)
            {
                state?.Draw();
            }
        }

        public void PushState(State.ID _stateId)
        {
            pendingList.Add(new PendingChange(Action.Push, _stateId));
        }

        public void PopState()
        {
            // we define none as it will pop automatically regardless of state.
            pendingList.Add(new PendingChange(Action.Pop, State.ID.NONE));
        }

        public void ClearStates()
        {
            // we define none as it will clear automatically regardless of state.
            pendingList.Add(new PendingChange(Action.Clear, State.ID.NONE));
        }

        public void ApplyPendingChanges()
        {
            foreach (var change in pendingList)
            {
                switch (change.Action)
                {
                    case Action.Push:
                        stateStack.Push(CreateState(change.StateId));
                        break;

                    case Action.Pop:
                        stateStack.Pop();
                        break;

                    case Action.Clear:
                        stateStack.Clear();
                        break;
                }
            }

            pendingList.Clear();
        }

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