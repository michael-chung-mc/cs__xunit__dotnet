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
	Point origin;
	double radius;
	Matrix mbrTransform;
	Material mbrMaterial;
	Form();
	Form& operator=(const Form other);
	Intersections getIntersections(Ray argRay);
	bool checkEqual(Form other);
	Vector getNormal(Point argPoint);
	void setTransform(const Matrix m);
};

class Sphere : public Form {
public:
	Sphere();
};
#endif