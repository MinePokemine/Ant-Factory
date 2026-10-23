using UnityEngine;

public class BackForthAnt : Ant {
    Tunnel previous;
    public override void Tick() {
        Tunnel[] adjacent = tunnels.Touching(position);
        if (adjacent.Length > 2) {
            MoveAlong(previous);
        }
        else if (adjacent[0].pos1 == previous.pos1 || adjacent[0].pos2 == previous.pos1
                || adjacent[0].pos1 == previous.pos2 || adjacent[0].pos2 == previous.pos2 ) {
            MoveAlong(adjacent[0]);
        }
        else if (adjacent[1].pos1 == previous.pos1 || adjacent[1].pos2 == previous.pos1
                || adjacent[1].pos1 == previous.pos2 || adjacent[1].pos2 == previous.pos2 ) {
            MoveAlong(adjacent[1]);
        }
    }
}
