using System.Numerics;
using UnityEngine;

public abstract class Block : MonoBehaviour
{

    public Block()
    {
    }
    protected GameObject block;

    public GameObject GetBlock()
    {
        return block;
    }

    public abstract void generate();
}