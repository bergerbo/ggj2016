using UnityEngine;
using System.Collections;
using XInputDotNetPure;
using System.Collections.Generic;

public interface Ritual {
    bool ProcessInput(PlayerIndex playerIndex, Player.ActionInput input);
    IEnumerable<PlayerIndex> UnfaithfulPlayers();
    void Explain();
}
