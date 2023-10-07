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
	std::unique_ptr<Matrix> mbrTransform;
	std::unique_ptr<Material> mbrMaterial;
	Ray mbrObjectRay;
	Form();
	Form(const Form& other);
	virtual ~Form();
	Form& operator=(const Form other);
	virtual Intersections getIntersections(Ray argRay);
	virtual Intersections getIntersectionsLocal(Ray argRay);
	virtual bool checkEqual(Form other);
	virtual Vector getNormal(Point argPoint);
	virtual Vector getNormalLocal(Point argPoint);
	Color getColor(PointSource argLighting, Point argPosition, Vector argEye, Vector argNormal, bool argInShadow);
	Color getColorShaded(PointSource argLighting, Point argPosition, Vector argEye, Vector argNormal, bool argInShadow);
	Color getColorLocal(Point argPosition);
	void setTransform(const Matrix &argMatrix);
	void setMaterial(const Material &argMaterial);
};

class Sphere : public Form {
public:
	Sphere();
	bool checkEqual(Form other) override;
	// Intersections getIntersections(Ray argRay) override;
	Intersections getIntersectionsLocal(Ray argRay) override;
	Vector getNormalLocal(Point argPoint) override;
};

class Plane : public Form {
public:
	// Intersections getIntersections(Ray argRay) override;
	Intersections getIntersectionsLocal(Ray argRay) override;
	Vector getNormalLocal(Point argPoint) override;
};

#endif