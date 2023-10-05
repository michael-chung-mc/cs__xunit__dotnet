#pragma once
#ifndef INTERSECTION_H
#define INTERSECTION_H

#include "form.h"
#include "tuple.h"
class Ray;
#include <vector>

struct IntersectionState {
public:
	double time;
	Form object;
	Point point;
	Point mbrOverPoint;
	Vector pov;
	Vector normal;
	bool inside;
};

class Intersection {
public:
	bool mbrExists;
	double time;
	Form object;
	Intersection();
	Intersection(double time, Form s);
	bool checkEqual(Intersection other);
	bool operator<(Intersection other) const;
	IntersectionState getState(Ray argRay);
};


class Intersections {
public:
	std::vector<Intersection> intersections;
	Intersections();
	Intersections(const Intersections& other);
	Intersections(double time, Form s);
	void intersect(double time, Form s);
	Intersection hit();
	Intersections& operator=(const Intersections other);
};

#endif