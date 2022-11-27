using Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.TeamLogic.TeamTypes
{
    public abstract class Team
    {
        public string Type { get; }

        public int getTeamTypeId()
        {
            var disciplineId = (ETeam)Enum.Parse(typeof(ETeam), this.Type);
            return (int)disciplineId;
        }
    }
}
