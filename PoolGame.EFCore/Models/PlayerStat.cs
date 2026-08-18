using System;
using System.Collections.Generic;

namespace PoolGame.EFCore.Models;

public partial class PlayerStat
{
    public int GameId { get; set; }

    public int UserId { get; set; }

    public bool IsWinner { get; set; }

    public int? ShotsMade { get; set; }

    public int? ShotsAttempted { get; set; }

    public int? HandBalls { get; set; }

    public int? Fouls { get; set; }

    public int? BestStreak { get; set; }

    public int StatId { get; set; }

    public virtual Game Game { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
