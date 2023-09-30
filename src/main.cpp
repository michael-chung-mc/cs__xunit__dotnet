#include "pch.h"
#include <iostream>

class Projectile {
public:
	Projectile(Point pos, Vector vel)
	{
		position = pos;
		velocity = vel;
	}
	Point position;
	Vector velocity;
};

class Environment {
public:
	Environment(Vector g, Vector w)
	{
		gravity = g;
		wind = w;
	}
	Vector gravity;
	Vector wind;
};

class Simulation {
private:
	Canvas c = Canvas(getPPMWidth(), getPPMHeight());
public:
	Projectile tick(Environment env, Projectile ball)
	{
		Point position = ball.position + ball.velocity;
		Vector velocity = ball.velocity + env.gravity + env.wind;
		return Projectile(position, velocity);
	}
	void fire(int power) {
		c.fill(Color(1, 1, 1));
		//Projectile b = Projectile(Point(0, 1, 0), (Vector(1, 1, 0)*power).normalize());
		Projectile b = Projectile(Point(0, 1, 0), (Vector(1, 1, 0).normalize()) * power);
		Environment e = Environment(Vector(0, -.1, 0), Vector(-.01, 0, 0));
		while (b.position.y >= 0)
		{
			b = tick(e,b);
			std::cout << "x: " << std::to_string(b.position.x) << " y: " << std::to_string(b.position.y) << " z: " << std::to_string(b.position.z) << " w: " << std::to_string(b.position.w) << std::endl;
			Color white = Color(0, 0, 0);
			c.setPixel((int)b.position.x, getPPMHeight() - b.position.y, white);
		}
		c.save();
	}
};

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

//int main(int argc, char **argv)
//{
//     // ::testing::InitGoogleTest( &argc, argv);
//     // return RUN_ALL_TESTS();

// 	// double arr[] = { 2, 0, 0, 0, 0, 1, 0, 0, 0,0,1,0 ,0 ,0 ,0, 1 };
// 	// Matrix m = Matrix(4, 4, arr);
// 	// Tuple t = Tuple(2, 2, 2, 2);
// 	// Tuple x = m * t;

// 	// int firepower = 10;
// 	// std::cout << "Firing: " << firepower << std::endl;
// 	// Simulation sim;
// 	// sim.fire(firepower);

// 	shadowTracer();

// 	return 0;
//}