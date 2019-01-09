using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Resort.Actions;

namespace Resort.Views
{
    internal class MenuView : IView
    {
        public MenuView(IReadOnlyCollection<(string, IAction)> items)
        {
            Contract.Requires(items.Count > 0);

            _list.AddRange(items);
            _selectedItem = _list.First();
        }

        public IAction GetAction()
        {
            //TODO: remove Magic
            _menuStartX = Console.CursorTop;
            Console.CursorVisible = false;
            foreach (var item in _list)
            {
                string selectedMark = item.Equals(_selectedItem) ? ">" : " ";
                Console.WriteLine($"{selectedMark} {item.Item1}");
            }
            while (true)
            {
                ConsoleKeyInfo pressedKey = Console.ReadKey(true);
                Console.SetCursorPosition(0, _menuStartX + _selectedIndex);
                Console.Write(" ");
                switch (pressedKey.Key)
                {
                    case ConsoleKey.UpArrow:
                        _selectedItem = _selectedItem.Equals(_list.First()) ? _list.Last() : _list[_selectedIndex - 1];
                        break;
                    case ConsoleKey.DownArrow:
                        _selectedItem = _selectedItem.Equals(_list.Last()) ? _list.First() : _list[_selectedIndex + 1];
                        break;
                    case ConsoleKey.Enter:
                    case ConsoleKey.Spacebar:
                        Console.SetCursorPosition(0, _menuStartX + _list.Count + 1);
                        return _selectedItem.Item2;
                }
                Console.SetCursorPosition(0, _menuStartX + _selectedIndex);
                Console.Write(">");
            }
        }

        private int _menuStartX;

        private int _selectedIndex => _list.IndexOf(_selectedItem);

        private (string, IAction) _selectedItem;
        private List<(string, IAction)> _list = new List<(string, IAction)>();
    }
}
