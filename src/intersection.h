#pragma once
#ifndef INTERSECTION_H
#define INTERSECTION_H

#include "form.h"
#include "tuple.h"
class Ray;
#include <vector>

struct IntersectionState {
public:
	double mbrTime;
	Form mbrObject;
	Point mbrPoint;
	Point mbrOverPoint;
	Vector mbrEye;
	Vector argNormal;
	bool argInside;
};

class Intersection {
public:
	bool mbrExists;
	double mbrTime;
	Form mbrObject;
	Intersection();
	Intersection(double time, Form s);
	bool checkEqual(Intersection other);
	bool operator<(Intersection other) const;
	IntersectionState getState(Ray argRay);
};


class Intersections {
public:
	std::vector<Intersection> mbrIntersections;
	Intersections();
	Intersections(const Intersections& other);
	Intersections(double time, Form s);
	void intersect(double time, Form s);
	Intersection hit();
	Intersections& operator=(const Intersections other);
};

#endif