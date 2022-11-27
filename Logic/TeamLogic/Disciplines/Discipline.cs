using Data.Enums;

namespace Logic.TeamLogic.Disciplines
{
    public abstract class Discipline
    {
        public string Name { get; set; }

        public int getDisciplineId()
        {
            var disciplineId = (EDiscipline)Enum.Parse(typeof(EDiscipline), this.Name);
            return (int)disciplineId;
        }
    }
}
