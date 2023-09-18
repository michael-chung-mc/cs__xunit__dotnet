#include "pch.h";

Intersection::Intersection(double t, Sphere s)
{
	object = s;
	time = t;
	intersections.push_back(*this);
}

void Intersection::intersect(double t, Sphere s)
{
	intersections.push_back(Intersection(t, s));
}