namespace DynamicStateManager.States
{
    public abstract class State
    {
        public enum ID
        {
            MAIN_MENU,
            GAME,
            PAUSE,
            NONE
        }

        public struct Context
        {
            public Context(int _health)
            {
                Health = _health;
            }

            // define properties when needed
            public readonly int Health;
        }

        protected StateStack StateStack;
        protected Context StateContext;

        protected State(StateStack _stateStack, Context _stateContext)
        {
            StateStack = _stateStack;
            StateContext = _stateContext;
        }


        public abstract void Update();
        public abstract void Draw();
        protected abstract void ProcessIntInput(int input);
    }
}
