using System;

public class GameData
{
    public Team team1;
    public Team team2;
    public long timeLeft;
    public bool gameOver;
}

public class Team
{
    public short teamId;
    public short score;
}
