#include "intersection.h"
#include "form.h"
#include "ray.h"
#include "tuple.h"
#include "pch.h"
#include <algorithm>

Intersection::Intersection()
{
	this->object = Sphere();
	this->time = 0;
	mbrExists = true;
}
Intersection::Intersection(double time, Sphere s)
{
	this->object = s;
	this->time = time;
	mbrExists = true;
}

bool Intersection::checkEqual(Intersection other)
{
	return time == other.time && object.checkEqual(other.object);
}
bool Intersection::operator<(Intersection other) const
{
	return time < other.time;
}

IntersectionState Intersection::getState(Ray argRay)
{
	IntersectionState is = IntersectionState();
	is.time = time;
	is.object = object;
	is.point = argRay.position(is.time);
	is.pov = -(argRay.direction);
	is.normal = is.object.normal(is.point);
	if (is.normal.dot(is.pov) < 0) {
		is.inside = true;
		is.normal = -is.normal;
	}
	else {
		is.inside = false;
	}
	is.mbrOverPoint = is.point + is.normal * getEpsilon();
	return is;
}

Intersections::Intersections()
{
	//Intersection i = Intersection(0, Sphere());
	//this->intersections.push_back(i);
}
Intersections::Intersections(const Intersections& other)
{
	this->intersections = other.intersections;
}
Intersections::Intersections(double t, Sphere s)
{
	Intersection i = Intersection(t,s);
	intersections.push_back(i);
}
void Intersections::intersect(double t, Sphere s)
{
	intersections.push_back(Intersection(t, s));
    sort(intersections.begin(), intersections.end());
}

Intersection Intersections::hit()
{
	int index = -1;
	for (int i = 0; i < intersections.size(); i++)
	{
		if ((intersections[i].time > 0)
			&& ((index < 0) || (intersections[i].time <= intersections[index].time)))
		{
			index = i;
		}
	}
	if (index < 0) {
		//std::cout << "hit missed" << std::endl;
		Intersection varIx = Intersection();
		varIx.mbrExists = false;
		return varIx;
	}
	else {
		//std::cout << "hit" << std::endl;
		return intersections[index];
	}
}

Intersections& Intersections::operator=(const Intersections other)
{
	if (this == &other) return *this;
	this->intersections = other.intersections;
	return *this;
}