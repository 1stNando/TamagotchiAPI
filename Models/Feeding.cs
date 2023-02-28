using System;
using System.Collections.Generic;

namespace TamagotchiAPI.Models
{
    public class Feeding
    {
        public int Id { get; set; }
        public DateTime When { get; set; }
        public int PetId { get; set; }

        public Pet Pet { get; set; }
    }
}