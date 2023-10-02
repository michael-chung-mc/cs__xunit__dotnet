#pragma once
#ifndef SPHERE_H
#define SPHERE_H

#include "tuple.h"
#include "matrix.h"
#include "material.h"
class Intersection;
class Ray;
#include <vector>

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