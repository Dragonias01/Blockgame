using UnityEngine;

public class Block_factory : Factory
{
    //konstruktor
    public Block_factory() : base() { }
    public override GameObject generate(string type)
    {
        switch (type)
        {
            case "default1":
                Default_block block = new Default_block();
                block.generate();
                return block.GetPlattform();
            default:
                return null;
        }
    }
}
