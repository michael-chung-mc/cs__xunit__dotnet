#pragma once
#ifndef INTERSECTION_H
#define INTERSECTION_H

#include "form.h"
#include "tuple.h"
class Ray;
#include <vector>

class IntersectionState {
public:
	double mbrTime;
	std::unique_ptr<Form> mbrObject;
	Point mbrPoint;
	Point mbrOverPoint;
	Vector mbrEye;
	Vector mbrNormal;
	bool mbrInside;
	IntersectionState();
	IntersectionState(const IntersectionState &argOther);
	virtual ~IntersectionState() = default;
	IntersectionState& operator=(const IntersectionState &argOther);
    void setObject(Form* argObject);
};

class Intersection {
public:
	bool mbrExists;
	double mbrTime;
	std::unique_ptr<Form> mbrObject;
	Intersection();
	Intersection(const Intersection &argOther);
	Intersection(double time, Form *s);
	// Intersection(double time, std::unique_ptr<Form> s);
	virtual ~Intersection() = default;
	Intersection& operator=(const Intersection argOther);
	bool checkEqual(Intersection other);
	IntersectionState getState(Ray argRay);
    void setObject(Form* argObject);
};


class Intersections {
public:
    std::vector<std::unique_ptr<Intersection>> mbrIntersections;
	Intersections();
	Intersections(const Intersections& other);
	Intersections(double time, Form *s);
	// Intersections(double time, std::unique_ptr<Form> s);
	virtual ~Intersections() = default;
	Intersections& operator=(const Intersections other);
	void intersect(double time, Form* argObject);
	// void intersect(double t, std::unique_ptr<Form> argObject);
	Intersection hit();
	void setIntersection(Intersection* argIx);
    void setIntersection(double t, Form* argObject);
};

#endif