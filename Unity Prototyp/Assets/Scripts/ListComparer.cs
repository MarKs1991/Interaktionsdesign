using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public  class ListComparer : IComparable
{
    public int CompareTo(object obj)
    {
        throw new NotImplementedException();
    }

    // Start is called before the first frame update
    public void findIndexes(List<Vector2Int> v, int i)
    {
        if (i == 0)
        {


            var sorted = v
                .Select((x, i) => new KeyValuePair<Vector2Int, int>(x, i))
                .OrderBy(x => x.Key)
                .ToList();

            List<Vector2Int> B = sorted.Select(x => x.Key).ToList();
            List<int> idx = sorted.Select(x => x.Value).ToList();

            //Array.Sort<int>(i, (a, b) => v[i].CompareTo(v[i]));
        }
    }
}
