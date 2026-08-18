using System;
using System.Collections.Generic;

namespace PoolGame.EFCore.Models;

public partial class StatsPerTurn
{
    public int StatId { get; set; }

    public int? PlayerId { get; set; }

    public int? GameId { get; set; }

    public int? ShotsMade { get; set; }

    public int? ShotsAttempted { get; set; }

    public int? HandBalls { get; set; }

    public int? Fouls { get; set; }

    public virtual Game? Game { get; set; }

    public virtual User? Player { get; set; }
}
