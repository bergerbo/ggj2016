using UnityEngine;
using System.Collections;
using XInputDotNetPure;
using System.Collections.Generic;

public abstract class Ritual {
	public float duration;
    public abstract bool ProcessInput(PlayerIndex playerIndex, Player.ActionName input);
    public abstract IEnumerable<PlayerIndex> UnfaithfulPlayers();
    public abstract void Explain();
}
