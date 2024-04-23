using DynamicStateManager.GUI;
using DynamicStateManager.Input_Manager;

namespace DynamicStateManager.States
{
    public class MenuState : State
    {
        private readonly string[] options = [ "Play", "Options", "Quit" ];

        public MenuState(StateStack _stateStack, Context _stateContext) : base(_stateStack, _stateContext)
        { }

        public override void Update()
        {
            // for now update should be responsible for receiving input
            ProcessIntInput(Input.GetIntInput("> "));
        }

        public override void Draw()
        {
            StateDrawer.DrawState(this);
            StateDrawer.DrawOptions(options);
        }

        protected override void ProcessIntInput(int input)
        {
            switch (input)
            {
                case 1:
                    StateStack.PopState();
                    // StateStack.PushState();
                    break;

                case 2:
                    // StateStack.PushState(State.ID.OPTIONS);
                    break;

                case 3:
                    StateStack.ClearStates();
                    break;
            }
        }

        public override string ToString()
        {
            return "Main Menu";
        }
    }
}