using System.Collections.Generic;

/// <summary>
/// 多重映射结构
/// </summary>
/// <typeparam name="T">键值</typeparam>
/// <typeparam name="K">Hashset集合</typeparam>
public class UnOrderMultiMapSet<T, K> : Dictionary<T, HashSet<K>>
{
    // 重用Hashset
    public new HashSet<K> this[T t]
    {
        get
        {
            if (!this.TryGetValue(t, out var set))
            {
                set = new HashSet<K>();
            }

            return set;
        }
    }
    
    public Dictionary<T,HashSet<K>> GetDictionary()
    {
        return this;
    }
    
    public void Add(T t, K k)
    {
        if (!this.TryGetValue(t, out var set))
        {
            set = new HashSet<K>();
            base[t] = set;
        }
        set.Add(k);
    }
    
    public bool Remove(T t, K k)
    {
        if (!this.TryGetValue(t, out var set))
        {
            return false;
        }
        if (!set.Remove(k))
        {
            return false;
        }
        if (set.Count == 0)
        {
            this.Remove(t);
        }
        return true;
    }
    
    public bool Contains(T t, K k)
    {
        if (!this.TryGetValue(t, out var set))
        {
            return false;
        }
        return set.Contains(k);
    }

    public new int Count
    {
        get
        {
            int count = 0;
            foreach (KeyValuePair<T,HashSet<K>> kv in this)
            {
                count += kv.Value.Count;
            }

            return count;
        }
    }
}