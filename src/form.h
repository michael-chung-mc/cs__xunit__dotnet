#pragma once
#ifndef FORM_H
#define FORM_H

#include "tuple.h"
#include "matrix.h"
#include "material.h"
class Intersection;
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
	bool checkEqual(Sphere other);
	std::vector<Intersection> intersect(Ray r);
	Sphere& operator=(const Sphere other);
	void setTransform(const Matrix m);
	Vector normal(Point argPoint);
};
#endif