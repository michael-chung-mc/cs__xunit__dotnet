#pragma once
#ifndef Intersection_H
#define Intersection_H

class Intersection
{
public:
	double time;
	Sphere object;
	std::vector<Intersection> intersections;
	Intersection(double time, Sphere s);
	void intersect(double time, Sphere s);
};

#endif