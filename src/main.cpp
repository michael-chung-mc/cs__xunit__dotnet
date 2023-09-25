#include "pch.h"
#include "projectile.cpp"

void shadowTracer()
{
	Point pov = Point(0,0,-5);
	double wallWidth = 10;
	double wallZ = 10;
	double wallXY = wallWidth/2;
	double pixels = 100;
	double pixelSize = wallWidth/pixels;
	Canvas canvas = Canvas(pixels,pixels);
	Color hitColor = Color(1,0,0);

	Sphere obj = Sphere();

	for (int i = 0; i <= pixels; i++)
	{
		double worldY = wallXY - pixelSize * i;
		for (int j = 0; j <= pixels; j++)
		{
			double worldX = -wallXY + pixelSize * j;
			Point position = Point(worldX, worldY, wallZ);
			//std::cout << worldX << worldY << wallZ << std::endl;
			Ray r = Ray(pov, (position-pov).normalize());
			std::vector<Intersection> xs = obj.intersect(r);
			if (xs.size() != 0)
			{
				canvas.setPixel(i,j,hitColor);
			}
		}
	}
	canvas.save();
}

int main(int argc, char **argv)
{
    // ::testing::InitGoogleTest( &argc, argv);
    // return RUN_ALL_TESTS();

	// double arr[] = { 2, 0, 0, 0, 0, 1, 0, 0, 0,0,1,0 ,0 ,0 ,0, 1 };
	// Matrix m = Matrix(4, 4, arr);
	// Tuple t = Tuple(2, 2, 2, 2);
	// Tuple x = m * t;

	// int firepower = 10;
	// std::cout << "Firing: " << firepower << std::endl;
	// Simulation sim;
	// sim.fire(firepower);

	shadowTracer();

	return 0;
}