#pragma once
#ifndef FORM_H
#define FORM_H

#include "tuple.h"
#include "matrix.h"
#include "material.h"
class Intersections;
class Ray;
#include <vector>

class Form {
public:
	Matrix mbrTransform;
	Material mbrMaterial;
	Form();
};

class Sphere : public Form {
public:
	Point origin;
	double radius;
	Sphere();
	Sphere& operator=(const Sphere other);
	bool checkEqual(Sphere other);
	Intersections getIntersections(Ray argRay);
	void setTransform(const Matrix m);
	Vector normal(Point argPoint);
};
#endif