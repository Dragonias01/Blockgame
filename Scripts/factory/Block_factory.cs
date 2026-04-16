using UnityEngine;

public class Block_factory : Factory
{
    //konstruktor
    public Block_factory() : base() { }
    public override GameObject Generate(string type)
    {
        switch (type)
        {
            case "default1":
                return new Default_block();
            default:
                return null;
        }
    }
}
