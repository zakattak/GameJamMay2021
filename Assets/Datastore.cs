using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Datastore : MonoBehaviour
{
    public Dictionary<CropType, IntReactiveProperty> seedInventory = new Dictionary<CropType, IntReactiveProperty>();
    public List<CropType> storeInventory = new List<CropType>() {
        CropTemplates.Radish, CropTemplates.Potato
    };

    public List<GameObject> gardenGrid;

    public Garden garden;

    public MouseController mouseController;

    public Tetromino heldShape;
    public Crop heldCrop;

    public IntReactiveProperty mouseState = new IntReactiveProperty(0);

    public Dictionary<string, Color> colors = new Dictionary<string, Color>() {
        {"GREEN", new Color(125/255f, 197/255f, 94/255f)}, {"DARK_GREEN", new Color(121/255f, 191/255f, 92/255f)},
        {"GROUND", new Color(218/255f, 169/255f, 122/255f)}, {"WATER", new Color(67/255f, 151/255f, 213/255f)}
    };

    public IntReactiveProperty turnCount = new IntReactiveProperty(0);


    /*

        .o88b. db    db .d8888. d888888b  .d88b.  .88b  d88. d88888b d8888b.       db      d888888b d8b   db d88888b
        d8P  Y8 88    88 88'  YP `~~88~~' .8P  Y8. 88'YbdP`88 88'     88  `8D       88        `88'   888o  88 88'
        8P      88    88 `8bo.      88    88    88 88  88  88 88ooooo 88oobY'       88         88    88V8o 88 88ooooo
        8b      88    88   `Y8b.    88    88    88 88  88  88 88~~~~~ 88`8b         88         88    88 V8o88 88~~~~~
        Y8b  d8 88b  d88 db   8D    88    `8b  d8' 88  88  88 88.     88 `88.       88booo.   .88.   88  V888 88.
        `Y88P' ~Y8888P' `8888Y'    YP     `Y88P'  YP  YP  YP Y88888P 88   YD       Y88888P Y888888P VP   V8P Y88888P

    */

    public struct Order {
        public GameObject customer;
        public List<System.Tuple<GameObject, CropType>> orderItems;
    }

    public List<Order> pendingOrders = new List<Order>();
}
