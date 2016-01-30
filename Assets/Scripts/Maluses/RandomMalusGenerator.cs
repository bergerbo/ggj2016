using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public class RandomMalusGenerator {

    private Malus[] maluses;
    private System.Random rng;

    public RandomMalusGenerator(Malus[] maluses)
    {
        rng = new System.Random();
        this.maluses = maluses;
    }

    public Malus GetRandomMalus()
    {
        return maluses[rng.Next(0, maluses.Length)];
    }
}
