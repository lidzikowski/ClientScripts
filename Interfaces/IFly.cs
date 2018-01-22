using UnityEngine;

public interface IFly
{
    int speed { get; set; }

    Vector3 position { get; set; }
    Vector3 new_position { get; set; }
    GameObject object_model { get; set; }
    GameObject object_ship_model { get; set; }

    void ChangePosition(float x, float y);
    void FlyShip();
    void RotateShip();
}