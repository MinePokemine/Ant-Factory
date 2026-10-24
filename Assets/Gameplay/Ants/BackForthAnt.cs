using UnityEngine;

public class BackForthAnt : Ant {
    Tunnel previous;
    protected override void UniqueTick(Tunnels tunnels) {
        Tunnel[] adjacent = tunnels.Touching(position);
        if (adjacent.Length > 2 || adjacent.Length == 1) {
            Debug.Log("Moving Back");
            MoveAlong(previous);
        }
        else if (adjacent.Length == 0) {
            Debug.Log("No Adjacent Tunnels");
        }
        else if (adjacent[0].pos1 == previous.pos1 || adjacent[0].pos2 == previous.pos1
                || adjacent[0].pos1 == previous.pos2 || adjacent[0].pos2 == previous.pos2 ) {
            Debug.Log("Moving");
            MoveAlong(adjacent[0]);
        }
        else if (adjacent[1].pos1 == previous.pos1 || adjacent[1].pos2 == previous.pos1
                || adjacent[1].pos1 == previous.pos2 || adjacent[1].pos2 == previous.pos2 ) {
            Debug.Log("Moving");
            MoveAlong(adjacent[1]);
        }
    }
}
