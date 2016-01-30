using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public class RandomMalusGenerator {

    private Type[] malusTypes;
    private System.Random rng;

    public RandomMalusGenerator(Malus[] maluses)
    {
        rng = new System.Random();
        malusTypes = maluses.Select(m => m.GetType()).ToArray();
    }

    public Type GetRandomMalus()
    {
        return malusTypes[rng.Next(0, malusTypes.Length)];
    }
}
