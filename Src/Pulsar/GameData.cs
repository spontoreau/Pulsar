using System;
using System.Collections;
using System.Collections.Generic;

namespace Pulsar
{
	/// <summary>
	/// Game data.
	/// </summary>
	public sealed class GameData : IDictionary<string, object>
	{
		private Dictionary<string,object> _dictionary = new Dictionary<string,object>();

		/// <summary>
		/// Initializes a new instance of the <see cref="Pulsar.GameData"/> class.
		/// </summary>
		internal GameData ()
		{
		}

		/// <summary>
		/// Add the specified key and value.
		/// </summary>
		/// <param name="key">Key.</param>
		/// <param name="value">Value.</param>
		public void Add (string key, object value)
		{
			_dictionary.Add (key, value);
		}

		/// <Docs>The key to locate in the current instance.</Docs>
		/// <para>Determines whether the current instance contains an entry with the specified key.</para>
		/// <summary>
		/// Containses the key.
		/// </summary>
		/// <returns><c>true</c>, if key was containsed, <c>false</c> otherwise.</returns>
		/// <param name="key">Key.</param>
		public bool ContainsKey(string key)
		{
			return _dictionary.ContainsKey(key);
		}

		/// <Docs>The item to remove from the current collection.</Docs>
		/// <para>Removes the first occurrence of an item from the current collection.</para>
		/// <summary>
		/// Remove the specified key.
		/// </summary>
		/// <param name="key">Key.</param>
		public bool Remove (string key)
		{
			return _dictionary.Remove(key);
		}

		/// <Docs>To be added.</Docs>
		/// <summary>
		/// To be added.
		/// </summary>
		/// <remarks>To be added.</remarks>
		/// <returns><c>true</c>, if get value was tryed, <c>false</c> otherwise.</returns>
		/// <param name="key">Key.</param>
		/// <param name="value">Value.</param>
		public bool TryGetValue (string key, out object value)
		{
			return TryGetValue (key, out value);
		}

		/// <summary>
		/// Gets or sets the <see cref="Pulsar.GameData"/> at the specified index.
		/// </summary>
		/// <param name="index">Index.</param>
		public object this [string index] 
		{
			get 
			{
				return _dictionary[index];
			}
			set 
			{
				_dictionary[index] = value;
			}
		}

		/// <summary>
		/// Gets the keys.
		/// </summary>
		/// <value>The keys.</value>
		public ICollection<string> Keys 
		{
			get 
			{
				return _dictionary.Keys;
			}
		}

		/// <summary>
		/// Gets the values.
		/// </summary>
		/// <value>The values.</value>
		public ICollection<object> Values 
		{
			get 
			{
				return _dictionary.Values;
			}
		}

		/// <Docs>The item to add to the current collection.</Docs>
		/// <para>Adds an item to the current collection.</para>
		/// <remarks>To be added.</remarks>
		/// <exception cref="System.NotSupportedException">The current collection is read-only.</exception>
		/// <summary>
		/// Add the specified item.
		/// </summary>
		/// <param name="item">Item.</param>
		public void Add (KeyValuePair<string, object> item)
		{
			_dictionary.Add (item.Key, item.Value);
		}

		/// <summary>
		/// Clear this instance.
		/// </summary>
		public void Clear ()
		{
			_dictionary.Clear();
		}

		/// <Docs>The object to locate in the current collection.</Docs>
		/// <para>Determines whether the current collection contains a specific value.</para>
		/// <summary>
		/// Contains the specified item.
		/// </summary>
		/// <param name="item">Item.</param>
		public bool Contains (KeyValuePair<string, object> item)
		{
			return _dictionary.ContainsKey(item.Key) && _dictionary.ContainsValue(item.Value);
		}

		/// <summary>
		/// Copies to.
		/// </summary>
		/// <param name="array">Array.</param>
		/// <param name="arrayIndex">Array index.</param>
		public void CopyTo (KeyValuePair<string, object>[] array, int arrayIndex)
		{
		}

		/// <summary>
		/// Copy the specified gameDate, array and arrayIndex.
		/// </summary>
		/// <param name="gameDate">Game date.</param>
		/// <param name="array">Array.</param>
		/// <param name="arrayIndex">Array index.</param>
		private void Copy(GameData gameDate, KeyValuePair<string, object>[] array, int arrayIndex)
		{
			if (array == null)
				throw new ArgumentNullException ("array");

			if (arrayIndex < 0 || arrayIndex > array.Length)
				throw new ArgumentOutOfRangeException ("arrayIndex");

			if ((array.Length - arrayIndex) < gameDate.Count)
				throw new ArgumentOutOfRangeException ("array", "Array not large enough");

			foreach (var kv in _dictionary)
				array [arrayIndex++] = kv;
		}

		/// <Docs>The item to remove from the current collection.</Docs>
		/// <para>Removes the first occurrence of an item from the current collection.</para>
		/// <summary>
		/// Remove the specified item.
		/// </summary>
		/// <param name="item">Item.</param>
		public bool Remove (KeyValuePair<string, object> item)
		{
			return _dictionary.Remove (item.Key);
		}

		/// <summary>
		/// Gets the count.
		/// </summary>
		/// <value>The count.</value>
		public int Count 
		{
			get 
			{
				return _dictionary.Count;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance is read only.
		/// </summary>
		/// <value><c>true</c> if this instance is read only; otherwise, <c>false</c>.</value>
		public bool IsReadOnly 
		{
			get 
			{
				return false;
			}
		}

		/// <summary>
		/// Gets the enumerator.
		/// </summary>
		/// <returns>The enumerator.</returns>
		public IEnumerator<KeyValuePair<string, object>> GetEnumerator ()
		{
			return _dictionary.GetEnumerator ();
		}

		/// <summary>
		/// Gets the enumerator.
		/// </summary>
		/// <returns>The enumerator.</returns>
		IEnumerator IEnumerable.GetEnumerator ()
		{
			return _dictionary.GetEnumerator ();
		}
	}
}

