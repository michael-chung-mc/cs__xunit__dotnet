using System;
using LibRayTracer;

namespace app__ray_tracer
{
    internal class Program
    {
        static void Main()
        {
            
            // int firepower = 10;
            // std::cout << "Firing: " << firepower << std::endl;
            // Simulation sim;
            // sim.fire(firepower);

            RayTracer varRT = new RayTracer();
            varRT.Test();
        }
    }
}