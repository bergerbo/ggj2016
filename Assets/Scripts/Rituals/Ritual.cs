using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public interface Ritual {
    bool ProcessInput(PlayerIndex playerIndex, Player.ActionInput input);
    int[] UnfaithfulPlayers();
}
