
public class Player : Parent
{
    #region Identificators
    public int user_id { get; set; }
    public string username { get; set; }
    #endregion

    #region Constructors
    public Player(TempPlayer player):base(player)
    {
        user_id = player.user_id;
        username = player.username;
    }
    #endregion
}