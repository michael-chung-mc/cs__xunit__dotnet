#pragma once
#ifndef FORM_H
#define FORM_H

#include "tuple.h"
#include "matrix.h"
#include "material.h"
#include "ray.h"
class Intersections;
#include <vector>

class Form {
public:
	Point mbrOrigin;
	double mbrRadius;
	Matrix mbrTransform;
	Material mbrMaterial;
	Ray mbrObjectRay;
	Form();
	Form& operator=(const Form other);
	virtual Intersections getIntersections(Ray argRay);
	bool checkEqual(Form other);
	Vector getNormal(Point argPoint);
	void setTransform(const Matrix m);
};

class Sphere : public Form {
public:
	Sphere();
	Intersections getIntersections(Ray argRay);
};
#endif