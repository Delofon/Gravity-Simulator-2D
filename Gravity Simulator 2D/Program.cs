using System;

namespace GravitySimulator2D
{
    static class Program
    {
        static void Main(string[] args)
        {
            using (GravitySimulator2D gravitySimulator2D = new GravitySimulator2D())
            {
                gravitySimulator2D.Run();
            }
        }
    }
}

