﻿using System;
using System.Runtime.Serialization;

namespace PingPong
{
    class Jugador
    {
        public int Id { get; set; }
        public String Nombre{ get; set; }
        public String Image { get; set; }
        public int Jugados { get; set; }
        public int Puntos { get; set; }

        public Jugador(String nombre, String image)
        {
            Nombre = nombre;
            Image = image;
        }
    }
}
