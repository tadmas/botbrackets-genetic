using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Tadmas.BotBrackets.Genetic
{
    public static class CombinationsExtension
    {
        public static IEnumerable<T> Choose<T>(this IEnumerable<T> items, int k, Random r)
        {
            IEnumerable<IEnumerable<T>> allCombinations = items.Combinations(k);
            int which = r.Next(allCombinations.Count());
            foreach (var c in allCombinations)
            {
                if (which == 0)
                    return c;
                else
                    which--;
            }
            throw new InvalidOperationException();
        }

        // http://ericlippert.com/2014/10/20/producing-combinations-part-three/
        private static IEnumerable<IEnumerable<T>> Combinations<T>(this IEnumerable<T> items, int k)
        {
            if (k < 0)
                throw new InvalidOperationException();
            return
              from combination in Combinations(items.Count(), k)
              select items.ZipWhere(combination);
        }

        // http://ericlippert.com/2014/10/20/producing-combinations-part-three/
        private static IEnumerable<ImmutableStack<bool>> Combinations(int n, int k)
        {
            if (k == 0 && n == 0)
            {
                yield return ImmutableStack<bool>.Empty;
                yield break;
            }
            if (n < k)
                yield break;
            if (k > 0)
                foreach (var r in Combinations(n - 1, k - 1))
                    yield return r.Push(true);
            foreach (var r in Combinations(n - 1, k))
                yield return r.Push(false);
        }

        // http://ericlippert.com/2014/10/20/producing-combinations-part-three/
        private static IEnumerable<T> ZipWhere<T>(this IEnumerable<T> items, IEnumerable<bool> selectors)
        {
            using (var e1 = items.GetEnumerator())
                using (var e2 = selectors.GetEnumerator())
                    while (e1.MoveNext() && e2.MoveNext())
                        if (e2.Current)
                            yield return e1.Current;
        }

        // http://ericlippert.com/2014/10/16/producing-combinations-part-two/
        private abstract class ImmutableStack<T>: IEnumerable<T>
        {
            public static readonly ImmutableStack<T> Empty = new EmptyStack();
            private ImmutableStack() {}
            public abstract ImmutableStack<T> Pop();
            public abstract T Top { get; }
            public abstract bool IsEmpty { get; }
            public IEnumerator<T> GetEnumerator()
            {
                var current = this;
                while(!current.IsEmpty)
                {
                    yield return current.Top;
                    current = current.Pop();
                }    
            }
            IEnumerator IEnumerable.GetEnumerator() 
            { 
                return this.GetEnumerator(); 
            }
            public ImmutableStack<T> Push(T value)
            {
                return new NonEmptyStack(value, this);
            }
            private class EmptyStack: ImmutableStack<T>
            {
                public override ImmutableStack<T> Pop()
                { 
                    throw new InvalidOperationException(); 
                }
                public override T Top 
                { 
                    get { throw new InvalidOperationException(); } 
                }
                public override bool IsEmpty { get { return true; } }
            } 
            private class NonEmptyStack : ImmutableStack<T>
            {
                private readonly T head;
                private readonly ImmutableStack<T> tail;
                public NonEmptyStack(T head, ImmutableStack<T> tail)
                {
                    this.head = head;
                    this.tail = tail;
                }
                public override ImmutableStack<T> Pop() { return this.tail; }
                public override T Top { get { return this.head; } }
                public override bool IsEmpty { get { return false; } } 
            }
        }
    }
}
