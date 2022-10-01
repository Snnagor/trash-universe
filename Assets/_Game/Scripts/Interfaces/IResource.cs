using UnityEngine;

interface IResource
{
    public string NameRecource { get;}

    public Sprite IconResource { get;}
   
    public ProductData[] Products { get; }

    public float TimeRecycling { get; }

    public int Count { get; set; }

    public void Execute();
}
