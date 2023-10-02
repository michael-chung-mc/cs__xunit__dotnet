#include "intersection.h"
#include "sphere.h"

Intersection::Intersection()
{
	this->object = Sphere();
	this->time = 0;
}
Intersection::Intersection(double time, Sphere s)
{
	this->object = s;
	this->time = time;
}

bool Intersection::checkEqual(Intersection other)
{
	return time == other.time && object.checkEqual(other.object);
}

Intersections::Intersections()
{
	Intersection i = Intersection(0, Sphere());
	this->intersections.push_back(i);
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
	return index < 0 ? Intersection() : intersections[index];
}

Intersections& Intersections::operator=(const Intersections other)
{
	if (this == &other) return *this;
	this->intersections = other.intersections;
	return *this;
}