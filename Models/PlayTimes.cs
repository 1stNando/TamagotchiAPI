using System;
using System.Collections.Generic;

namespace TamagotchiAPI.Models
{
    public class PlayTimes
    {
        public int Id { get; set; }
        public DateTime When { get; set; } = DateTime.Now;
        public int PetId { get; set; }

        public Pet Pet { get; set; }
    }
}