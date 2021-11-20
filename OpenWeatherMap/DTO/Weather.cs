namespace DTO
{
    public class Weather
    {
        public ulong Id { get; set; }
        public string Main { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }

        public override string ToString()
        {
            return $"\t{Main}";
        }
    }
}