using UnityEngine;

public class Box
{
    public int box_id { get; set; }
    public string box_type { get; set; }
    public GameObject model { get; set; }
    
    public Box(T_Box box, GameObject _model)
    {
        box_id = box.box_id;
        box_type = box.box_type;
        model = _model;
    }
}