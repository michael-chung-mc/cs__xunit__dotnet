#include "pch.h";

Sphere::Sphere()
{
	origin = Point(0, 0, 0);
	radius = 1.0;
}

bool Sphere::checkEqual(Sphere other)
{
	Comparinator ce = Comparinator();
	return ce.checkTuple(this->origin,other.origin) && this->radius == other.radius;
}

std::vector<Intersection> Sphere::intersect(Ray r)
{
	std::vector<Intersection> intersections;
	Vector sphereToRay = r.origin - Point(0, 0, 0);
	double a = r.direction.dot(r.direction);
	double b = 2 * r.direction.dot(sphereToRay);
	double c = sphereToRay.dot(sphereToRay) - 1;
	double discriminant = pow(b,2) - 4 * a * c;
	if (discriminant < 0) return intersections;
	double intersectOne = (-b - sqrt(discriminant)) / (2 * a);
	double intersectTwo = (-b + sqrt(discriminant)) / (2 * a);
	if (intersectOne < intersectTwo)
	{
		intersections.push_back(Intersection(intersectOne, *this));
		intersections.push_back(Intersection(intersectTwo, *this));
	}
	else
	{
		intersections.push_back(Intersection(intersectTwo, *this));
		intersections.push_back(Intersection(intersectOne, *this));
	}
	return intersections;
}