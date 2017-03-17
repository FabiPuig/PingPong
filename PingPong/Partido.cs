using System;

namespace PingPong
{
    class Partido
    {
        public String Jugador1 { get; set; }
        public String Jugador2 { get; set; }
        public int PuntosJ1 { get; set; }
        public int PuntosJ2 { get; set; }

        public Partido( String jugador1, String jugador2)
        {
            Jugador1 = jugador1;
            Jugador2 = jugador2;
        }
    }
}
