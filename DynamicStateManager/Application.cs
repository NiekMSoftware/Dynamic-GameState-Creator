using DynamicStateManager.States;

namespace DynamicStateManager
{
    public class Application
    {
        private readonly StateStack states;

        public Application()
        {
            states = new StateStack(new State.Context(10));
            RegisterStates();

            // push in the menu state
            states.PushState(State.ID.MAIN_MENU);

            // apply any pending changes
            states.ApplyPendingChanges();
        }

        private void RegisterStates()
        {
            states.RegisterState<MenuState>(State.ID.MAIN_MENU);
        }

        public void Run()
        {
            while (states.stateStack.Count > 0)
            {
                Draw();
                Update();
            }
        }

        private void Update()
        {
            states.Update();
        }

        private void Draw()
        {
            states.Draw();
        }
    }
}