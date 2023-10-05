#include "ray.h"
#include "tuple.h"
#include "matrix.h"
#include "comparinator.h"

Ray::Ray()
{
	this->mbrOrigin = Point(0,0,0);
	this->mbrDirection = Vector(0,0,0);
}

Ray::Ray(Point origin, Vector direction)
{
	this->mbrOrigin = origin;
	//std::cout << this->origin.x << std::endl;
	this->mbrDirection = direction;
}
Point Ray::getPosition(double time)
{
	return mbrOrigin + mbrDirection * time;
}
bool Ray::checkEqual(Ray other)
{
	Comparinator varComp = Comparinator();
	return varComp.checkTuple(mbrOrigin, other.mbrOrigin) && varComp.checkTuple(mbrDirection, other.mbrDirection);
}
Ray Ray::transform(Matrix matrix)
{
	return Ray(matrix * this->mbrOrigin, matrix * mbrDirection);
}
Ray Ray::transform(TranslationMatrix matrix)
{
	return Ray(matrix * this->mbrOrigin, matrix * mbrDirection);
}
Ray Ray::transform(ScalingMatrix matrix)
{
	return Ray(matrix * this->mbrOrigin, matrix * mbrDirection);
}