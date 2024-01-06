using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///A scriptable object that stores references to T in a hashmap.
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class RuntimeSet<T> : ScriptableObject
{
    public Dictionary<int, T> Items = new Dictionary<int, T>();

    /// <summary>
    /// Adds an element to the hashmap and returns its key.
    /// </summary>
    /// <param name="t">Object to add</param>
    /// <returns>The generated hash used to access the object.</returns>
    public int Add(T t)
    {
        int hash = Guid.NewGuid().GetHashCode();
        if (!Items.ContainsValue(t)) Items.Add(hash, t);
        return hash;
    }

    /// <summary>
    /// Removes an element from the hashmap with its key.
    /// </summary>
    /// <param name="hash">The hash of the object (generated in the Add function).</param>
    public void Remove(int hash)
    {
        if (Items.ContainsKey(hash)) Items.Remove(hash);
    }

    public T GetItem(int hash)
    {
        return Items[hash];
    }
}