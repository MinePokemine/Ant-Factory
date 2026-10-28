using UnityEngine;

public class BackForthAnt : Ant {
    Tunnel previous = Tunnel.NULL;
    protected override void UniqueTick(Tunnels tunnels) {
        Tunnel[] adjacent = tunnels.Touching(position);
        Debug.Log(adjacent.Length);
        Debug.Log(previous);
        if (previous.dir == Direction.INVALID) {
            if (adjacent.Length == 0) {
                Debug.Log("No Adjacent Tunnels");
            } else {
                Debug.Log("Moving along " + adjacent[0]);
                (bool over, bool error) = MoveAlong(adjacent[0], true);
                if (error) {
                    Debug.Log("Error moving along tunnel " + adjacent[0]);
                } else {
                    previous = adjacent[0];
                }
            }
        }
        else if (adjacent.Length == 0) {
            Debug.Log("No Adjacent Tunnels");
        }
        else if (adjacent.Length != 2) {
            Debug.Log("Moving back along " + previous);

            (bool over, bool error) = MoveAlong(previous, true);
            if (error) {
                Debug.Log("Error moving along tunnel " + previous);
            }
        }
        else if (adjacent[0].Has(previous.Other(position))) {
            Debug.Log("0Moving along " + adjacent[1]);

            (bool over, bool error) = MoveAlong(adjacent[1], true);
            if (error) {
                Debug.Log("Error moving along tunnel " + adjacent[1]);
            } else {
                previous = adjacent[1];
            }
        }
        else if (adjacent[1].Has(previous.Other(position))) {
            Debug.Log("1Moving along " + adjacent[0]);

            (bool over, bool error) = MoveAlong(adjacent[0], true);
            if (error) {
                Debug.Log("Error moving along tunnel " + adjacent[0]);
            } else {
                previous = adjacent[0];
            }
        }
        else {
            Debug.Log("Nothing");
        }
    }
}
