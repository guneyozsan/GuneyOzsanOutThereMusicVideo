using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathUtility {

	public static int ClosestCubeRoot(int source, bool inclusive)
    {
        int root = 0;
        float rootPower = Mathf.Pow(root, 3);

        while (rootPower < source)
        {
            root++;
            rootPower = Mathf.Pow(root, 3);
        }

        if (inclusive)
        {
            return root;
        }
        else
        {
            return root - 1;
        }
    }

}
