#include "pch.h";

Ray::Ray(Point origin, Vector direction)
{
	this->origin = origin;
	this->direction = direction;
}
Point Ray::position(double time)
{
	return origin + direction * time;
}