using System.Runtime.InteropServices;
using NullGuard;using Resort.Actions;
using Resort.Views;

[assembly: NullGuard(ValidationFlags.All)]namespace Resort{
    internal class Program    {        [DllImport("libc")]        private static extern int system(string exec);

        private static void Main(string[] args)        {
            system(@"printf '\e[8;26;150t'");
            BuildManager bm = new BuildManager();
            Game g = new Game();
            TurnStartView view = new TurnStartView(g);            g.NewTurn();
            IAction action = view.GetAction();            while (action != null)
            {
                action = action.Do();
            }        }    }}