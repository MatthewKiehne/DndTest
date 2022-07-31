using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Actor
{
    public List<Feature> Features { get; }

    public Actor() {
        Features = new List<Feature>();
    }
}
