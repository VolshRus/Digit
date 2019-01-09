using System.Runtime.InteropServices;
using NullGuard;using Resort.Actions;
using Resort.Views;

[assembly: NullGuard(ValidationFlags.All)]namespace Resort{
    internal class Program    {
        private static void Main(string[] args)        {
            Game g = new Game();
            TurnStartView view = new TurnStartView(g);            IAction action = view.GetAction();            while (action != null)
            {
                action = action.Do();
            }        }    }}