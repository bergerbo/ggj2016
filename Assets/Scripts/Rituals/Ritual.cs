using UnityEngine;
using System.Collections;

public interface Ritual {
    bool ProcessInput(int playerNumber, Player.ActionInput input);
    int[] UnfaithfulPlayers();
}
