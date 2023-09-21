#include "pch.h";

Ray::Ray(Point origin, Vector direction)
{
	this->origin = origin;
	std::cout << this->origin.x << std::endl;
	this->direction = direction;
}
Point Ray::position(double time)
{
	return origin + direction * time;
}
Ray Ray::transform(TranslationMatrix matrix)
{
	return Ray(matrix * this->origin, direction);
}
Ray Ray::transform(ScalingMatrix matrix)
{
	return Ray(matrix * this->origin, direction);
}