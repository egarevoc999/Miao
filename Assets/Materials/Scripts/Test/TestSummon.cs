using UnityEngine;
using UnityEngine.Tilemaps;

public class TestSummon : MonoBehaviour
{
    public Tilemap targetTilemap;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Entity entity = new Entity();
        Card card = new Card();
        card.type = 1111;
        entity.card = card;
        entity.loc = new Location(9, 10, 0);

        Summon summon = new Summon(entity, targetTilemap);
        summon.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
