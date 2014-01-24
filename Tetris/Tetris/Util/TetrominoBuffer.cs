using System;
using System.Collections.Generic;
using System.Linq;

namespace Tetris
{
    class TetrominoBuffer
    {
        IEnumerable<IEnumerable<int>> _bags = new List<List<int>>();
        Random _rand;
        Queue<int> _buffer;

        public TetrominoBuffer()
        {
            _rand = new Random();
            _bags = Permutation.Permutations<int>(new List<int> { 0, 1, 2, 3, 4, 5, 6 });
            _buffer = new Queue<int>();
            AddBag();
            AddBag();
        }

        public IEnumerable<int> GetBag()
        {
            return RemoveBag();
        }

        public IEnumerable<int> PeekBag()
        {
            List<int> bag = new List<int>();
            for (int i = 0; i != 7; i++)
                bag.Add(_buffer.Peek());

            return bag;
        }

        public int GetNext()
        {
            int n = _buffer.Dequeue();
            if (_buffer.Count == 7)
                AddBag();

            return n;
        }

        public int Peek()
        {
            return _buffer.Peek();
        }

        public IEnumerable<int> Peek(int ammount)
        {
            List<int> bag = new List<int>();
            for (int i = 0; i != ammount; i++)
                bag.Add(_buffer.ElementAt<int>(i));

            return bag;
        }

        private void AddBag()
        {
            int n = _rand.Next(_bags.Count() - 1);
            foreach (int tetromino in _bags.ElementAt(n))
                _buffer.Enqueue(tetromino);
        }

        private List<int> RemoveBag()
        {
            List<int> bag = new List<int>();
            for(int i = 0; i != 7; i++)
                bag.Add(_buffer.Dequeue());

            AddBag();
            return bag;
        }
    }
}
