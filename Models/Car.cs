using API_Lab_1.Validators;

namespace API_Lab_1.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public long Price { get; set; }

        [DateMustBeInPast]
        public DateTime ProductionDate { get; set; }

        public string Type { get; set; }



        public Car(int id,
        string name,
        string model,
        long price,
        DateTime productionDate,
        string type)
        {
            Id = id;
            Name = name;
            Model = model;
            Price = price;
            ProductionDate = productionDate;
            Type = type;
        }
    }
}
