#include "ray.h"
#include "tuple.h"
#include "matrix.h"

Ray::Ray()
{
	this->origin = Point(0,0,0);
	this->direction = Vector(0,0,0);
}

Ray::Ray(Point origin, Vector direction)
{
	this->origin = origin;
	//std::cout << this->origin.x << std::endl;
	this->direction = direction;
}
Point Ray::position(double time)
{
	return origin + direction * time;
}
Ray Ray::transform(Matrix matrix)
{
	return Ray(matrix * this->origin, matrix * direction);
}
Ray Ray::transform(TranslationMatrix matrix)
{
	return Ray(matrix * this->origin, matrix * direction);
}
Ray Ray::transform(ScalingMatrix matrix)
{
	return Ray(matrix * this->origin, matrix * direction);
}