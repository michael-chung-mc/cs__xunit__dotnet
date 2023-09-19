#pragma once
#ifndef Intersection_H
#define Intersection_H

class Intersection
{
public:
	double time;
	Sphere object;
	std::vector<Intersection> intersections;
	Intersection();
	Intersection(double time, Sphere s);
	bool checkEqual(Intersection other);
	void intersect(double time, Sphere s);
	Intersection hit();
};

#endif