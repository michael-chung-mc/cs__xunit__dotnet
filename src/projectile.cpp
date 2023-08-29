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
public:
	Projectile tick(Environment env, Projectile ball)
	{
		Point position = ball.position + ball.velocity;
		Vector velocity = ball.velocity + env.gravity + env.wind;
		return Projectile(position, velocity);
	}

	void fire(int power) {
		//Projectile b = Projectile(Point(0, 1, 0), (Vector(1, 1, 0)*power).normalize());
		Projectile b = Projectile(Point(0, 1, 0), Vector(1, 1, 0) * power);
		Environment e = Environment(Vector(0, -.1, 0), Vector(-.01, 0, 0));
		while (b.position.y >= 0)
		{
			b = tick(e,b);
			std::cout << "x: " << std::to_string(b.position.x) << " y: " << std::to_string(b.position.y) << " z: " << std::to_string(b.position.z) << " w: " << std::to_string(b.position.w) << std::endl;
		}
	}
};
