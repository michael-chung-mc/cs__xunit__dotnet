#include "ray.h"
#include "tuple.h"
#include "matrix.h"

Ray::Ray()
{
	this->argOrigin = Point(0,0,0);
	this->argDirection = Vector(0,0,0);
}

Ray::Ray(Point origin, Vector direction)
{
	this->argOrigin = origin;
	//std::cout << this->origin.x << std::endl;
	this->argDirection = direction;
}
Point Ray::getPosition(double time)
{
	return argOrigin + argDirection * time;
}
Ray Ray::transform(Matrix matrix)
{
	return Ray(matrix * this->argOrigin, matrix * argDirection);
}
Ray Ray::transform(TranslationMatrix matrix)
{
	return Ray(matrix * this->argOrigin, matrix * argDirection);
}
Ray Ray::transform(ScalingMatrix matrix)
{
	return Ray(matrix * this->argOrigin, matrix * argDirection);
}