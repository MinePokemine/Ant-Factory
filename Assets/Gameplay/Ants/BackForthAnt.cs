using UnityEngine;

public class BackForthAnt : Ant {
    Tunnel previous = Tunnel.NULL;
    protected override void UniqueTick(Tunnels tunnels) {
        Tunnel[] adjacent = tunnels.Touching(position);
        Debug.Log(adjacent.Length);
        if (previous.dir == Direction.INVALID) {
            if (adjacent.Length == 0) {
                Debug.Log("No Adjacent Tunnels");
            } else {
                (bool over, bool error) = MoveAlong(adjacent[0]);
                if (error) {
                    Debug.Log("Error moving along tunnel " + adjacent[0]);
                }
            }
        }
        else if (adjacent.Length > 2 || adjacent.Length == 1) {
            Debug.Log("Moving back along " + previous);

            (bool over, bool error) = MoveAlong(previous);
            if (error) {
                Debug.Log("Error moving along tunnel " + previous);
            }
        }
        else if (adjacent.Length == 0) {
            Debug.Log("No Adjacent Tunnels");
        }
        else if (adjacent[0].pos1 == previous.pos1 || adjacent[0].pos2 == previous.pos1
                || adjacent[0].pos1 == previous.pos2 || adjacent[0].pos2 == previous.pos2 ) {
            Debug.Log("Moving along " + adjacent[0]);

            (bool over, bool error) = MoveAlong(adjacent[0]);
            if (error) {
                Debug.Log("Error moving along tunnel " + adjacent[0]);
            }
        }
        else if (adjacent[1].pos1 == previous.pos1 || adjacent[1].pos2 == previous.pos1
                || adjacent[1].pos1 == previous.pos2 || adjacent[1].pos2 == previous.pos2 ) {
            Debug.Log("Moving along " + adjacent[1]);

            (bool over, bool error) = MoveAlong(adjacent[1]);
            if (error) {
                Debug.Log("Error moving along tunnel " + adjacent[1]);
            }
        }
    }
}
