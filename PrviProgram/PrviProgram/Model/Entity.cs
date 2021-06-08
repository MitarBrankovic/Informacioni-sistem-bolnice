using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Entity
    {
        public Entity() { }
        public Entity(int id)
        {
            ID = id;
        }
        public int ID { get; set; }
    }
}
