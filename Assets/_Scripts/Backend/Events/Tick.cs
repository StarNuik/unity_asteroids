using System.Collections;
using System.Collections.Generic;
using Asteroids;
using Asteroids.Lib;
using UnityEngine;

public class Tick
{
	public SessionState State { get; private set; }
	public IEventStream ServerStream { get; private set; }
	public IEventStream OutStream { get; private set; }

	public static Tick New(SessionState state, IEventStream serverStream, IEventStream outStream)
	{
		return new()
		{
			State = state,
			ServerStream = serverStream,
			OutStream = outStream,
		};
	}
}
