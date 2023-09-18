#pragma once
#ifndef Sphere_H
#define Sphere_H
class Sphere {
public:
	Point origin;
	double radius;
	Sphere();
	bool checkEqual(Sphere other);
	std::vector<double> intersect(Ray r);
};
#endif