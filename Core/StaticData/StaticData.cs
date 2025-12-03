using Core.Domain;

namespace Core.StaticData;

public static class StaticData
{
    public const string ZONEA = "ZoneA";
    public const string ZONEB = "ZoneB";
    public const string ZONEC = "ZoneC";

    public static readonly List<DistanceMap> DistanceMap = new List<DistanceMap>
    {
        new DistanceMap(ZONEA, ZONEA, 0),
        new DistanceMap(ZONEA, ZONEB, 3),
        new DistanceMap(ZONEA, ZONEC, 6),
        new DistanceMap(ZONEB, ZONEA, 3),
        new DistanceMap(ZONEB, ZONEB, 0),
        new DistanceMap(ZONEB, ZONEC, 8),
        new DistanceMap(ZONEC, ZONEC, 0),
    };

    public static readonly List<Store> Stores = new List<Store>
    {
        new Store(
            "1",
            ZONEA,
            Items.Where(x => x.Id is "4" or "5" or "6").ToList()
        ),
        new Store(
            "2",
            ZONEB,
            Items.Where(x => x.Name is "4" or "5").ToList()
        ),
        new Store(
            "3",
            ZONEC,
            Items.Where(x => x.Name is "7" or "8").ToList()
        )
    };

    public static readonly List<Item> Items = new List<Item>
    {
        new Item("1", "Notebook", "", 15),
        new Item("2", "Keyboard", "", 50),
        new Item("3", "Mouse", "", 25),
        new Item("4", "Monitor", "", 75),
        new Item("5", "Milk", "", 5.70, Category.DAIRY),
        new Item("6", "Eggs", "", 5.70, Category.BAKERY),
        new Item("7", "Bread", "", 1.30, Category.BAKERY),
        new Item("8", "Juice", "", 8.99, Category.BEVERAGES),
    };
}