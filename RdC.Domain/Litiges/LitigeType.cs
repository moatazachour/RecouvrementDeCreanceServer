using RdC.Domain.Abstrations;
using System.Text.Json.Serialization;

namespace RdC.Domain.Litiges
{
    public sealed class LitigeType : Entity
    {
        private LitigeType(
            int id,
            string name,
            string description)
            : base(id)
        {
            Name = name;
            Description = description;
        }

        public string Name { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        public List<Litige> Litiges { get; private set; }

        public static LitigeType Create(
            string name,
            string description)
        {
            LitigeType type = new LitigeType(
                id: 0,
                name: name,
                description: description);

            return type;
        }


        private LitigeType() { }
    }
}
