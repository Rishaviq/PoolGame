using System;
using System.Collections.Generic;

namespace PoolGame.EFCore.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string? ProfileName { get; set; }

    public string UserPassword { get; set; } = null!;

    public virtual ICollection<PlayerStat> PlayerStats { get; set; } = new List<PlayerStat>();

    public virtual ICollection<StatsPerTurn> StatsPerTurns { get; set; } = new List<StatsPerTurn>();
}
