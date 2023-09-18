#pragma once
#ifndef Sphere_H
#define Sphere_H

class Intersection;

class Sphere {
public:
	Point origin;
	double radius;
	Sphere();
	bool checkEqual(Sphere other);
	std::vector<Intersection> intersect(Ray r);
};
#endif