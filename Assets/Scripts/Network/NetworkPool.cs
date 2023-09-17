using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System;
using UnityEngine;

namespace Sky9th.Network
{
    public class NetworkPool<T>
    {
        // Mirror is single threaded, no need for concurrent collections.
        // stack increases the chance that a reused writer remains in cache.
        readonly Stack<T> objects = new Stack<T>();

        // take an element from the pool, or create a new one if empty
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Get() => objects.Pop();

        // return an element to the pool
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Return(T item) => objects.Push(item);

        // count to see how many objects are in the pool. useful for tests.
        public int Count => objects.Count;



    }

}