using System;
using System.Collections.Generic;

namespace PoolGame.EFCore.Models;

public partial class Game
{
    public int GameId { get; set; }

    public DateTime GameDate { get; set; }

    public bool GameIsDraw { get; set; }

    public virtual ICollection<PlayerStat> PlayerStats { get; set; } = new List<PlayerStat>();

    public virtual ICollection<StatsPerTurn> StatsPerTurns { get; set; } = new List<StatsPerTurn>();
}
