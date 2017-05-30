using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockList : MonoBehaviour {

    public static List<Block> Blocks = new List<Block>();

    public void Awake()
    {
        //Dirt Block
        Block dirt = new Block("Dirt", false, 2, 15);
        Blocks.Add(dirt);

        //Grass Block
        Blocks.Add(new Block("Grass", false, 0, 15, 3, 15, 2, 15));
    }
}