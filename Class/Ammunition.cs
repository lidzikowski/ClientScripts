using UnityEngine;

public class Ammunition
{
    public string ammunition_name { get; set; }
    public double ammunition_multiplier { get; set; }
    public GameObject model { get; set; }
    
    public Ammunition(T_Ammunition ammunition, GameObject _model)
    {
        ammunition_name = ammunition.ammunition_name;
        ammunition_multiplier = ammunition.ammunition_multiplier;
        model = _model;
    }
}