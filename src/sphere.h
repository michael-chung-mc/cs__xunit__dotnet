#pragma once
#ifndef Sphere_H
#define Sphere_H

#include "material.h"

class Intersection;

class Sphere {
public:
	Point origin;
	double radius;
	Matrix transform;
	Material material;
	Sphere();
	bool checkEqual(Sphere other);
	std::vector<Intersection> intersect(Ray r);
	Sphere& operator=(const Sphere other);
	void setTransform(const Matrix m);
	Vector normal(Point argPoint);
};
#endif