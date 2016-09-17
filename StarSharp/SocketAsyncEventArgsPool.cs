﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace StarSharp
{
	/// <summary>
	/// Based on example from http://msdn2.microsoft.com/en-us/library/system.net.sockets.socketasynceventargs.socketasynceventargs.aspx
	/// Represents a collection of reusable SocketAsyncEventArgs objects.  
	/// </summary>
	internal sealed class SocketAsyncEventArgsPool
	{
		/// <summary>
		/// Pool of SocketAsyncEventArgs.
		/// </summary>
		Stack<SocketAsyncEventArgs> pool;

		/// <summary>
		/// Initializes the object pool to the specified size.
		/// </summary>
		/// <param name="capacity">Maximum number of SocketAsyncEventArgs objects the pool can hold.</param>
		internal SocketAsyncEventArgsPool(Int32 capacity)
		{
			this.pool = new Stack<SocketAsyncEventArgs>(capacity);
		}

		/// <summary>
		/// Removes a SocketAsyncEventArgs instance from the pool.
		/// </summary>
		/// <returns>SocketAsyncEventArgs removed from the pool.</returns>
		internal SocketAsyncEventArgs Pop()
		{
			lock (this.pool)
			{
				if (this.pool.Count > 0)
				{
					return this.pool.Pop();
				}
				else
				{
					return null;
				}
			}
		}

		/// <summary>
		/// Add a SocketAsyncEventArg instance to the pool. 
		/// </summary>
		/// <param name="item">SocketAsyncEventArgs instance to add to the pool.</param>
		internal void Push(SocketAsyncEventArgs item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("Items added to a SocketAsyncEventArgsPool cannot be null");
			}
			lock (this.pool)
			{
				this.pool.Push(item);
			}
		}
	}
}
