#include "pch.h";

Intersection::Intersection()
{
	object = Sphere();
	time = 0;
}

Intersection::Intersection(double t, Sphere s)
{
	object = s;
	time = t;
	intersections.push_back(*this);
}

bool Intersection::checkEqual(Intersection other)
{
	return time == other.time && object.checkEqual(other.object);
}

void Intersection::intersect(double t, Sphere s)
{
	intersections.push_back(Intersection(t, s));
}

Intersection Intersection::hit()
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