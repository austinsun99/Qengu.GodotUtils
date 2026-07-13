using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Qengu.GodotUtils.Collections;

/// <summary>
/// A bidirectional dictionary: maps keys/values both ways and disallows duplicate keys both ways.
/// Access each dictionary with the Forward or Reverse indexers.
/// </summary>
public sealed class BijectiveDictionary<TForward, TReverse>
where TForward : notnull where TReverse : notnull
{
    public sealed class Indexer<TF, TR>(Dictionary<TF, TR> _dict, Indexer<TR, TF> _other) : IDictionary<TF, TR>, ICollection<KeyValuePair<TF, TR>>, IEnumerable<KeyValuePair<TF, TR>>, IEnumerable
        where TF : notnull where TR : notnull
    {

        private readonly Dictionary<TF, TR> dict = _dict;
        private Indexer<TR, TF> other = _other;

        public TR this[TF key]
        {
            get => dict[key];
            set
            {
                other[dict[key]] = key;
                dict[key] = value;
            }
        }

        public ICollection<TF> Keys => dict.Keys;
        public ICollection<TR> Values => dict.Values;
        public int Count => dict.Count;
        public bool IsReadOnly => false;

        public Indexer(Dictionary<TF, TR> _dict) : this(_dict, null!) { }

        internal void UpdateIndexer(Indexer<TR, TF> _other)
        {
            other = _other;
        }

        public void Add(TF key, TR value)
        {
            dict.Add(key, value);
            other.dict.Add(value, key);
        }
        public void Add(KeyValuePair<TF, TR> item) => Add(item.Key, item.Value);

        public void Clear()
        {
            dict.Clear();
            other.dict.Clear();
        }

        public void CopyTo(KeyValuePair<TF, TR>[] array, int arrayIndex)
        {
            foreach (var kv in array) Add(kv);
        }

        public bool Remove(TF key) => other.dict.Remove(this[key]) && dict.Remove(key);
        public bool Remove(KeyValuePair<TF, TR> item) => Remove(item.Key);

        public bool Contains(KeyValuePair<TF, TR> item) => dict.Contains(item);
        public bool ContainsKey(TF key) => dict.ContainsKey(key);
        public bool TryGetValue(TF key, [MaybeNullWhen(false)] out TR value) => dict.TryGetValue(key, out value);
        public IEnumerator<KeyValuePair<TF, TR>> GetEnumerator() => dict.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => dict.GetEnumerator();
    }

    private readonly Dictionary<TForward, TReverse> forwardDict = [];
    private readonly Dictionary<TReverse, TForward> reverseDict = [];

    public Indexer<TForward, TReverse> Forward { get; }
    public Indexer<TReverse, TForward> Reverse { get; }

    public BijectiveDictionary()
    {
        Forward = new Indexer<TForward, TReverse>(forwardDict, null!);
        Reverse = new Indexer<TReverse, TForward>(reverseDict, Forward);
        Forward.UpdateIndexer(Reverse);
    }
}
